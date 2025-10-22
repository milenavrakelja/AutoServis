using AutoServis.ClientApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AutoServis.ClientApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        
        MainFrame.NavigationFailed += (s, e) =>
        {
            System.Windows.MessageBox.Show($"Navigacija nije uspela:\n{e.Exception?.Message}");
            e.Handled = true;
        };

        var loginView = App.Services.GetRequiredService<LoginView>();
        MainFrame.Navigate(loginView);
    }
}