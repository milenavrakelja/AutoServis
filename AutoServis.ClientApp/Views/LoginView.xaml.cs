using AutoServis.ClientApp.ViewModels;
using AutoServis.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using AutoServis.ClientApp.Services;

namespace AutoServis.ClientApp.Views;

public partial class LoginView : UserControl
{
    private readonly LoginViewModel _viewModel;

    public LoginView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<LoginViewModel>();
    }
    private async void LoginBtn_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameBox.Text.Trim();
        var password = PasswordBox.Password;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            var msg = Application.Current.FindResource("Client_Login_UnesiteKredencijale") as string
                      ?? "Unesite korisničko ime i lozinku.";
            MessageBox.Show(msg);
            return;
        }

        var klijent = await _viewModel.LoginAsync(username, password);


        if (klijent != null)
        {

            App.Current.Properties["LoggedInKlijent"] = klijent;
            App.SelectedTheme = klijent.SelectedTheme; 

            App.LoggedInKlijentId = klijent.KlijentId; 



            var mainWindow = Window.GetWindow(this);
            if (mainWindow is MainWindow mw)
            {
                var mainMenu = App.Services.GetRequiredService<MainMenuView>();
                mw.MainFrame.Navigate(mainMenu);
            }
        }
        else
        {
            var msg = Application.Current.FindResource("Client_Login_PogresnoKredencijale") as string
                      ?? "Pogrešno korisničko ime ili lozinka.";
            MessageBox.Show(msg);
        }
    }
   

    

    private void RegisterBtn_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = Window.GetWindow(this);
        if (mainWindow is MainWindow mw)
        {
            var registerView = App.Services.GetRequiredService<RegisterView>();
            mw.MainFrame.Navigate(registerView);
        }

    }
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordHint.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }
    private void LanguageComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (sender is System.Windows.Controls.ComboBox comboBox && comboBox.SelectedItem is System.Windows.Controls.ComboBoxItem selectedItem)
        {
            string lang = selectedItem.Tag.ToString();
            ChangeLanguage(lang);
        }
    }

    private void ChangeLanguage(string langCode)
    {
        
        var dictionaries = Application.Current.Resources.MergedDictionaries;
        for (int i = dictionaries.Count - 1; i >= 0; i--)
        {
            if (dictionaries[i].Source?.OriginalString.Contains("Strings.") == true)
            {
                dictionaries.RemoveAt(i);
            }
        }

        // Dodaj novi jezički resurs
        var newLangDict = new ResourceDictionary
        {
            Source = new Uri($"Resources/Strings.{langCode}.xaml", UriKind.Relative)
        };
        dictionaries.Add(newLangDict);
    }
}