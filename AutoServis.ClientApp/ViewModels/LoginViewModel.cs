using AutoServis.Core;
using AutoServis.Core.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoServis.ClientApp.ViewModels;

public class LoginViewModel
{
    private readonly AppDbContext _context;

    public LoginViewModel(AppDbContext context)
    {
        _context = context;
    }
    

    public async Task<Klijent?> LoginAsync(string korisnickoIme, string unesenaLozinka)
    {
        // Učitaj klijenta po korisničkom imenu
        var klijent = await _context.Klijenti
            .FirstOrDefaultAsync(k => k.KorisnickoIme == korisnickoIme);

        if (klijent == null)
            return null;

        // Proveri da li unesena lozinka odgovara hešu
        bool ispravna = BC.Verify(unesenaLozinka, klijent.Lozinka);
        return ispravna ? klijent : null;
    }
}