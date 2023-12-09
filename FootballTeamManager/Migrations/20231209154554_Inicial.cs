using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamManager.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jugador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    ImagenUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Puntos = table.Column<int>(type: "INTEGER", nullable: true),
                    Asistencias = table.Column<int>(type: "INTEGER", nullable: true),
                    Ganados = table.Column<int>(type: "INTEGER", nullable: true),
                    Efectividad = table.Column<double>(type: "REAL", nullable: true),
                    PorcentajeAsistencia = table.Column<double>(type: "REAL", nullable: true),
                    Posicion = table.Column<int>(type: "INTEGER", nullable: false),
                    JuegaDesde = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstaActivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    EquipoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jugador_Equipo_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jugador_EquipoId",
                table: "Jugador",
                column: "EquipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jugador");

            migrationBuilder.DropTable(
                name: "Equipo");
        }
    }
}
