using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AutoServis.WorkerApp.ViewModels;

public class DijeloviLagerViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;

    private ObservableCollection<Dio> _dijelovi = new();
    public ObservableCollection<Dio> Dijelovi
    {
        get => _dijelovi;
        set
        {
            _dijelovi = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEmpty));
        }
    }

    public bool IsEmpty => Dijelovi == null || Dijelovi.Count == 0;

    public DijeloviLagerViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadAllAsync()
    {
        var dijelovi = await _context.Djelovi.ToListAsync();
        Dijelovi = new ObservableCollection<Dio>(dijelovi); // 🔑 ključna promjena
    }

    public async Task DodajIliObnoviDioAsync(string naziv, int kolicina)
    {
        var postojeci = await _context.Djelovi
            .FirstOrDefaultAsync(d => d.Naziv == naziv);

        if (postojeci != null)
        {
            postojeci.KolicinaNaLageru = kolicina;
        }
        else
        {
            _context.Djelovi.Add(new Dio { Naziv = naziv, KolicinaNaLageru = kolicina });
        }

        await _context.SaveChangesAsync();
    }

    public async Task UvecajKolicinuAsync(int dioId, int iznos)
    {
        var dio = await _context.Djelovi.FindAsync(dioId);
        if (dio != null)
        {
            dio.KolicinaNaLageru += iznos;
            await _context.SaveChangesAsync();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}