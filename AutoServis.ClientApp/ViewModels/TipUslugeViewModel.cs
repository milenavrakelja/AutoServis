using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AutoServis.ClientApp.ViewModels;

public class TipUslugeViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;

    private ObservableCollection<TipUsluge> _tipovi = new();
    public ObservableCollection<TipUsluge> Tipovi
    {
        get => _tipovi;
        set
        {
            _tipovi = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEmpty));
        }
    }

    public bool IsEmpty => Tipovi == null || Tipovi.Count == 0;

    public TipUslugeViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadAllAsync()
    {
        var tipovi = await _context.TipoviUsluge.ToListAsync();
        Tipovi = new ObservableCollection<TipUsluge>(tipovi);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}