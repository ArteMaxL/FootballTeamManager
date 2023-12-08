using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballTeamManager.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaJugador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jugador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    ImagenUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Puntos = table.Column<int>(type: "INTEGER", nullable: false),
                    Asistencias = table.Column<int>(type: "INTEGER", nullable: false),
                    Ganados = table.Column<int>(type: "INTEGER", nullable: false),
                    Efectividad = table.Column<double>(type: "REAL", nullable: false),
                    PorcentajeAsistencia = table.Column<double>(type: "REAL", nullable: false),
                    Posicion = table.Column<int>(type: "INTEGER", nullable: false),
                    JuegaDesde = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstaActivo = table.Column<bool>(type: "INTEGER", nullable: false),
                    EquipoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jugador_Equipo_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }
    }
}
