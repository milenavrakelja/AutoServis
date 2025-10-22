using AutoServis.Core;
using AutoServis.Core.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoServis.WorkerApp.ViewModels;

public class RegisterViewModel
{
    private readonly AppDbContext _context;

    public RegisterViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> RegisterAsync(
        string ime, string? prezime, string? kontakt, string korisnickoIme, string lozinka)
    {
        // Provera da li korisnik već postoji
        if (await _context.Radnici.AnyAsync(r => r.KorisnickoIme == korisnickoIme))
        {
            return (false, "Korisničko ime već postoji.");
        }

        // Heširanje lozinke
        string hashedLozinka = BC.HashPassword(lozinka);

        // Kreiranje novog radnika
        var noviRadnik = new Radnik
        {
            Ime = ime,
            Prezime = string.IsNullOrEmpty(prezime) ? null : prezime,
            Kontakt = string.IsNullOrEmpty(kontakt) ? null : kontakt,
            KorisnickoIme = korisnickoIme,
            Lozinka = hashedLozinka,
            Uloga = "Radnik" // Default uloga
        };

        _context.Radnici.Add(noviRadnik);
        await _context.SaveChangesAsync();

        return (true, "Uspešna registracija!");
    }
}