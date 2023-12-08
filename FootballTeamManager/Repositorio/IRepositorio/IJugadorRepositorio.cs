using FootballTeamManager.Modelos;

namespace FootballTeamManager.Repositorio.IRepositorio
{
    public interface IJugadorRepositorio
    {
        ICollection<Jugador> GetJugadores();
        Jugador GetJugador(int jugadorId);
        bool ExisteJugador(string nombre);
        bool ExisteJugador(int id);
        bool CrearJugador(Jugador modelo);
        bool ActualizarJugador(Jugador modelo);
        bool BorrarJugador(Jugador modelo);
        ICollection<Jugador> GetJugadoresPorEquipo(int equipoId);
        ICollection<Jugador> BuscarJugador(string nombre);
        bool Guardar();
    }
}
