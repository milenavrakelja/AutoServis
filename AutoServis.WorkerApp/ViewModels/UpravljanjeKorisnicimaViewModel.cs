using AutoServis.Core;
using AutoServis.Core.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;

namespace AutoServis.WorkerApp.ViewModels;

public class UpravljanjeKorisnicimaViewModel : INotifyPropertyChanged
{
    private readonly IServiceProvider _serviceProvider;

    public ObservableCollection<Radnik> Radnici { get; } = new();

    public UpravljanjeKorisnicimaViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task LoadRadnikeAsync()
    {
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var radnici = await context.Radnici.ToListAsync();
        Radnici.Clear();
        foreach (var r in radnici)
        {
            
            r.LozinkaPlain = "****"; 
                                     
                                     
            Radnici.Add(r);
        }
    }

    public async Task DodajRadnikaAsync(string ime, string? prezime, string korisnickoIme, string lozinka, string? kontakt, string uloga)
    {
        if (string.IsNullOrWhiteSpace(ime))
            throw new ArgumentException("Ime je obavezno.");
        if (string.IsNullOrWhiteSpace(korisnickoIme))
            throw new ArgumentException("Korisničko ime je obavezno.");
        if (string.IsNullOrWhiteSpace(lozinka))
            throw new ArgumentException("Lozinka je obavezna.");

        var hashed = BC.HashPassword(lozinka);
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        context.Radnici.Add(new Radnik
        {
            Ime = ime.Trim(),
            Prezime = string.IsNullOrWhiteSpace(prezime) ? null : prezime.Trim(),
            KorisnickoIme = korisnickoIme.Trim(),
            Lozinka = hashed,
            Kontakt = string.IsNullOrWhiteSpace(kontakt) ? null : kontakt.Trim(),
            Uloga = uloga ?? "Radnik",
            SelectedTheme = "DefaultBlue"
        });
        await context.SaveChangesAsync();
    }

    public async Task IzmijeniRadnikaAsync(int radnikId, string ime, string? prezime, string korisnickoIme, string? novaLozinka, string? kontakt, string uloga)
    {
        if (string.IsNullOrWhiteSpace(ime))
            throw new ArgumentException("Ime je obavezno.");
        if (string.IsNullOrWhiteSpace(korisnickoIme))
            throw new ArgumentException("Korisničko ime je obavezno.");

        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var radnik = await context.Radnici.FindAsync(radnikId);
        if (radnik == null)
            throw new InvalidOperationException("Radnik nije pronađen.");

        radnik.Ime = ime.Trim();
        radnik.Prezime = string.IsNullOrWhiteSpace(prezime) ? null : prezime.Trim();
        radnik.KorisnickoIme = korisnickoIme.Trim();
        radnik.Kontakt = string.IsNullOrWhiteSpace(kontakt) ? null : kontakt.Trim();
        radnik.Uloga = uloga ?? "Radnik";

        if (!string.IsNullOrWhiteSpace(novaLozinka))
        {
            radnik.Lozinka = BC.HashPassword(novaLozinka);
        }

        context.Radnici.Update(radnik);
        await context.SaveChangesAsync();
    }

    public async Task PromeniUloguAsync(int radnikId, string novaUloga)
    {
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var radnik = await context.Radnici.FindAsync(radnikId);
        if (radnik != null)
        {
            radnik.Uloga = novaUloga;
            await context.SaveChangesAsync();
        }
    }

    public async Task ObrisiRadnikaAsync(int radnikId)
    {
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var radnik = await context.Radnici.FindAsync(radnikId);
        if (radnik != null)
        {
            context.Radnici.Remove(radnik);
            await context.SaveChangesAsync();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}