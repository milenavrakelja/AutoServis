using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServis.Core.Migrations
{
    /// <inheritdoc />
    public partial class DodajKorisnickoImeLozinkuIUloguURadnika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Klijenti_KlijentID",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaID",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Klijenti_KlijentID",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Racuni_RacunID",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_Radnici_RadnikId",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_Rezervacije_RezervacijaID",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipUslugeID",
                table: "Usluge");

            migrationBuilder.AlterColumn<int>(
                name: "TipUslugeID",
                table: "Usluge",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UslugaId1",
                table: "StavkeRacuna",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId1",
                table: "Rezervacije",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "Radnici",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Lozinka",
                table: "Radnici",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Uloga",
                table: "Radnici",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "RezervacijaID1",
                table: "Racuni",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Radnici",
                columns: new[] { "RadnikId", "Ime", "Kontakt", "KorisnickoIme", "Lozinka", "Prezime", "Uloga" },
                values: new object[] { 1, "Admin", "admin@autoservis.ba", "admin", "admin123", "Sistem", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_Usluge_TipID",
                table: "Usluge",
                column: "TipID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRacuna_UslugaId1",
                table: "StavkeRacuna",
                column: "UslugaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_StatusId1",
                table: "Rezervacije",
                column: "StatusId1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Radnik_Uloga",
                table: "Radnici",
                sql: "Uloga IN ('Radnik', 'Administrator')");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_RezervacijaID1",
                table: "Racuni",
                column: "RezervacijaID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Klijenti_KlijentID",
                table: "Racuni",
                column: "KlijentID",
                principalTable: "Klijenti",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaID",
                table: "Racuni",
                column: "RezervacijaID",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaID1",
                table: "Racuni",
                column: "RezervacijaID1",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Klijenti_KlijentID",
                table: "Rezervacije",
                column: "KlijentID",
                principalTable: "Klijenti",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId",
                table: "Rezervacije",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId1",
                table: "Rezervacije",
                column: "StatusId1",
                principalTable: "Statusi",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeRacuna_Racuni_RacunID",
                table: "StavkeRacuna",
                column: "RacunID",
                principalTable: "Racuni",
                principalColumn: "RacunID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId",
                table: "StavkeRacuna",
                column: "UslugaId",
                principalTable: "Usluge",
                principalColumn: "UslugaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId1",
                table: "StavkeRacuna",
                column: "UslugaId1",
                principalTable: "Usluge",
                principalColumn: "UslugaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_Radnici_RadnikId",
                table: "Usluge",
                column: "RadnikId",
                principalTable: "Radnici",
                principalColumn: "RadnikId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_Rezervacije_RezervacijaID",
                table: "Usluge",
                column: "RezervacijaID",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipID",
                table: "Usluge",
                column: "TipID",
                principalTable: "TipoviUsluge",
                principalColumn: "TipUslugeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipUslugeID",
                table: "Usluge",
                column: "TipUslugeID",
                principalTable: "TipoviUsluge",
                principalColumn: "TipUslugeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Klijenti_KlijentID",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaID",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaID1",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Klijenti_KlijentID",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId1",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Racuni_RacunID",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId1",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_Radnici_RadnikId",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_Rezervacije_RezervacijaID",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipID",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipUslugeID",
                table: "Usluge");

            migrationBuilder.DropIndex(
                name: "IX_Usluge_TipID",
                table: "Usluge");

            migrationBuilder.DropIndex(
                name: "IX_StavkeRacuna_UslugaId1",
                table: "StavkeRacuna");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_StatusId1",
                table: "Rezervacije");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Radnik_Uloga",
                table: "Radnici");

            migrationBuilder.DropIndex(
                name: "IX_Racuni_RezervacijaID1",
                table: "Racuni");

            migrationBuilder.DeleteData(
                table: "Radnici",
                keyColumn: "RadnikId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "UslugaId1",
                table: "StavkeRacuna");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "Radnici");

            migrationBuilder.DropColumn(
                name: "Lozinka",
                table: "Radnici");

            migrationBuilder.DropColumn(
                name: "Uloga",
                table: "Radnici");

            migrationBuilder.DropColumn(
                name: "RezervacijaID1",
                table: "Racuni");

            migrationBuilder.AlterColumn<int>(
                name: "TipUslugeID",
                table: "Usluge",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Klijenti_KlijentID",
                table: "Racuni",
                column: "KlijentID",
                principalTable: "Klijenti",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaID",
                table: "Racuni",
                column: "RezervacijaID",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Klijenti_KlijentID",
                table: "Rezervacije",
                column: "KlijentID",
                principalTable: "Klijenti",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId",
                table: "Rezervacije",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeRacuna_Racuni_RacunID",
                table: "StavkeRacuna",
                column: "RacunID",
                principalTable: "Racuni",
                principalColumn: "RacunID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId",
                table: "StavkeRacuna",
                column: "UslugaId",
                principalTable: "Usluge",
                principalColumn: "UslugaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_Radnici_RadnikId",
                table: "Usluge",
                column: "RadnikId",
                principalTable: "Radnici",
                principalColumn: "RadnikId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_Rezervacije_RezervacijaID",
                table: "Usluge",
                column: "RezervacijaID",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipUslugeID",
                table: "Usluge",
                column: "TipUslugeID",
                principalTable: "TipoviUsluge",
                principalColumn: "TipUslugeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
