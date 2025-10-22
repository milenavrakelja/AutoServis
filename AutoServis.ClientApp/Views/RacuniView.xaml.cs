using AutoServis.ClientApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoServis.ClientApp.Views;

public partial class RacuniView : UserControl
{
    private readonly RacuniViewModel _viewModel;

    public RacuniView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<RacuniViewModel>();
        DataContext = _viewModel; // 🔑 Obavezno za binding
        Loaded += async (s, e) => await LoadAllAsyncSafe();
    }

    private async Task LoadAllAsyncSafe()
    {
        if (App.LoggedInKlijentId == null)
        {
            var msg = Application.Current.FindResource("Client_Racuni_NistePrijavljeni") as string
                      ?? "Niste prijavljeni kao klijent.";
            MessageBox.Show(msg);
            return;
        }

        await _viewModel.LoadAllAsync(App.LoggedInKlijentId.Value);
        // ❌ NIKAKO NE POSTAVLJAJ RacuniGrid.ItemsSource RUČNO!
    }

    private async void PretraziBtn_Click(object sender, RoutedEventArgs e)
    {
        if (App.LoggedInKlijentId == null)
        {
            var msg = Application.Current.FindResource("Client_Racuni_NistePrijavljeni") as string
                      ?? "Niste prijavljeni kao klijent.";
            MessageBox.Show(msg);
            return;
        }

        if (DatePicker.SelectedDate.HasValue)
        {
            await _viewModel.PretraziPoDatumuAsync(
                klijentId: App.LoggedInKlijentId.Value,
                datum: DatePicker.SelectedDate.Value
            );
            // ❌ NIKAKO NE POSTAVLJAJ RacuniGrid.ItemsSource RUČNO!
        }
        else
        {
            var msg = Application.Current.FindResource("Client_Racuni_IzaberiteDatum") as string
                      ?? "Izaberite datum za pretragu.";
            MessageBox.Show(msg);
        }
    }

    private async void PrikaziSveBtn_Click(object sender, RoutedEventArgs e)
    {
        DatePicker.SelectedDate = null;
        _viewModel.Datum = null; // sinhronizacija sa ViewModelom
        await LoadAllAsyncSafe();
    }
}