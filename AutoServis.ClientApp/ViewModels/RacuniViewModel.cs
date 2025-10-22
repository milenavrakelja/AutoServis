using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AutoServis.ClientApp.ViewModels;

public class RacuniViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;

    private ObservableCollection<Racun> _racuni = new();
    public ObservableCollection<Racun> Racuni
    {
        get => _racuni;
        set
        {
            _racuni = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEmpty));
        }
    }

    public bool IsEmpty => Racuni == null || Racuni.Count == 0;

    private DateTime? _datum;
    public DateTime? Datum
    {
        get => _datum;
        set
        {
            _datum = value;
            OnPropertyChanged();
        }
    }

    public RacuniViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadAllAsync(int klijentId)
    {
        var racuni = await _context.Racuni
            .Where(r => r.KlijentId == klijentId)
            .ToListAsync();

        Racuni.Clear();
        foreach (var r in racuni)
            Racuni.Add(r);
    }

    public async Task PretraziPoDatumuAsync(int klijentId, DateTime datum)
    {
        var trazeniDatum = DateOnly.FromDateTime(datum);
        var pocetak = trazeniDatum.ToDateTime(TimeOnly.MinValue);
        var kraj = trazeniDatum.ToDateTime(TimeOnly.MaxValue);

        var racuni = await _context.Racuni
            .Where(r => r.KlijentId == klijentId &&
                       r.DatumIzdavanja >= DateOnly.FromDateTime(pocetak) &&
                       r.DatumIzdavanja <= DateOnly.FromDateTime(kraj))
            .ToListAsync();

        Racuni.Clear();
        foreach (var r in racuni)
            Racuni.Add(r);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}