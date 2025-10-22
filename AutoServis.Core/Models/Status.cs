namespace AutoServis.Core.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string? Opis { get; set; }

        public List<Rezervacija> Rezervacije { get; set; } = new();
        public List<Usluga> Usluge { get; set; } = new();
    }
}