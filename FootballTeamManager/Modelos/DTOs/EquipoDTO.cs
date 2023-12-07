using System.ComponentModel.DataAnnotations;

namespace FootballTeamManager.Modelos.DTOs
{
    public class EquipoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El número máximo de caracteres es 100")]
        public string Nombre { get; set; }
    }
}
