namespace AutoServis.Core.Models
{
    public class Usluga
    {
        public int UslugaId { get; set; }

        public int RezervacijaId { get; set; }
        public Rezervacija Rezervacija { get; set; } = null!;

        public int TipUslugeId { get; set; }
        public TipUsluge TipUsluge { get; set; } = null!;

        public int RadnikId { get; set; }
        public Radnik Radnik { get; set; } = null!;

        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public List<Dio> Djelovi { get; set; } = new();
        public List<StavkaRacuna> StavkeRacuna { get; set; } = new();
    }
}