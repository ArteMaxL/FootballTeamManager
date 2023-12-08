using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FootballTeamManager.Modelos.DTOs
{
    public class CrearJugadorDTO
    {

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(2)]
        [MaxLength(80, ErrorMessage = "El número máximo de caracteres es 80")]
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

        public int EquipoId { get; set; }
    }
}
