using AutoMapper;
using FootballTeamManager.Modelos;
using FootballTeamManager.Modelos.DTOs;
using FootballTeamManager.Repositorio.IRepositorio;
using FootballTeamManager.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamManager.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorRepositorio _repo;
        private readonly IMapper _mapper;
        private readonly IJugadorService _jugadorService;

        public JugadorController(IJugadorRepositorio repo, IMapper mapper, IJugadorService jugadorService)
        {
            _repo = repo;
            _mapper = mapper;
            _jugadorService = jugadorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetJugadores()
        {
            var listaJugadores = _repo.GetJugadores();
            var listaJugadoresDTO = new List<JugadorDTO>();

            foreach (var item in listaJugadores)
            {
                listaJugadoresDTO.Add(_mapper.Map<JugadorDTO>(item));
            }

            return Ok(listaJugadoresDTO);
        }

        [HttpGet("{playerId:int}", Name = "GetPlayer")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetJugador(int playerId)
        {
            var jugador = _repo.GetJugador(playerId);

            if (jugador == null) return NotFound();

            var jugadorDTO = _mapper.Map<JugadorDTO>(jugador);

            return Ok(jugadorDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(JugadorDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearJugador([FromBody] CrearJugadorDTO crearJugadorDTO)
        {
            try
            {
                var jugador = _jugadorService.CrearJugador(crearJugadorDTO);
                return CreatedAtRoute("GetPlayer", new { playerId = jugador.Id }, jugador);
            }
            catch (ArgumentException ex)
            {
                return BadRequest( new { Error = ex.Message });
            }
            catch
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el jugador: {crearJugadorDTO.Nombre}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPatch("{playerId:int}", Name = "PatchPlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchJugador(int playerId, [FromBody] JugadorDTO jugadorDTO)
        {
            if (jugadorDTO == null || playerId != jugadorDTO.Id) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var jugador = _mapper.Map<Jugador>(jugadorDTO);

            if (!_repo.ActualizarJugador(jugador))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el jugador: {jugador.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{playerId:int}", Name = "EliminarJugador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarJugador(int playerId)
        {
            if (!_repo.ExisteJugador(playerId)) return NotFound();

            var jugador = _repo.GetJugador(playerId);

            if (!_repo.BorrarJugador(jugador))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el jugador: {jugador.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("GetPlayersByTeam/{teamId:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetJugadoresPorEquipo(int teamId)
        {
            var listaJugadores = _repo.GetJugadoresPorEquipo(teamId);

            if (listaJugadores == null) NotFound();

            var listaJugadoresDTO = new List<JugadorDTO>();

            foreach (var item in listaJugadores)
            {
                listaJugadoresDTO.Add(_mapper.Map<JugadorDTO>(item));
            }

            return Ok(listaJugadoresDTO);
        }

        [HttpGet("SearchByName/{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Buscar(string name)
        {
            try
            {
                var resultado = _repo.BuscarJugador(name.ToLower().Trim());
                if (resultado.Any()) return Ok(resultado);

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
