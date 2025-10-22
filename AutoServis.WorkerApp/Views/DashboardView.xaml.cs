using AutoServis.WorkerApp.Services;
using AutoServis.Core.Models;
using AutoServis.WorkerApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutoServis.WorkerApp.Views;

public partial class DashboardView : UserControl
{
    private readonly DashboardViewModel _viewModel;
    private Button? _trenutnoOdabranoDugme;

    public DashboardView()
    {
        InitializeComponent();

        if (!string.IsNullOrEmpty(App.SelectedTheme))
        {
            ThemeService.ApplyTheme(App.SelectedTheme);
        }

        _viewModel = App.Services.GetRequiredService<DashboardViewModel>();

        var radnik = App.Current.Properties["LoggedInRadnik"] as Radnik;
        if (radnik != null)
        {
            _viewModel.SetCurrentUser(radnik);
        }

        this.DataContext = _viewModel;

        Loaded += (s, e) =>
        {
            // Početna selekcija
            TipoviBtn_Click(null, null);
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

    private void TipoviBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(TipoviBtn);
        ContentFrame.Navigate(new TipoviUslugeView());
    }

    private void DijeloviBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(DijeloviBtn);
        ContentFrame.Navigate(new DijeloviLagerView());
    }

    private void RezervacijeBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(RezervacijeBtn);
        ContentFrame.Navigate(new RezervacijeView());
    }

    private void KlijentiBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(KlijentiBtn);
        ContentFrame.Navigate(new KlijentiView());
    }

    private void KorisniciBtn_Click(object sender, RoutedEventArgs e)
    {
        OznačiOdabranoDugme(KorisniciBtn);
        ContentFrame.Navigate(new UpravljanjeKorisnicimaView());
    }

    private async void LogoutBtn_Click(object sender, RoutedEventArgs e)
    {
        await _viewModel.SaveUserThemeAsync(ThemeService.CurrentTheme);
        App.Current.Properties.Remove("LoggedInRadnik");

        var mainWindow = Window.GetWindow(this) as MainWindow;
        mainWindow?.MainFrame.Navigate(new Uri("Views/LoginView.xaml", UriKind.Relative));
    }

    private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string themeName = selectedItem.Tag?.ToString() ?? "Blue";
            ThemeService.ApplyTheme(themeName);
        }
    }
}