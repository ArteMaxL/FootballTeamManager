using AutoMapper;
using FootballTeamManager.Modelos;
using FootballTeamManager.Modelos.DTOs;
using FootballTeamManager.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadorController : ControllerBase
    {
        private readonly IJugadorRepositorio _repo;
        private readonly IMapper _mapper;

        public JugadorController(IJugadorRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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

        [HttpGet("{jugadorId:int}", Name = "GetJugador")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetJugador(int jugadorId)
        {
            var jugador = _repo.GetJugador(jugadorId);

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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (crearJugadorDTO == null) return BadRequest(ModelState);

            if (_repo.ExisteJugador(crearJugadorDTO.Nombre))
            {
                ModelState.AddModelError("", "El jugador ya existe");
                return NotFound(ModelState);
            }

            var jugador = _mapper.Map<Jugador>(crearJugadorDTO);

            if (!_repo.CrearJugador(jugador))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el jugador: {jugador.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetJugador", new { jugadorId = jugador.Id }, jugador);
        }

        [HttpPatch("{jugadorId:int}", Name = "PatchJugador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchJugador(int jugadorId, [FromBody] JugadorDTO jugadorDTO)
        {
            if (jugadorDTO == null || jugadorId != jugadorDTO.Id) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var jugador = _mapper.Map<Jugador>(jugadorDTO);

            if (!_repo.ActualizarJugador(jugador))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el jugador: {jugador.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{jugadorId:int}", Name = "EliminarJugador")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarJugador(int jugadorId)
        {
            if (!_repo.ExisteJugador(jugadorId)) return NotFound();

            var jugador = _repo.GetJugador(jugadorId);

            if (!_repo.BorrarJugador(jugador))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el jugador: {jugador.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("GetJugadoresPorEquipo/{equipoId:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetJugadoresPorEquipo(int equipoId)
        {
            var listaJugadores = _repo.GetJugadoresPorEquipo(equipoId);

            if (listaJugadores == null) NotFound();

            var listaJugadoresDTO = new List<JugadorDTO>();

            foreach (var item in listaJugadores)
            {
                listaJugadoresDTO.Add(_mapper.Map<JugadorDTO>(item));
            }

            return Ok(listaJugadoresDTO);
        }

        [HttpGet("Buscar/{nombre:alpha}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _repo.BuscarJugador(nombre.Trim());
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
