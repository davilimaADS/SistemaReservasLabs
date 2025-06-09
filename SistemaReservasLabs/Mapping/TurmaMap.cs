using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Codigo).IsRequired().HasMaxLength(20);
            builder.Property(t => t.Disciplina).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Horario).IsRequired();
            builder.Property(t => t.PeriodoLetivo).IsRequired();

            builder.HasOne(t => t.Curso)
                .WithMany(c => c.Turmas)
                .HasForeignKey(t => t.CursoId);

            builder.HasMany(t => t.Reservas)
                .WithOne(r => r.Turma)
                .HasForeignKey(r => r.TurmaId);
        }
    }
}
