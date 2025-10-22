using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AutoServis.WorkerApp.ViewModels;

public class TipoviUslugeViewModel : INotifyPropertyChanged
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

    public TipoviUslugeViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadAllAsync()
    {
        var tipovi = await _context.TipoviUsluge.ToListAsync();
        Tipovi = new ObservableCollection<TipUsluge>(tipovi); // 🔑 ključna promjena
    }

    public async Task AddTipAsync(string naziv, decimal cijena)
    {
        _context.TipoviUsluge.Add(new TipUsluge { Naziv = naziv, Cijena = cijena });
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCijenaAsync(int tipId, decimal novaCijena)
    {
        var tip = await _context.TipoviUsluge.FindAsync(tipId);
        if (tip != null)
        {
            tip.Cijena = novaCijena;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTipAsync(int tipId)
    {
        var tip = await _context.TipoviUsluge.FindAsync(tipId);
        if (tip != null)
        {
            _context.TipoviUsluge.Remove(tip);
            await _context.SaveChangesAsync();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}