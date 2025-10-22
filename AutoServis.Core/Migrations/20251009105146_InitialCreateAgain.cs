using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServis.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAgain : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Klijenti_KlijentID",
                table: "Vozila");

            migrationBuilder.DropIndex(
                name: "IX_Usluge_TipID",
                table: "Usluge");

            migrationBuilder.DropIndex(
                name: "IX_StavkeRacuna_UslugaId1",
                table: "StavkeRacuna");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_StatusId1",
                table: "Rezervacije");

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
                name: "RezervacijaID1",
                table: "Racuni");

            migrationBuilder.RenameColumn(
                name: "KlijentID",
                table: "Vozila",
                newName: "KlijentId");

            migrationBuilder.RenameColumn(
                name: "VoziloID",
                table: "Vozila",
                newName: "VoziloId");

            migrationBuilder.RenameIndex(
                name: "IX_Vozila_KlijentID",
                table: "Vozila",
                newName: "IX_Vozila_KlijentId");

            migrationBuilder.RenameColumn(
                name: "TipUslugeID",
                table: "Usluge",
                newName: "TipUslugeId");

            migrationBuilder.RenameColumn(
                name: "TipID",
                table: "Usluge",
                newName: "TipId");

            migrationBuilder.RenameColumn(
                name: "RezervacijaID",
                table: "Usluge",
                newName: "RezervacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_Usluge_TipUslugeID",
                table: "Usluge",
                newName: "IX_Usluge_TipUslugeId");

            migrationBuilder.RenameIndex(
                name: "IX_Usluge_RezervacijaID",
                table: "Usluge",
                newName: "IX_Usluge_RezervacijaId");

            migrationBuilder.RenameColumn(
                name: "TipUslugeID",
                table: "TipoviUsluge",
                newName: "TipUslugeId");

            migrationBuilder.RenameColumn(
                name: "RacunID",
                table: "StavkeRacuna",
                newName: "RacunId");

            migrationBuilder.RenameIndex(
                name: "IX_StavkeRacuna_RacunID",
                table: "StavkeRacuna",
                newName: "IX_StavkeRacuna_RacunId");

            migrationBuilder.RenameColumn(
                name: "KlijentID",
                table: "Rezervacije",
                newName: "KlijentId");

            migrationBuilder.RenameColumn(
                name: "RezervacijaID",
                table: "Rezervacije",
                newName: "RezervacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacije_KlijentID",
                table: "Rezervacije",
                newName: "IX_Rezervacije_KlijentId");

            migrationBuilder.RenameColumn(
                name: "RezervacijaID",
                table: "Racuni",
                newName: "RezervacijaId");

            migrationBuilder.RenameColumn(
                name: "KlijentID",
                table: "Racuni",
                newName: "KlijentId");

            migrationBuilder.RenameColumn(
                name: "RacunID",
                table: "Racuni",
                newName: "RacunId");

            migrationBuilder.RenameIndex(
                name: "IX_Racuni_RezervacijaID",
                table: "Racuni",
                newName: "IX_Racuni_RezervacijaId");

            migrationBuilder.RenameIndex(
                name: "IX_Racuni_KlijentID",
                table: "Racuni",
                newName: "IX_Racuni_KlijentId");

            migrationBuilder.RenameColumn(
                name: "KlijentID",
                table: "Klijenti",
                newName: "KlijentId");

            migrationBuilder.AlterColumn<int>(
                name: "TipUslugeId",
                table: "Usluge",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Klijenti_KlijentId",
                table: "Racuni",
                column: "KlijentId",
                principalTable: "Klijenti",
                principalColumn: "KlijentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaId",
                table: "Racuni",
                column: "RezervacijaId",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Klijenti_KlijentId",
                table: "Rezervacije",
                column: "KlijentId",
                principalTable: "Klijenti",
                principalColumn: "KlijentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId",
                table: "Rezervacije",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkeRacuna_Racuni_RacunId",
                table: "StavkeRacuna",
                column: "RacunId",
                principalTable: "Racuni",
                principalColumn: "RacunId",
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
                name: "FK_Usluge_Rezervacije_RezervacijaId",
                table: "Usluge",
                column: "RezervacijaId",
                principalTable: "Rezervacije",
                principalColumn: "RezervacijaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipUslugeId",
                table: "Usluge",
                column: "TipUslugeId",
                principalTable: "TipoviUsluge",
                principalColumn: "TipUslugeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Klijenti_KlijentId",
                table: "Vozila",
                column: "KlijentId",
                principalTable: "Klijenti",
                principalColumn: "KlijentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Klijenti_KlijentId",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Racuni_Rezervacije_RezervacijaId",
                table: "Racuni");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Klijenti_KlijentId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Statusi_StatusId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Racuni_RacunId",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkeRacuna_Usluge_UslugaId",
                table: "StavkeRacuna");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_Radnici_RadnikId",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_Rezervacije_RezervacijaId",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Usluge_TipoviUsluge_TipUslugeId",
                table: "Usluge");

            migrationBuilder.DropForeignKey(
                name: "FK_Vozila_Klijenti_KlijentId",
                table: "Vozila");

            migrationBuilder.RenameColumn(
                name: "KlijentId",
                table: "Vozila",
                newName: "KlijentID");

            migrationBuilder.RenameColumn(
                name: "VoziloId",
                table: "Vozila",
                newName: "VoziloID");

            migrationBuilder.RenameIndex(
                name: "IX_Vozila_KlijentId",
                table: "Vozila",
                newName: "IX_Vozila_KlijentID");

            migrationBuilder.RenameColumn(
                name: "TipUslugeId",
                table: "Usluge",
                newName: "TipUslugeID");

            migrationBuilder.RenameColumn(
                name: "TipId",
                table: "Usluge",
                newName: "TipID");

            migrationBuilder.RenameColumn(
                name: "RezervacijaId",
                table: "Usluge",
                newName: "RezervacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Usluge_TipUslugeId",
                table: "Usluge",
                newName: "IX_Usluge_TipUslugeID");

            migrationBuilder.RenameIndex(
                name: "IX_Usluge_RezervacijaId",
                table: "Usluge",
                newName: "IX_Usluge_RezervacijaID");

            migrationBuilder.RenameColumn(
                name: "TipUslugeId",
                table: "TipoviUsluge",
                newName: "TipUslugeID");

            migrationBuilder.RenameColumn(
                name: "RacunId",
                table: "StavkeRacuna",
                newName: "RacunID");

            migrationBuilder.RenameIndex(
                name: "IX_StavkeRacuna_RacunId",
                table: "StavkeRacuna",
                newName: "IX_StavkeRacuna_RacunID");

            migrationBuilder.RenameColumn(
                name: "KlijentId",
                table: "Rezervacije",
                newName: "KlijentID");

            migrationBuilder.RenameColumn(
                name: "RezervacijaId",
                table: "Rezervacije",
                newName: "RezervacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacije_KlijentId",
                table: "Rezervacije",
                newName: "IX_Rezervacije_KlijentID");

            migrationBuilder.RenameColumn(
                name: "RezervacijaId",
                table: "Racuni",
                newName: "RezervacijaID");

            migrationBuilder.RenameColumn(
                name: "KlijentId",
                table: "Racuni",
                newName: "KlijentID");

            migrationBuilder.RenameColumn(
                name: "RacunId",
                table: "Racuni",
                newName: "RacunID");

            migrationBuilder.RenameIndex(
                name: "IX_Racuni_RezervacijaId",
                table: "Racuni",
                newName: "IX_Racuni_RezervacijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Racuni_KlijentId",
                table: "Racuni",
                newName: "IX_Racuni_KlijentID");

            migrationBuilder.RenameColumn(
                name: "KlijentId",
                table: "Klijenti",
                newName: "KlijentID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Vozila_Klijenti_KlijentID",
                table: "Vozila",
                column: "KlijentID",
                principalTable: "Klijenti",
                principalColumn: "KlijentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
