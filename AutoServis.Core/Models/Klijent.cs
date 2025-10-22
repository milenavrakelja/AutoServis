using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServis.Core.Models
{
    public class Klijent
    {
        public int KlijentId { get; set; }
        public string? Kontakt { get; set; }
        public string? Adresa { get; set; }
        public string? KorisnickoIme { get; set; }
        public string? Lozinka { get; set; }
        public string SelectedTheme { get; set; }

        [NotMapped] 
        public string LozinkaPlain { get; set; } = string.Empty;
        // Navigation properties
        public List<Vozilo> Vozila { get; set; } = new();
        public List<Rezervacija> Rezervacije { get; set; } = new();
    }
}