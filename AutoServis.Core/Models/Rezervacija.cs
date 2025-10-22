namespace AutoServis.Core.Models
{
    public class Rezervacija
    {
        public int RezervacijaId { get; set; }
        public DateOnly? DatumRezervacije { get; set; }
        public DateOnly? DatumUsluge { get; set; }
        public string? OpisProblema { get; set; }

        public int KlijentId { get; set; }
        public Klijent Klijent { get; set; } = null!;

        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public List<Usluga> Usluge { get; set; } = new();
        public List<Racun> Racuni { get; set; } = new();
    }
}