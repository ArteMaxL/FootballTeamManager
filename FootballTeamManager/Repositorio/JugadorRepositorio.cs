using FootballTeamManager.Data;
using FootballTeamManager.Modelos;
using FootballTeamManager.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace FootballTeamManager.Repositorio
{
    public class JugadorRepositorio : IJugadorRepositorio
    {
        private readonly ApplicationDbContext _context;

        public JugadorRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ActualizarJugador(Jugador modelo)
        {
            modelo.FechaCreacion = DateTime.Now;
            _context.Jugador.Update(modelo);
            return Guardar();
        }

        public bool BorrarJugador(Jugador modelo)
        {
            _context.Jugador.Remove(modelo);
            return Guardar();
        }

        public bool CrearJugador(Jugador modelo)
        {
            _context.Jugador.Add(modelo);
            return Guardar();
        }

        public bool ExisteJugador(string nombre)
        {
            bool valor = _context.Jugador.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteJugador(int id)
        {
            bool valor = _context.Jugador.Any(x => x.Id == id);
            return valor;
        }

        public ICollection<Jugador> GetJugadores()
        {
            return _context.Jugador.OrderBy(x => x.Nombre).ToList();
        }

        public Jugador GetJugador(int equipoId)
        {
            return _context.Jugador.FirstOrDefault(x => x.Id == equipoId);
        }

        public ICollection<Jugador> GetJugadoresPorEquipo(int equipoId)
        {
            return _context.Jugador.Include(e => e.Equipo).Where(e => e.EquipoId == equipoId).ToList();
        }

        public ICollection<Jugador> BuscarJugador(string nombre)
        {
            IQueryable<Jugador> query = _context.Jugador;

            if (!string.IsNullOrEmpty(nombre)) query = query.Where(j => j.Nombre.ToLower().Contains(nombre));

            return query.ToList();
        }

        public bool Guardar()
        {
            try
            {
                return _context.SaveChanges() >= 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
