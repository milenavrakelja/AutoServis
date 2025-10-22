using AutoServis.WorkerApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AutoServis.WorkerApp.Views;

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
        var ime = ImeBox.Text.Trim();
        var prezime = PrezimeBox.Text.Trim();
        var kontakt = KontaktBox.Text.Trim();
        var username = UsernameBox.Text.Trim();
        var password = PasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;

        // Obavezna polja
        if (string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Ime, korisničko ime i lozinka su obavezni.");
            return;
        }

        if (password != confirmPassword)
        {
            MessageBox.Show("Lozinke se ne poklapaju.");
            return;
        }

        var (uspeh, poruka) = await _viewModel.RegisterAsync(ime, prezime, kontakt, username, password);
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