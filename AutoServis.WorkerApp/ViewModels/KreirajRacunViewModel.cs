using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace AutoServis.WorkerApp.ViewModels;

public class KreirajRacunViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;
    private Rezervacija? _rezervacija;

    public Rezervacija? Rezervacija
    {
        get => _rezervacija;
        private set
        {
            _rezervacija = value;
            OnPropertyChanged();
        }
    }

    private decimal _ukupnaCijena;
    public decimal UkupnaCijena
    {
        get => _ukupnaCijena;
        private set
        {
            _ukupnaCijena = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<StavkaRacuna> Stavke { get; } = new();

    public KreirajRacunViewModel(AppDbContext context)
    {
        _context = context;
        Stavke.CollectionChanged += OnStavkeChanged;
    }

    private void OnStavkeChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        
        UkupnaCijena = Stavke.Sum(s => s.Ukupno) ?? 0m;
    }

    public async Task UcitajRezervacijuAsync(int rezervacijaId)
    {
        var rez = await _context.Rezervacije
            .Include(r => r.Klijent)
            .Include(r => r.Usluge)
                .ThenInclude(u => u.TipUsluge)
            .FirstAsync(r => r.RezervacijaId == rezervacijaId);

        Rezervacija = rez;

        Stavke.Clear();
        foreach (var usluga in Rezervacija.Usluge)
        {
            if (usluga.TipUsluge != null)
            {
                var cijena = usluga.TipUsluge.Cijena ?? 0;
                Stavke.Add(new StavkaRacuna
                {
                    UslugaId = usluga.UslugaId,
                    Usluga = usluga,
                    CijenaPoJedinici = cijena,
                    Kolicina = 1,
                    Ukupno = cijena
                });
            }
        }
        
    }

    public async Task<Racun> KreirajRacunAsync()
    {
        if (Rezervacija == null)
            throw new InvalidOperationException("Rezervacija nije učitana.");

        var racun = new Racun
        {
            BrojRacuna = $"RAC-{DateTime.Now:yyyyMMdd}-{Rezervacija.RezervacijaId}",
            DatumIzdavanja = DateOnly.FromDateTime(DateTime.Now),
            KlijentId = Rezervacija.KlijentId,
            RezervacijaId = Rezervacija.RezervacijaId,
            UkupnaCijena = UkupnaCijena 
        };

        _context.Racuni.Add(racun);
        await _context.SaveChangesAsync();

        foreach (var stavka in Stavke)
        {
            _context.StavkeRacuna.Add(new StavkaRacuna
            {
                RacunId = racun.RacunId,
                UslugaId = stavka.UslugaId,
                CijenaPoJedinici = stavka.CijenaPoJedinici,
                Kolicina = stavka.Kolicina,
                Ukupno = stavka.Ukupno
            });
        }

        await _context.SaveChangesAsync();
        return racun;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}