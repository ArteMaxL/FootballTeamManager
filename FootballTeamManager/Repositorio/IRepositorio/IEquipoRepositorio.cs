using FootballTeamManager.Modelos;

namespace FootballTeamManager.Repositorio.IRepositorio
{
    public interface IEquipoRepositorio
    {
        ICollection<Equipo> GetEquipos();
        Equipo GetEquipo(int equipoId);
        bool ExisteEquipo(string nombre);
        bool ExisteEquipo(int id);
        bool CrearEquipo(Equipo modelo);
        bool ActualizarEquipo(Equipo modelo);
        bool BorrarEquipo(Equipo modelo);
        bool Guardar();
    }
}
