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

public class RezervacijeViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;

    private ObservableCollection<Rezervacija> _rezervacije = new();
    public ObservableCollection<Rezervacija> Rezervacije
    {
        get => _rezervacije;
        set
        {
            _rezervacije = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEmpty));
        }
    }

    public bool IsEmpty => Rezervacije == null || Rezervacije.Count == 0;

    public RezervacijeViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task LoadRezervacijeAsync(int klijentId)
    {
        var rezervacije = await _context.Rezervacije
            .Where(r => r.KlijentId == klijentId)
            .Include(r => r.Status)
            .ToListAsync();

        Rezervacije = new ObservableCollection<Rezervacija>(rezervacije); // 🔑 ključna promjena
    }

    public async Task KreirajRezervacijuAsync(int klijentId, string model, int godina, string opis, DateOnly datumUsluge)
    {
        var vozilo = await _context.Vozila
            .FirstOrDefaultAsync(v => v.KlijentId == klijentId && v.Model == model && v.GodinaProizvodnje == godina);

        if (vozilo == null)
        {
            vozilo = new Vozilo
            {
                Model = model,
                GodinaProizvodnje = godina,
                KlijentId = klijentId
            };
            _context.Vozila.Add(vozilo);
            await _context.SaveChangesAsync();
        }

        var rezervacija = new Rezervacija
        {
            KlijentId = klijentId,
            OpisProblema = opis,
            DatumRezervacije = DateOnly.FromDateTime(DateTime.Today),
            DatumUsluge = datumUsluge,
            StatusId = 1
        };

        _context.Rezervacije.Add(rezervacija);
        await _context.SaveChangesAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}