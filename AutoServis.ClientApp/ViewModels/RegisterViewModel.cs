using AutoServis.Core;
using AutoServis.Core.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoServis.ClientApp.ViewModels;

public class RegisterViewModel
{
    private readonly AppDbContext _context;

    public RegisterViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> RegisterAsync(
        string korisnickoIme, string lozinka, string kontakt, string adresa)
    {
        if (await _context.Klijenti.AnyAsync(k => k.KorisnickoIme == korisnickoIme))
            return (false, "Korisničko ime već postoji.");

        var hashed = BC.HashPassword(lozinka);
        _context.Klijenti.Add(new Klijent
        {
            KorisnickoIme = korisnickoIme,
            Lozinka = hashed,
            Kontakt = kontakt,
            Adresa = adresa
        });

        await _context.SaveChangesAsync();
        return (true, "Uspješna registracija!");
    }
}