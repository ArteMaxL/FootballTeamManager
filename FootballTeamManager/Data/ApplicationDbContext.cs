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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jugador>()
                .HasOne(x => x.Equipo)
                .WithMany()
                .HasForeignKey(x => x.EquipoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Jugador>()
                .Property(j => j.Puntos)
                .IsRequired(false);

            modelBuilder.Entity<Jugador>()
                .Property(j => j.Asistencias)
                .IsRequired(false);

            modelBuilder.Entity<Jugador>()
                .Property(j => j.Ganados)
                .IsRequired(false);

            modelBuilder.Entity<Jugador>()
                .Property(j => j.Efectividad)
                .IsRequired(false);

            modelBuilder.Entity<Jugador>()
                .Property(j => j.PorcentajeAsistencia)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
