using AutoMapper;
using FootballTeamManager.Modelos.DTOs;
using FootballTeamManager.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballTeamManager.Controllers
{
    [Route("api/[controller]")]
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
    }
}
