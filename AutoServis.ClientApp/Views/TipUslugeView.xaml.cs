using AutoServis.ClientApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace AutoServis.ClientApp.Views;

public partial class TipUslugeView : UserControl
{
    private readonly TipUslugeViewModel _viewModel;

    public TipUslugeView()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<TipUslugeViewModel>();
        DataContext = _viewModel;
        Loaded += async (s, e) => await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        await _viewModel.LoadAllAsync();
    }
}