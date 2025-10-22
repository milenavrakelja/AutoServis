using AutoServis.Core;
using AutoServis.Core.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoServis.WorkerApp.ViewModels;

public class KlijentiViewModel : INotifyPropertyChanged
{
    private readonly IServiceProvider _serviceProvider;

    private ObservableCollection<Klijent> _klijenti = new();
    public ObservableCollection<Klijent> Klijenti
    {
        get => _klijenti;
        set
        {
            _klijenti = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEmpty));
        }
    }

    public bool IsEmpty => Klijenti == null || Klijenti.Count == 0;

    public KlijentiViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task LoadKlijenteAsync()
    {
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var klijenti = await context.Klijenti.ToListAsync();

        // Postavljamo privremenu lozinku za prikaz
        foreach (var k in klijenti)
        {
            k.LozinkaPlain = "****";
        }

        Klijenti = new ObservableCollection<Klijent>(klijenti); // 🔑 ključna promjena
    }

    public async Task DodajKlijentaAsync(string korisnickoIme, string lozinka, string? kontakt, string? adresa)
    {
        if (string.IsNullOrWhiteSpace(korisnickoIme))
            throw new ArgumentException("Korisničko ime je obavezno.");
        if (string.IsNullOrWhiteSpace(lozinka))
            throw new ArgumentException("Lozinka je obavezna.");

        var hashed = BC.HashPassword(lozinka);
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        context.Klijenti.Add(new Klijent
        {
            KorisnickoIme = korisnickoIme,
            Lozinka = hashed,
            Kontakt = kontakt,
            Adresa = adresa,
            SelectedTheme = "DefaultBlue"
        });
        await context.SaveChangesAsync();
    }

    public async Task IzmijeniKlijentaAsync(int klijentId, string korisnickoIme, string? novaLozinka, string? kontakt, string? adresa)
    {
        if (string.IsNullOrWhiteSpace(korisnickoIme))
            throw new ArgumentException("Korisničko ime je obavezno.");

        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var klijent = await context.Klijenti.FindAsync(klijentId);
        if (klijent != null)
        {
            klijent.KorisnickoIme = korisnickoIme.Trim();
            klijent.Kontakt = string.IsNullOrWhiteSpace(kontakt) ? null : kontakt.Trim();
            klijent.Adresa = string.IsNullOrWhiteSpace(adresa) ? null : adresa.Trim();

            if (!string.IsNullOrWhiteSpace(novaLozinka))
            {
                klijent.Lozinka = BC.HashPassword(novaLozinka);
            }

            await context.SaveChangesAsync();
        }
    }

    public async Task ObrisiKlijentaAsync(int klijentId)
    {
        using var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var klijent = await context.Klijenti.FindAsync(klijentId);
        if (klijent != null)
        {
            context.Klijenti.Remove(klijent);
            await context.SaveChangesAsync();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}