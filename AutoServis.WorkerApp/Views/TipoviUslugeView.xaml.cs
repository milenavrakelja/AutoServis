using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core.Models;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AutoServis.WorkerApp.Views;

public partial class TipoviUslugeView : UserControl
{
    private readonly TipoviUslugeViewModel _viewModel;

    public TipoviUslugeView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<TipoviUslugeViewModel>();
        DataContext = _viewModel; // 🔑 obavezno za binding
        LoadData();
    }

    private async void LoadData()
    {
        await _viewModel.LoadAllAsync();
        // ❌ NIKAKO NE POSTAVLJAJ TipoviGrid.ItemsSource RUČNO!
    }

    private async void DodajBtn_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NazivBox.Text) || string.IsNullOrWhiteSpace(CijenaBox.Text))
        {
            var msg = Application.Current.FindResource("Worker_Tipovi_UnesiteNazivICijenu") as string
                      ?? "Unesite naziv i cenu.";
            MessageBox.Show(msg);
            return;
        }

        if (!decimal.TryParse(CijenaBox.Text, out decimal cijena))
        {
            var msg = Application.Current.FindResource("Worker_Tipovi_ValidnaCijena") as string
                      ?? "Unesite validnu cenu.";
            MessageBox.Show(msg);
            return;
        }

        await _viewModel.AddTipAsync(NazivBox.Text.Trim(), cijena);
        LoadData();
        NazivBox.Clear();
        CijenaBox.Clear();
    }

    private async void IzmeniCenuBtn_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var tip = button?.Tag as TipUsluge;
        if (tip == null) return;

        var prompt = Application.Current.FindResource("Worker_Tipovi_NovaCijenaPrompt") as string ?? "Nova cijena:";
        var title = Application.Current.FindResource("Worker_Tipovi_IzmjenaCeneTitle") as string ?? "Izmjena cene";

        var novaCijenaStr = Microsoft.VisualBasic.Interaction.InputBox(prompt, title, tip.Cijena?.ToString() ?? "0");
        if (decimal.TryParse(novaCijenaStr, out decimal novaCijena))
        {
            await _viewModel.UpdateCijenaAsync(tip.TipUslugeId, novaCijena);
            LoadData();
        }
    }

    private async void ObrisiBtn_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var tip = button?.Tag as TipUsluge;
        if (tip == null) return;

        var format = Application.Current.FindResource("Worker_Tipovi_PotvrdaBrisanja") as string
                     ?? "Da li želite da obrišete tip usluge '{0}'?";
        var message = string.Format(format, tip.Naziv);
        var title = Application.Current.FindResource("Worker_Tipovi_PotvrdaTitle") as string ?? "Potvrda";

        var result = MessageBox.Show(message, title, MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            await _viewModel.DeleteTipAsync(tip.TipUslugeId);
            LoadData();
        }
    }

    private async void OsveziBtn_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }
}