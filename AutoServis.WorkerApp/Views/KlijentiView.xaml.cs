using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core.Models;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoServis.WorkerApp.Views;

public partial class KlijentiView : UserControl
{
    private readonly KlijentiViewModel _viewModel;

    public KlijentiView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<KlijentiViewModel>();
        DataContext = _viewModel; // 🔑 obavezno za binding
        LoadData();
    }

    private async void LoadData()
    {
        await _viewModel.LoadKlijenteAsync();
        // ❌ NIKAKO NE POSTAVLJAJ KlijentiGrid.ItemsSource RUČNO!
    }

    private async void DodajKlijentaBtn_Click(object sender, RoutedEventArgs e)
    {
        var title = Application.Current.FindResource("Worker_Klijenti_DodajKlijentaTitle") as string ?? "Add Client";
        var korisnickoImePrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteKorisnickoIme") as string ?? "Enter username:";
        var lozinkaPrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteLozinku") as string ?? "Enter password:";
        var kontaktPrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteKontakt") as string ?? "Enter contact (optional):";
        var adresaPrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteAdresu") as string ?? "Enter address (optional):";

        var korisnickoIme = Microsoft.VisualBasic.Interaction.InputBox(korisnickoImePrompt, title);
        if (string.IsNullOrWhiteSpace(korisnickoIme)) return;

        var lozinka = Microsoft.VisualBasic.Interaction.InputBox(lozinkaPrompt, title);
        if (string.IsNullOrWhiteSpace(lozinka)) return;

        var kontakt = Microsoft.VisualBasic.Interaction.InputBox(kontaktPrompt, title);
        var adresa = Microsoft.VisualBasic.Interaction.InputBox(adresaPrompt, title);

        try
        {
            await _viewModel.DodajKlijentaAsync(korisnickoIme, lozinka, kontakt, adresa);
            LoadData();
        }
        catch (Exception ex)
        {
            var greskaTitle = Application.Current.FindResource("Worker_Greska") as string ?? "Error";
            MessageBox.Show($"Greška prilikom dodavanja klijenta:\n{ex.Message}", greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void ObrisiKlijentaBtn_Click(object sender, RoutedEventArgs e)
    {
        var selektovani = KlijentiGrid.SelectedItem as Klijent;
        if (selektovani == null)
        {
            var msg = Application.Current.FindResource("Worker_Klijenti_IzaberiteZaBrisanje") as string
                      ?? "Izaberite klijenta za brisanje.";
            MessageBox.Show(msg);
            return;
        }

        var format = Application.Current.FindResource("Worker_Klijenti_PotvrdaBrisanja") as string
                     ?? "Da li želite da obrišete klijenta '{0}'?";
        var message = string.Format(format, selektovani.KorisnickoIme);
        var title = Application.Current.FindResource("Worker_Klijenti_PotvrdaBrisanjaTitle") as string ?? "Potvrda brisanja";

        var potvrda = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (potvrda == MessageBoxResult.Yes)
        {
            await _viewModel.ObrisiKlijentaAsync(selektovani.KlijentId);
            LoadData();
        }
    }

    private async void IzmijeniKlijentaBtn_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var klijent = button?.Tag as Klijent;
        if (klijent == null) return;

        try
        {
            var title = Application.Current.FindResource("Worker_Klijenti_IzmijeniKlijentaTitle") as string ?? "Edit Client";
            var korisnickoImePrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteKorisnickoIme") as string ?? "Enter username:";
            var lozinkaPrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteNovuLozinku") as string ?? "Enter new password (leave empty to keep current):";
            var kontaktPrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteKontakt") as string ?? "Enter contact:";
            var adresaPrompt = Application.Current.FindResource("Worker_Klijenti_UnesiteAdresu") as string ?? "Enter address:";

            var korisnickoIme = Microsoft.VisualBasic.Interaction.InputBox(korisnickoImePrompt, title, klijent.KorisnickoIme);
            if (string.IsNullOrWhiteSpace(korisnickoIme)) return;

            var lozinka = Microsoft.VisualBasic.Interaction.InputBox(lozinkaPrompt, title);
            var kontakt = Microsoft.VisualBasic.Interaction.InputBox(kontaktPrompt, title, klijent.Kontakt ?? "");
            var adresa = Microsoft.VisualBasic.Interaction.InputBox(adresaPrompt, title, klijent.Adresa ?? "");

            string? lozinkaZaCuvanje = null;
            if (!string.IsNullOrWhiteSpace(lozinka))
            {
                lozinkaZaCuvanje = lozinka;
            }
            await _viewModel.IzmijeniKlijentaAsync(klijent.KlijentId, korisnickoIme, lozinkaZaCuvanje, kontakt, adresa);
            LoadData();
        }
        catch (Exception ex)
        {
            var greskaTitle = Application.Current.FindResource("Worker_Greska") as string ?? "Error";
            MessageBox.Show($"Greška prilikom izmene klijenta:\n{ex.Message}", greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}