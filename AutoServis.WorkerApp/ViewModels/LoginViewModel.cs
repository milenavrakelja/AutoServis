using AutoServis.Core;
using AutoServis.Core.Models;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoServis.WorkerApp.ViewModels;

public class LoginViewModel
{
    private readonly AppDbContext _context;

    public LoginViewModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Radnik?> LoginAsync(string korisnickoIme, string unesenaLozinka)
    {
        // Učitaj radnika po korisničkom imenu
        var radnik = await _context.Radnici
            .FirstOrDefaultAsync(r => r.KorisnickoIme == korisnickoIme);

        if (radnik == null) return null;

        // Proveri da li unesena lozinka odgovara hešu
        bool ispravna = BC.Verify(unesenaLozinka, radnik.Lozinka);
        return ispravna ? radnik : null;
    }
}