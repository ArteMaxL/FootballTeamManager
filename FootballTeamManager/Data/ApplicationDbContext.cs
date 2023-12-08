using FootballTeamManager.Modelos;
using Microsoft.EntityFrameworkCore;

namespace FootballTeamManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Equipo> Equipo { get; set; }   
        public DbSet<Jugador> Jugador { get; set; }   
    }
}
