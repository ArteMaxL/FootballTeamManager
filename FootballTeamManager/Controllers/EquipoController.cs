using AutoMapper;
using FootballTeamManager.Modelos;
using FootballTeamManager.Modelos.DTOs;
using FootballTeamManager.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamManager.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoRepositorio _repo;
        private readonly IMapper _mapper;

        public EquipoController(IEquipoRepositorio repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEquipos()
        {
            var listaEquipos = _repo.GetEquipos();
            var listaEquiposDTO = new List<EquipoDTO>();

            foreach (var item in listaEquipos) 
            {
                listaEquiposDTO.Add(_mapper.Map<EquipoDTO>(item));
            }

            return Ok(listaEquiposDTO);
        }

        [HttpGet("{teamId:int}", Name = "GetTeam")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEquipo( int teamId)
        {
            var equipo = _repo.GetEquipo(teamId);

            if (equipo == null) return NotFound();

            var equipoDTO = _mapper.Map<EquipoDTO>(equipo);

            return Ok(equipoDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EquipoDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearEquipo([FromBody] CrearEquipoDTO crearEquipoDTO)
        {
            if (crearEquipoDTO == null) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_repo.ExisteEquipo(crearEquipoDTO.Nombre))
            {
                ModelState.AddModelError("", "El equipo ya existe");
                return NotFound(ModelState);
            }

            var equipo = _mapper.Map<Equipo>(crearEquipoDTO);

            if (!_repo.CrearEquipo(equipo))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el equipo: {equipo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetTeam", new { teamId = equipo.Id }, equipo);
        }

        [HttpPatch("{teamId:int}", Name = "PatchTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchEquipo(int teamId, [FromBody] EquipoDTO equipoDTO)
        {
            if (equipoDTO == null || teamId != equipoDTO.Id) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var equipo = _mapper.Map<Equipo>(equipoDTO);

            if (!_repo.ActualizarEquipo(equipo))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el equipo: {equipo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{teamId:int}", Name = "DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EliminarEquipo(int teamId)
        {
            if (!_repo.ExisteEquipo(teamId)) return NotFound();

            var equipo = _repo.GetEquipo(teamId);

            if (!_repo.BorrarEquipo(equipo))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el equipo: {equipo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
