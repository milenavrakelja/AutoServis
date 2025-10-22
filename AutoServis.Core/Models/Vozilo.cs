namespace AutoServis.Core.Models
{
    public class Vozilo
    {
        public int VoziloId { get; set; }
        public string? Model { get; set; }
        public int? GodinaProizvodnje { get; set; }

        // Foreign key
        public int KlijentId { get; set; }
        public Klijent Klijent { get; set; } = null!;
    }
}