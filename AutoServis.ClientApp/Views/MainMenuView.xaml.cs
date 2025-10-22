using AutoServis.ClientApp.Services;
using AutoServis.ClientApp.ViewModels;
using AutoServis.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutoServis.ClientApp.Views;

public partial class MainMenuView : UserControl
{
    private Button? _trenutnoOdabranoDugme;

    public MainMenuView()
    {
        InitializeComponent();

        // Primijeni sačuvanu temu
        if (!string.IsNullOrEmpty(App.SelectedTheme))
        {
            System.Diagnostics.Debug.WriteLine($"Primjenjujem temu iz baze: {App.SelectedTheme}");
            ThemeService.ApplyTheme(App.SelectedTheme);
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Nema sačuvane teme u bazi.");
        }

        Loaded += async (s, e) =>
        {
            var tipUslugeView = App.Services.GetRequiredService<TipUslugeView>();
            ContentFrame.Navigate(tipUslugeView);
            // Označi početno dugme
            OznačiOdabranoDugme(TipoviBtn);
        };
    }

    private void OznačiOdabranoDugme(Button dugme)
    {
        // Ukloni okvir sa prethodnog
        if (_trenutnoOdabranoDugme != null)
        {
            _trenutnoOdabranoDugme.BorderThickness = new Thickness(0);
        }

        // Postavi crni okvir na novo odabrano
        dugme.BorderBrush = Brushes.Black;
        dugme.BorderThickness = new Thickness(2);

        _trenutnoOdabranoDugme = dugme;
    }

    private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string themeName = selectedItem.Tag?.ToString();
            ThemeService.ApplyTheme(themeName);
        }
    }

    private void TipoviBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(TipoviBtn);
        var view = App.Services.GetRequiredService<TipUslugeView>();
        ContentFrame.Navigate(view);
    }

    private void RezervacijeBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(RezervacijeBtn);
        var view = App.Services.GetRequiredService<RezervacijeView>();
        ContentFrame.Navigate(view);
    }

    private void RacuniBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(RacuniBtn);
        var view = App.Services.GetRequiredService<RacuniView>();
        ContentFrame.Navigate(view);
    }

    private async void LogoutBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(LogoutBtn); // opcionalno — možete i preskočiti

        var klijent = App.Current.Properties["LoggedInKlijent"] as Klijent;
        if (klijent != null)
        {
            var viewModel = App.Services.GetRequiredService<MainMenuViewModel>();
            await viewModel.SaveUserThemeAsync(klijent.KlijentId, ThemeService.CurrentTheme);
        }

        App.Current.Properties.Remove("LoggedInKlijent");

        var mainWindow = Window.GetWindow(this) as MainWindow;
        if (mainWindow != null)
        {
            var loginView = App.Services.GetRequiredService<LoginView>();
            mainWindow.MainFrame.Navigate(loginView);
        }
    }
}