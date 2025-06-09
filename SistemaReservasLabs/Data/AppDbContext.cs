using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Models.Entities;
using System.Reflection;

namespace SistemaReservasLabs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<LaboratorioTecnico> LaboratoriosTecnicos { get; set; }
        public DbSet<HistoricoReserva> HistoricoReservas { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
