using AutoServis.ClientApp.ViewModels;
using AutoServis.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoServis.ClientApp.Views;

public partial class RezervacijeView : UserControl
{
    private readonly RezervacijeViewModel _viewModel;
    private int _klijentId;

    public RezervacijeView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<RezervacijeViewModel>();
        DataContext = _viewModel; // 🔑 obavezno za binding

        if (App.LoggedInKlijentId == null)
        {
            var msg = Application.Current.FindResource("Client_Rezervacije_NistePrijavljeni") as string
                      ?? "Niste prijavljeni! Vraćanje na login...";
            MessageBox.Show(msg);
            var mainWindow = Window.GetWindow(this);
            if (mainWindow is MainWindow mw)
            {
                var loginView = App.Services.GetRequiredService<LoginView>();
                mw.MainFrame.Navigate(loginView);
            }
            return;
        }

        _klijentId = App.LoggedInKlijentId.Value;
        Loaded += async (s, e) => await LoadRezervacije();
    }

    private async void KreirajBtn_Click(object sender, RoutedEventArgs e)
    {
        var model = ModelBox.Text.Trim();
        if (string.IsNullOrEmpty(model))
        {
            var msg = Application.Current.FindResource("Client_Rezervacije_UnesiteModel") as string
                      ?? "Unesite model vozila.";
            MessageBox.Show(msg);
            return;
        }

        if (!int.TryParse(GodinaBox.Text, out int godina) || godina < 1900 || godina > DateTime.Now.Year + 2)
        {
            var msg = Application.Current.FindResource("Client_Rezervacije_ValidnaGodina") as string
                      ?? "Unesite validnu godinu proizvodnje (npr. 2020).";
            MessageBox.Show(msg);
            return;
        }

        var opis = OpisBox.Text.Trim();
        if (string.IsNullOrEmpty(opis))
        {
            var msg = Application.Current.FindResource("Client_Rezervacije_UnesiteOpis") as string
                      ?? "Unesite opis problema.";
            MessageBox.Show(msg);
            return;
        }

        if (!DateOnly.TryParse(DatumBox.Text, out DateOnly datumUsluge))
        {
            var msg = Application.Current.FindResource("Client_Rezervacije_ValidanDatum") as string
                      ?? "Unesite datum u formatu YYYY-MM-DD (npr. 2025-04-10).";
            MessageBox.Show(msg);
            return;
        }

        if (datumUsluge < DateOnly.FromDateTime(DateTime.Today))
        {
            var msg = Application.Current.FindResource("Client_Rezervacije_DatumProšlost") as string
                      ?? "Datum usluge ne može biti u prošlosti.";
            MessageBox.Show(msg);
            return;
        }

        try
        {
            await _viewModel.KreirajRezervacijuAsync(_klijentId, model, godina, opis, datumUsluge);
            await LoadRezervacije();
            var uspjehMsg = Application.Current.FindResource("Client_Rezervacije_UspjehKreiranja") as string
                            ?? "Rezervacija je uspešno kreirana!";
            var uspjehTitle = Application.Current.FindResource("Client_Rezervacije_UspjehTitle") as string
                              ?? "Uspeh";
            MessageBox.Show(uspjehMsg, uspjehTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            var greskaMsg = Application.Current.FindResource("Client_Rezervacije_GreskaKreiranja") as string
                            ?? "Greška prilikom kreiranja rezervacije:";
            var greskaTitle = Application.Current.FindResource("Client_Rezervacije_GreskaTitle") as string
                              ?? "Greška";
            MessageBox.Show($"{greskaMsg}\n{ex.Message}", greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task LoadRezervacije()
    {
        await _viewModel.LoadRezervacijeAsync(_klijentId);
        // ❌ NIKAKO NE POSTAVLJAJ RezervacijeGrid.ItemsSource RUČNO!
    }
}