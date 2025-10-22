using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core.Models;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using System;

namespace AutoServis.WorkerApp.Views;

public partial class UpravljanjeKorisnicimaView : UserControl
{
    private readonly UpravljanjeKorisnicimaViewModel _viewModel;

    public UpravljanjeKorisnicimaView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<UpravljanjeKorisnicimaViewModel>();
        LoadData();
    }

    private async void LoadData()
    {
        await _viewModel.LoadRadnikeAsync();
        RadniciGrid.ItemsSource = _viewModel.Radnici;
    }

    private async void ObrisiRadnikaBtn_Click(object sender, RoutedEventArgs e)
    {
        var selektovani = RadniciGrid.SelectedItem as Radnik;
        if (selektovani == null)
        {
            var msg = Application.Current.FindResource("Worker_Korisnici_IzaberiteZaBrisanje") as string
                      ?? "Izaberite radnika za brisanje.";
            MessageBox.Show(msg);
            return;
        }

        var format = Application.Current.FindResource("Worker_Korisnici_PotvrdaBrisanja") as string
                     ?? "Da li želite da obrišete radnika '{0}'?";
        var message = string.Format(format, selektovani.KorisnickoIme);
        var title = Application.Current.FindResource("Worker_Korisnici_PotvrdaBrisanjaTitle") as string ?? "Potvrda brisanja";

        
        var potvrda = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (potvrda == MessageBoxResult.Yes) 
        {
            await _viewModel.ObrisiRadnikaAsync(selektovani.RadnikId);
            LoadData();
        }
    }

    private async void DodajRadnikaBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var title = Application.Current.FindResource("Worker_Korisnici_DodajRadnikaTitle") as string ?? "Add Worker";
            var imePrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteIme") as string ?? "Enter first name:";
            var prezimePrompt = Application.Current.FindResource("Worker_Korisnici_UnesitePrezime") as string ?? "Enter last name (optional):";
            var korisnickoImePrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteKorisnickoIme") as string ?? "Enter username:";
            var lozinkaPrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteLozinku") as string ?? "Enter password:";
            var kontaktPrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteKontakt") as string ?? "Enter contact (phone/email, optional):";
            var ulogaPrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteUlogu") as string ?? "Enter role (Worker/Admin):";

            var ime = Microsoft.VisualBasic.Interaction.InputBox(imePrompt, title);
            if (string.IsNullOrWhiteSpace(ime)) return;

            var prezime = Microsoft.VisualBasic.Interaction.InputBox(prezimePrompt, title);
            var korisnickoIme = Microsoft.VisualBasic.Interaction.InputBox(korisnickoImePrompt, title);
            if (string.IsNullOrWhiteSpace(korisnickoIme)) return;

            var lozinka = Microsoft.VisualBasic.Interaction.InputBox(lozinkaPrompt, title);
            if (string.IsNullOrWhiteSpace(lozinka)) return;

            var kontakt = Microsoft.VisualBasic.Interaction.InputBox(kontaktPrompt, title);
            var uloga = Microsoft.VisualBasic.Interaction.InputBox(ulogaPrompt, title, "Worker");

            if (uloga != "Worker" && uloga != "Admin" && uloga != "Radnik"  && uloga != "Administratro")
            {
                var greskaUloga = Application.Current.FindResource("Worker_Korisnici_GreskaUloga") as string
                                  ?? "Role must be 'Worker' or 'Admin'.";
                var greskaTitle = Application.Current.FindResource("Worker_Greska") as string ?? "Error";
                MessageBox.Show(greskaUloga, greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await _viewModel.DodajRadnikaAsync(ime, prezime, korisnickoIme, lozinka, kontakt, uloga);
            LoadData();
        }
        catch (Exception ex)
        {
            var greskaTitle = Application.Current.FindResource("Worker_Greska") as string ?? "Error";
            MessageBox.Show($"Greška prilikom dodavanja radnika:\n{ex.Message}", greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void IzmijeniRadnikaBtn_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var radnik = button?.Tag as Radnik;
        if (radnik == null) return;

        try
        {
            var title = Application.Current.FindResource("Worker_Korisnici_IzmijeniRadnikaTitle") as string ?? "Edit Worker";
            var imePrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteIme") as string ?? "Enter first name:";
            var prezimePrompt = Application.Current.FindResource("Worker_Korisnici_UnesitePrezime") as string ?? "Enter last name (optional):";
            var korisnickoImePrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteKorisnickoIme") as string ?? "Enter username:";
            var lozinkaPrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteNovuLozinku") as string ?? "Enter new password (leave empty to keep current):";
            var kontaktPrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteKontakt") as string ?? "Enter contact:";
            var ulogaPrompt = Application.Current.FindResource("Worker_Korisnici_UnesiteUlogu") as string ?? "Enter role (Worker/Admin):";

            var ime = Microsoft.VisualBasic.Interaction.InputBox(imePrompt, title, radnik.Ime);
            if (string.IsNullOrWhiteSpace(ime)) return;

            var prezime = Microsoft.VisualBasic.Interaction.InputBox(prezimePrompt, title, radnik.Prezime ?? "");
            var korisnickoIme = Microsoft.VisualBasic.Interaction.InputBox(korisnickoImePrompt, title, radnik.KorisnickoIme);
            if (string.IsNullOrWhiteSpace(korisnickoIme)) return;

            var lozinka = Microsoft.VisualBasic.Interaction.InputBox(lozinkaPrompt, title);
            var kontakt = Microsoft.VisualBasic.Interaction.InputBox(kontaktPrompt, title, radnik.Kontakt ?? "");
            var uloga = Microsoft.VisualBasic.Interaction.InputBox(ulogaPrompt, title, radnik.Uloga);

            if (uloga != "Radnik" && uloga != "Administator")
            {
                var greskaUloga = Application.Current.FindResource("Worker_Korisnici_GreskaUloga") as string
                                  ?? "Role must be 'Worker' or 'Admin'.";
                var greskaTitle = Application.Current.FindResource("Worker_Greska") as string ?? "Error";
                MessageBox.Show(greskaUloga, greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string? lozinkaZaCuvanje = null;
            if (!string.IsNullOrWhiteSpace(lozinka))
            {
                lozinkaZaCuvanje = lozinka;
            }

            await _viewModel.IzmijeniRadnikaAsync(radnik.RadnikId, ime, prezime, korisnickoIme, lozinkaZaCuvanje, kontakt, uloga);
            LoadData();
        }
        catch (Exception ex)
        {
            var greskaTitle = Application.Current.FindResource("Worker_Greska") as string ?? "Error";
            MessageBox.Show($"Greška prilikom izmene radnika:\n{ex.Message}", greskaTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}