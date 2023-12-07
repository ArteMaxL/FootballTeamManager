using System.ComponentModel.DataAnnotations;

namespace FootballTeamManager.Modelos
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
