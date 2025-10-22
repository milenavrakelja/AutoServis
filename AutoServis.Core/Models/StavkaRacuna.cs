using AutoServis.Core.Models;

public class StavkaRacuna
{
    public int StavkaRacunaId { get; set; } 
    public int Kolicina { get; set; } = 1;
    public decimal? CijenaPoJedinici { get; set; }
    public decimal? Ukupno { get; set; }

    public int RacunId { get; set; }
    public Racun Racun { get; set; } = null!;

    public int UslugaId { get; set; }
    public Usluga Usluga { get; set; } = null!;
}