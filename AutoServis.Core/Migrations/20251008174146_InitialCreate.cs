using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServis.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Djelovi",
                columns: table => new
                {
                    DioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KolicinaNaLageru = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Djelovi", x => x.DioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    KlijentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kontakt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adresa = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KorisnickoIme = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lozinka = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.KlijentID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Radnici",
                columns: table => new
                {
                    RadnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prezime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kontakt = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnici", x => x.RadnikId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Statusi",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusi", x => x.StatusId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoviUsluge",
                columns: table => new
                {
                    TipUslugeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cijena = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Naziv = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviUsluge", x => x.TipUslugeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vozila",
                columns: table => new
                {
                    VoziloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GodinaProizvodnje = table.Column<int>(type: "int", nullable: true),
                    KlijentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozila", x => x.VoziloID);
                    table.ForeignKey(
                        name: "FK_Vozila_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    RezervacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DatumRezervacije = table.Column<DateOnly>(type: "date", nullable: true),
                    DatumUsluge = table.Column<DateOnly>(type: "date", nullable: true),
                    OpisProblema = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KlijentID = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.RezervacijaID);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Statusi_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statusi",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    RacunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrojRacuna = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DatumIzdavanja = table.Column<DateOnly>(type: "date", nullable: true),
                    UkupnaCijena = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    KlijentID = table.Column<int>(type: "int", nullable: false),
                    RezervacijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.RacunID);
                    table.ForeignKey(
                        name: "FK_Racuni_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racuni_Rezervacije_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacije",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usluge",
                columns: table => new
                {
                    UslugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrojRadnihSati = table.Column<int>(type: "int", nullable: true),
                    RezervacijaID = table.Column<int>(type: "int", nullable: false),
                    TipID = table.Column<int>(type: "int", nullable: false),
                    TipUslugeID = table.Column<int>(type: "int", nullable: false),
                    RadnikId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluge", x => x.UslugaId);
                    table.ForeignKey(
                        name: "FK_Usluge_Radnici_RadnikId",
                        column: x => x.RadnikId,
                        principalTable: "Radnici",
                        principalColumn: "RadnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usluge_Rezervacije_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacije",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usluge_Statusi_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statusi",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usluge_TipoviUsluge_TipUslugeID",
                        column: x => x.TipUslugeID,
                        principalTable: "TipoviUsluge",
                        principalColumn: "TipUslugeID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DIO_has_USLUGA",
                columns: table => new
                {
                    DjeloviDioId = table.Column<int>(type: "int", nullable: false),
                    UslugeUslugaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIO_has_USLUGA", x => new { x.DjeloviDioId, x.UslugeUslugaId });
                    table.ForeignKey(
                        name: "FK_DIO_has_USLUGA_Djelovi_DjeloviDioId",
                        column: x => x.DjeloviDioId,
                        principalTable: "Djelovi",
                        principalColumn: "DioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DIO_has_USLUGA_Usluge_UslugeUslugaId",
                        column: x => x.UslugeUslugaId,
                        principalTable: "Usluge",
                        principalColumn: "UslugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StavkeRacuna",
                columns: table => new
                {
                    StavkaRacunaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    CijenaPoJedinici = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Ukupno = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    RacunID = table.Column<int>(type: "int", nullable: false),
                    UslugaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeRacuna", x => x.StavkaRacunaId);
                    table.ForeignKey(
                        name: "FK_StavkeRacuna_Racuni_RacunID",
                        column: x => x.RacunID,
                        principalTable: "Racuni",
                        principalColumn: "RacunID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeRacuna_Usluge_UslugaId",
                        column: x => x.UslugaId,
                        principalTable: "Usluge",
                        principalColumn: "UslugaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DIO_has_USLUGA_UslugeUslugaId",
                table: "DIO_has_USLUGA",
                column: "UslugeUslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_KlijentID",
                table: "Racuni",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_RezervacijaID",
                table: "Racuni",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_KlijentID",
                table: "Rezervacije",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_StatusId",
                table: "Rezervacije",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRacuna_RacunID",
                table: "StavkeRacuna",
                column: "RacunID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRacuna_UslugaId",
                table: "StavkeRacuna",
                column: "UslugaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_RadnikId",
                table: "Usluge",
                column: "RadnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_RezervacijaID",
                table: "Usluge",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_StatusId",
                table: "Usluge",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_TipUslugeID",
                table: "Usluge",
                column: "TipUslugeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozila_KlijentID",
                table: "Vozila",
                column: "KlijentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DIO_has_USLUGA");

            migrationBuilder.DropTable(
                name: "StavkeRacuna");

            migrationBuilder.DropTable(
                name: "Vozila");

            migrationBuilder.DropTable(
                name: "Djelovi");

            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Usluge");

            migrationBuilder.DropTable(
                name: "Radnici");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "TipoviUsluge");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Statusi");
        }
    }
}
