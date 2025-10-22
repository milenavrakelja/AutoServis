using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media;
using System;

namespace AutoServis.WorkerApp.Views;

public partial class RezervacijeView : UserControl
{
    private readonly RezervacijeViewModel _viewModel;
    private Rezervacija? _izabranaRezervacija;
    private readonly HashSet<int> _izabraneUslugeIds = new();
    private readonly Dictionary<int, int> _izabraniDijeloviKolicine = new();

    public RezervacijeView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<RezervacijeViewModel>();
        Loaded += async (s, e) => await UcitajPodatke();
    }

    private async Task UcitajPodatke()
    {
        await _viewModel.UcitajSveAsync();
        RezervacijeGrid.ItemsSource = _viewModel.Rezervacije;
        StatusCombo.ItemsSource = _viewModel.SviStatusi;
        UslugeListBox.ItemsSource = _viewModel.SviTipoviUsluge;
        DijeloviListBox.ItemsSource = _viewModel.SviDijelovi;
        _izabraneUslugeIds.Clear();
        _izabraniDijeloviKolicine.Clear();
        AzurirajUkupnuCenu();
    }

    private void RezervacijeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _izabranaRezervacija = RezervacijeGrid.SelectedItem as Rezervacija;
        if (_izabranaRezervacija != null)
        {
            PrikaziDetaljeKlijenta();
            DetaljiPanel.Visibility = Visibility.Visible;
            _izabraneUslugeIds.Clear();
            _izabraniDijeloviKolicine.Clear();
            AzurirajUkupnuCenu();

            
            IzdajRacunBtn.IsEnabled = (_izabranaRezervacija.StatusId == 3);
        }
        else
        {
            DetaljiPanel.Visibility = Visibility.Collapsed;
            IzdajRacunBtn.IsEnabled = false;
        }
    }

    private void PrikaziDetaljeKlijenta()
    {
        if (_izabranaRezervacija?.Klijent != null)
        {
            KlijentImeText.Text = _izabranaRezervacija.Klijent.KorisnickoIme;
            KlijentKontaktText.Text = _izabranaRezervacija.Klijent.Kontakt ?? "-";
            KlijentAdresaText.Text = _izabranaRezervacija.Klijent.Adresa ?? "-";
        }
        else
        {
            KlijentImeText.Text = "-";
            KlijentKontaktText.Text = "-";
            KlijentAdresaText.Text = "-";
        }
    }

    private void Usluga_Checked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var tip = checkBox?.Tag as TipUsluge;
        if (tip != null)
        {
            _izabraneUslugeIds.Add(tip.TipUslugeId);
            AzurirajUkupnuCenu();
        }
    }

    private void Usluga_Unchecked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var tip = checkBox?.Tag as TipUsluge;
        if (tip != null)
        {
            _izabraneUslugeIds.Remove(tip.TipUslugeId);
            AzurirajUkupnuCenu();
        }
    }

    private void Dio_Checked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var dio = checkBox?.DataContext as Dio; 
        if (dio != null)
        {
            _izabraniDijeloviKolicine[dio.DioId] = 1;
            OmoguciKolicinaBox(dio, true);
        }
    }

    private void Dio_Unchecked(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        var dio = checkBox?.DataContext as Dio; 
        if (dio != null)
        {
            _izabraniDijeloviKolicine.Remove(dio.DioId);
            OmoguciKolicinaBox(dio, false);
        }
    }

    private void KolicinaBox_LostFocus(object sender, RoutedEventArgs e)
    {
        var textBox = sender as TextBox;
        var dio = textBox?.DataContext as Dio; 
        if (dio == null) return;

        if (int.TryParse(textBox.Text, out int kolicina) && kolicina > 0)
        {
            _izabraniDijeloviKolicine[dio.DioId] = kolicina;
        }
        else
        {
            textBox.Text = "1";
            _izabraniDijeloviKolicine[dio.DioId] = 1;
        }
    }
    private void AzurirajStatusBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_izabranaRezervacija == null)
        {
            var msg = Application.Current.FindResource("Worker_Rezervacije_IzaberiteRezervaciju") as string
                      ?? "Izaberite rezervaciju.";
            MessageBox.Show(msg);
            return;
        }

        var izabraniStatus = StatusCombo.SelectedItem as Status;
        if (izabraniStatus == null)
        {
            var msg = Application.Current.FindResource("Worker_Rezervacije_IzaberiteStatus") as string
                      ?? "Izaberite status.";
            MessageBox.Show(msg);
            return;
        }

        _ = AzurirajStatus(izabraniStatus.StatusId);
    }

    private async Task AzurirajStatus(int noviStatusId)
    {
        await _viewModel.AzurirajStatusRezervacijeAsync(_izabranaRezervacija!.RezervacijaId, noviStatusId);
        var msg = Application.Current.FindResource("Worker_Rezervacije_StatusAzuriran") as string ?? "Status ažuriran!";
        MessageBox.Show(msg);
        await UcitajPodatke();
    }

    private void DodajUslugeBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_izabranaRezervacija == null)
        {
            var msg = Application.Current.FindResource("Worker_Rezervacije_IzaberiteRezervaciju") as string
                      ?? "Izaberite rezervaciju.";
            MessageBox.Show(msg);
            return;
        }

        if (!_izabraneUslugeIds.Any() && !_izabraniDijeloviKolicine.Any())
        {
            var msg = Application.Current.FindResource("Worker_Rezervacije_IzaberiteUsluguIliDio") as string
                      ?? "Izaberite bar jednu uslugu ili dio.";
            MessageBox.Show(msg);
            return;
        }

        var radnik = App.Current.Properties["LoggedInRadnik"] as Radnik;
        if (radnik == null)
        {
            var msg = Application.Current.FindResource("Worker_Rezervacije_NistePrijavljeni") as string
                      ?? "Niste prijavljeni kao radnik.";
            MessageBox.Show(msg);
            return;
        }

        _ = DodajUsluge(_izabraneUslugeIds.ToList(), radnik.RadnikId);
    }

    private async Task DodajUsluge(List<int> tipoviIds, int radnikId)
    {
        try
        {
            await _viewModel.DodajUslugeRezervacijiAsync(
                _izabranaRezervacija!.RezervacijaId,
                tipoviIds,
                radnikId,
                _izabraniDijeloviKolicine
            );
            var msg = Application.Current.FindResource("Worker_Rezervacije_UslugeDodati") as string
                      ?? "Usluge i dijelovi uspešno dodati!";
            MessageBox.Show(msg);
            await UcitajPodatke();
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show($"Greška: {ex.Message}", "Nedovoljno dijelova", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void OmoguciKolicinaBox(Dio dio, bool enabled)
    {
        var container = DijeloviListBox.ItemContainerGenerator.ContainerFromItem(dio);
        if (container is ListBoxItem listBoxItem)
        {
            var textBox = FindChild<TextBox>(listBoxItem, "KolicinaBox");
            textBox?.SetCurrentValue(TextBox.IsEnabledProperty, enabled);
            if (enabled && _izabraniDijeloviKolicine.TryGetValue(dio.DioId, out int kolicina))
            {
                textBox?.SetCurrentValue(TextBox.TextProperty, kolicina.ToString());
            }
        }
    }

    private void AzurirajUkupnuCenu()
    {
        var ukupnoUsluge = _viewModel.IzracunajUkupnuCenu(_izabraneUslugeIds.ToList());
        UkupnaCenaText.Text = $"{ukupnoUsluge:F2} KM";
    }

   
    private static T? FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
    {
        if (parent == null) return null;

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T namedChild && namedChild.GetValue(NameProperty).ToString() == childName)
            {
                return namedChild;
            }

            var childOfChild = FindChild<T>(child, childName);
            if (childOfChild != null) return childOfChild;
        }
        return null;
    }
    private void IzdajRacunBtn_Click(object sender, RoutedEventArgs e)
    {
        if (_izabranaRezervacija == null) return;

        var racunWindow = new Window
        {
            Title = "Kreiraj račun",
            Content = new KreirajRacunView(_izabranaRezervacija.RezervacijaId),
            Width = 600,
            Height = 400,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        racunWindow.ShowDialog();
    }
}