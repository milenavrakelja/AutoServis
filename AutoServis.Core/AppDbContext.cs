using Microsoft.EntityFrameworkCore;
using AutoServis.Core.Models;

namespace AutoServis.Core
{
    public class AppDbContext : DbContext
    {
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<Status> Statusi { get; set; }
        public DbSet<TipUsluge> TipoviUsluge { get; set; }
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<Usluga> Usluge { get; set; }
        public DbSet<Dio> Djelovi { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<StavkaRacuna> StavkeRacuna { get; set; }

        // konstruktor za DI
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationHelper.GetConnectionString();
                optionsBuilder.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many
            modelBuilder.Entity<Dio>()
                .HasMany(d => d.Usluge)
                .WithMany(u => u.Djelovi)
                .UsingEntity(j => j.ToTable("DIO_has_USLUGA"));

            // Ograničenje za ulogu
            modelBuilder.Entity<Radnik>()
                .HasCheckConstraint("CK_Radnik_Uloga", "Uloga IN ('Radnik', 'Administrator')");
        }
    }
}