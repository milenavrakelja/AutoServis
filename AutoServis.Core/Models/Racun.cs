namespace AutoServis.Core.Models
{
    public class Racun
    {
        public int RacunId { get; set; }
        public string? BrojRacuna { get; set; }
        public DateOnly? DatumIzdavanja { get; set; }
        public decimal? UkupnaCijena { get; set; }

        public int KlijentId { get; set; }
        public Klijent Klijent { get; set; } = null!;

        public int RezervacijaId { get; set; }
        public Rezervacija Rezervacija { get; set; } = null!;

        public List<StavkaRacuna> StavkeRacuna { get; set; } = new();
    }
}