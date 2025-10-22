using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace AutoServis.WorkerApp.ViewModels;

public class RezervacijeViewModel
{
    private readonly AppDbContext _context;

    public ObservableCollection<Rezervacija> Rezervacije { get; } = new();
    public ObservableCollection<Status> SviStatusi { get; } = new();
    public ObservableCollection<TipUsluge> SviTipoviUsluge { get; } = new();
    public ObservableCollection<Dio> SviDijelovi { get; } = new();

    public RezervacijeViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task UcitajSveAsync()
    {
        var rezervacije = await _context.Rezervacije
            .Include(r => r.Klijent)
            .Include(r => r.Status)
            .ToListAsync();

        Rezervacije.Clear();
        foreach (var r in rezervacije)
            Rezervacije.Add(r);

        var statusi = await _context.Statusi.ToListAsync();
        SviStatusi.Clear();
        foreach (var s in statusi)
            SviStatusi.Add(s);

        var tipovi = await _context.TipoviUsluge.ToListAsync();
        SviTipoviUsluge.Clear();
        foreach (var t in tipovi)
            SviTipoviUsluge.Add(t);

        var dijelovi = await _context.Djelovi.ToListAsync();
        SviDijelovi.Clear();
        foreach (var d in dijelovi)
            SviDijelovi.Add(d);
    }

    public async Task AzurirajStatusRezervacijeAsync(int rezervacijaId, int noviStatusId)
    {
        var rezervacija = await _context.Rezervacije.FindAsync(rezervacijaId);
        if (rezervacija != null)
        {
            rezervacija.StatusId = noviStatusId;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DodajUslugeRezervacijiAsync(int rezervacijaId, List<int> tipoviIds, int radnikId, Dictionary<int, int> dioKolicine)
    {
        // umanji lager za sve dijelove
        foreach (var kvp in dioKolicine)
        {
            var dioId = kvp.Key;
            var kolicina = kvp.Value;

            if (kolicina <= 0) continue;

            var dio = await _context.Djelovi.FindAsync(dioId);
            if (dio == null || dio.KolicinaNaLageru < kolicina)
            {
                throw new InvalidOperationException($"Nema dovoljno dijelova '{dio?.Naziv}' na lageru.");
            }
            dio.KolicinaNaLageru -= kolicina;
        }

        // kreiraj usluge
        var sveUsluge = new List<Usluga>();
        if (!tipoviIds.Any())
        {
            
            sveUsluge.Add(new Usluga
            {
                RezervacijaId = rezervacijaId,
                RadnikId = radnikId,
                StatusId = 1
            });
        }
        else
        {
            foreach (int tipId in tipoviIds)
            {
                sveUsluge.Add(new Usluga
                {
                    RezervacijaId = rezervacijaId,
                    TipUslugeId = tipId,
                    RadnikId = radnikId,
                    StatusId = 1
                });
            }
        }

        _context.Usluge.AddRange(sveUsluge);
        await _context.SaveChangesAsync();

        
        if (dioKolicine.Any())
        {
            var prvaUsluga = sveUsluge.First();
            foreach (var kvp in dioKolicine)
            {
                var dio = await _context.Djelovi.FindAsync(kvp.Key);
                prvaUsluga.Djelovi.Add(dio!);
            }
        }

        await _context.SaveChangesAsync();
    }

    public decimal IzracunajUkupnuCenu(List<int> tipoviUslugeIds)
    {
        if (!tipoviUslugeIds.Any()) return 0;

        var cene = SviTipoviUsluge
            .Where(t => tipoviUslugeIds.Contains(t.TipUslugeId))
            .Select(t => t.Cijena ?? 0)
            .ToList();

        return cene.Sum();
    }
}