using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServis.Core.Models
{
    public class Radnik
    {
        public int RadnikId { get; set; }
        [Required] public string Ime { get; set; } = string.Empty;
        public string? Prezime { get; set; }
        public string? Kontakt { get; set; }
        public string SelectedTheme { get; set; } 

        [Required, MaxLength(45)] public string KorisnickoIme { get; set; } = string.Empty;
        [Required, MaxLength(100)] public string Lozinka { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string Uloga { get; set; } = "Radnik";

        [NotMapped] 
        public string LozinkaPlain { get; set; } = string.Empty;

        public List<Usluga> Usluge { get; set; } = new();
    }
}