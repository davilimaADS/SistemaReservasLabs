using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
            builder.Property(u => u.EmailInstitucional).IsRequired().HasMaxLength(100);
            builder.Property(u => u.SenhaHash).IsRequired();
            builder.Property(u => u.Matricula).IsRequired().HasMaxLength(20);

            builder.HasOne(u => u.Curso)
                .WithMany(c => c.Professores)
                .HasForeignKey(u => u.CursoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.LaboratoriosTecnico)
                .WithOne(lt => lt.Tecnico)
                .HasForeignKey(lt => lt.TecnicoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
