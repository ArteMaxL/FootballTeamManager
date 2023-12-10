using FootballTeamManager.Modelos;
using FootballTeamManager.Modelos.DTOs;

namespace FootballTeamManager.Services.IServices
{
    public interface IJugadorService
    {
        JugadorDTO CrearJugador(CrearJugadorDTO modelo);
    }
}
