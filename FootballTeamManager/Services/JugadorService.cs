using AutoMapper;
using FootballTeamManager.Modelos;
using FootballTeamManager.Modelos.DTOs;
using FootballTeamManager.Repositorio.IRepositorio;
using FootballTeamManager.Services.IServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FootballTeamManager.Services
{
    public class JugadorService : IJugadorService
    {
        private readonly IJugadorRepositorio _repo;
        private readonly IMapper _mapper;

        public JugadorService(IMapper mapper, IJugadorRepositorio repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public JugadorDTO CrearJugador(CrearJugadorDTO modelo)
        {
            if (!IsValid(modelo) || modelo is null) throw new ArgumentException("El modelo no es válido");

            if (_repo.ExisteJugador(modelo.Nombre))
                throw new ArgumentException("El jugador ya existe");

            var jugador = _mapper.Map<Jugador>(modelo);

            if (jugador.EquipoId == 0) jugador.EquipoId = null;

            jugador.FechaCreacion = DateTime.Now;
            jugador.EstaActivo = true;

            var creacionExitosa = _repo.CrearJugador(jugador);

            var jugadorDTO = _mapper.Map<JugadorDTO>(jugador);

            return creacionExitosa ? jugadorDTO : null;
        }

        private bool IsValid(object model)
        {
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(model, validationContext, validationResults, true);
        }
    }
}
