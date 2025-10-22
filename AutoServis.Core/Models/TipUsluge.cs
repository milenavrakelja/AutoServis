namespace AutoServis.Core.Models
{
    public class TipUsluge
    {
        public int TipUslugeId { get; set; }
        public decimal? Cijena { get; set; }
        public string? Naziv { get; set; }

        public List<Usluga> Usluge { get; set; } = new();
    }
}