using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.Professores)
                .WithOne(u => u.Curso)
                .HasForeignKey(u => u.CursoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Turmas)
                .WithOne(t => t.Curso)
                .HasForeignKey(t => t.CursoId);
        }
    }
}
