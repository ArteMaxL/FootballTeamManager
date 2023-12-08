using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballTeamManager.Modelos
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(80)]
        public string Nombre { get; set; }
        public string ImagenUrl { get; set; }
        public int Puntos { get; set; }
        public int Asistencias { get; set; }
        public int Ganados { get; set; }
        public double Efectividad { get; set; }
        public double PorcentajeAsistencia { get; set; }
        public Posiciones Posicion { get; set; }
        public DateOnly JuegaDesde { get; set; }
        public DateTime FechaCreacion { get; set; }

        [DefaultValue(true)]
        public bool EstaActivo { get; set; }

        [ForeignKey(nameof(EquipoId))]
        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }
    }

    public enum Posiciones
    {
        Arquero,
        Defensa,
        Mediocampo,
        Delantero
    }
}
