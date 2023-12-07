using FootballTeamManager.Data;
using FootballTeamManager.Modelos;
using FootballTeamManager.Repositorio.IRepositorio;

namespace FootballTeamManager.Repositorio
{
    public class EquipoRepositorio : IEquipoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public EquipoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ActualizarEquipo(Equipo modelo)
        {
            modelo.FechaCreacion = DateTime.Now;
            _context.Equipo.Update(modelo);
            return Guardar();
        }

        public bool BorrarEquipo(Equipo modelo)
        {
            _context.Equipo.Remove(modelo);
            return Guardar();
        }

        public bool CrearEquipo(Equipo modelo)
        {
            modelo.FechaCreacion = DateTime.Now;
            _context.Equipo.Add(modelo);
            return Guardar();
        }

        public bool ExisteEquipo(string nombre)
        {
            bool valor = _context.Equipo.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public bool ExisteEquipo(int id)
        {
            bool valor = _context.Equipo.Any(x => x.Id == id);
            return valor;
        }

        public ICollection<Equipo> GetEquipos()
        {
            return _context.Equipo.OrderBy(x => x.Nombre).ToList();
        }

        public Equipo GetEquipo(int equipoId)
        {
            return _context.Equipo.FirstOrDefault(x => x.Id == equipoId);
        }

        public bool Guardar()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
