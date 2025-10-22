using AutoServis.ClientApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AutoServis.ClientApp.Views;

public partial class RegisterView : UserControl
{
    private readonly RegisterViewModel _viewModel;

    public RegisterView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<RegisterViewModel>();
    }

    private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
    {
        var username = UsernameBox.Text.Trim();
        var password = PasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;
        var kontakt = ContactBox.Text.Trim();
        var adresa = AddressBox.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(kontakt) || string.IsNullOrEmpty(adresa))
        {
            var msg = Application.Current.FindResource("Client_Register_SvaPoljaObavezna") as string
                      ?? "Sva polja su obavezna.";
            MessageBox.Show(msg);
            return;
        }

        if (password != confirmPassword)
        {
            var msg = Application.Current.FindResource("Client_Register_LozinkeNePoklapaju") as string
                      ?? "Lozinke se ne poklapaju.";
            MessageBox.Show(msg);
            return;
        }

        var (uspeh, poruka) = await _viewModel.RegisterAsync(username, password, kontakt, adresa);
        MessageBox.Show(poruka); 

        if (uspeh)
        {
            NavigateToLogin();
        }
    }

    private void LoginBtn_Click(object sender, RoutedEventArgs e)
    {
        NavigateToLogin();
    }

    private void NavigateToLogin()
    {
        var mainWindow = Window.GetWindow(this);
        if (mainWindow is MainWindow mw)
            mw.MainFrame.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));
    }
}