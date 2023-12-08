using AutoMapper;
using FootballTeamManager.Modelos;
using FootballTeamManager.Modelos.DTOs;

namespace FootballTeamManager.Mapper
{
    public class EquipoMapper : Profile
    {
        public EquipoMapper()
        {
            CreateMap<Equipo, EquipoDTO>().ReverseMap();
            CreateMap<Equipo, CrearEquipoDTO>().ReverseMap();

            CreateMap<Jugador, JugadorDTO>().ReverseMap();
            CreateMap<Jugador, CrearJugadorDTO>().ReverseMap();
        }
    }
}
