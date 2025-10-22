using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core.Models;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AutoServis.WorkerApp.Views;

public partial class DijeloviLagerView : UserControl
{
    private readonly DijeloviLagerViewModel _viewModel;

    public DijeloviLagerView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<DijeloviLagerViewModel>();
        DataContext = _viewModel; // 🔑 obavezno za binding
        LoadData();
    }

    private async void LoadData()
    {
        await _viewModel.LoadAllAsync();
        // ❌ NIKAKO NE POSTAVLJAJ DijeloviGrid.ItemsSource RUČNO!
    }

    private async void DodajBtn_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NazivBox.Text) || string.IsNullOrWhiteSpace(KolicinaBox.Text))
        {
            var msg = Application.Current.FindResource("Worker_Dijelovi_UnesiteNazivIKolicinu") as string
                      ?? "Unesite naziv i količinu.";
            MessageBox.Show(msg);
            return;
        }

        if (!int.TryParse(KolicinaBox.Text, out int kolicina) || kolicina < 0)
        {
            var msg = Application.Current.FindResource("Worker_Dijelovi_ValidnaKolicina") as string
                      ?? "Unesite validnu količinu (broj ≥ 0).";
            MessageBox.Show(msg);
            return;
        }

        await _viewModel.DodajIliObnoviDioAsync(NazivBox.Text.Trim(), kolicina);
        LoadData();
        NazivBox.Clear();
        KolicinaBox.Clear();
    }

    private async void UvecajBtn_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var dio = button?.Tag as Dio;
        if (dio == null) return;

        await _viewModel.UvecajKolicinuAsync(dio.DioId, 1);
        LoadData();
    }
}