namespace AutoServis.Core.Models
{
    public class Dio
    {
        public int DioId { get; set; }
        public string? Naziv { get; set; }
        public int? KolicinaNaLageru { get; set; }

        public List<Usluga> Usluge { get; set; } = new();
    }
}