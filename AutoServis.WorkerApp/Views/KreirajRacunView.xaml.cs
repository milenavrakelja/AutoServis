using AutoServis.WorkerApp.ViewModels;
using System.Windows.Controls;
using System.Windows;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace AutoServis.WorkerApp.Views; 

public partial class KreirajRacunView : UserControl
{
    private readonly KreirajRacunViewModel _viewModel;

    public KreirajRacunView(int rezervacijaId)
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<KreirajRacunViewModel>();
        DataContext = _viewModel;
        Loaded += async (s, e) => await _viewModel.UcitajRezervacijuAsync(rezervacijaId);
    }
    private async void IzdajRacunBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await _viewModel.KreirajRacunAsync();
            var msg = Application.Current.FindResource("Worker_Racun_RacunIzdat") as string ?? "Račun uspešno izdat!";
            var title = Application.Current.FindResource("Worker_Racun_UspehTitle") as string ?? "Uspeh";
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            var title = Application.Current.FindResource("Worker_Racun_GreskaTitle") as string ?? "Greška";
            MessageBox.Show($"Greška: {ex.Message}", title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}