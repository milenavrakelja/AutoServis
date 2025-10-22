using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core.Models;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoServis.WorkerApp.Services;


namespace AutoServis.WorkerApp.Views;

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
            MessageBox.Show("Unesite korisničko ime i lozinku.");
            return;
        }

        var radnik = await _viewModel.LoginAsync(username, password);

        if (radnik != null)
        {
            App.SelectedTheme = radnik.SelectedTheme; 
            App.Current.Properties["LoggedInRadnik"] = radnik;

            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.MainFrame.Navigate(new Uri("Views/DashboardView.xaml", UriKind.Relative));
        }
        else
        {
            MessageBox.Show("Pogrešno korisničko ime ili lozinka.");
        }
    }
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordHint.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void RegisterBtn_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = Window.GetWindow(this);
        if (mainWindow is MainWindow mw)
            mw.MainFrame.Navigate(new Uri("Views/RegisterView.xaml", UriKind.Relative));
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