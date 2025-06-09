using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class LaboratorioMap : IEntityTypeConfiguration<Laboratorio>
    {
        public void Configure(EntityTypeBuilder<Laboratorio> builder)
        {
            builder.ToTable("Laboratorios");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Nome).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Capacidade).IsRequired();
            builder.Property(l => l.Localizacao).IsRequired();
            builder.Property(l => l.Area).IsRequired();

            builder.HasOne(l => l.CoordenadorLaboratorio)
                .WithMany()
                .HasForeignKey(l => l.CoordenadorLaboratorioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Tecnicos)
                .WithOne(lt => lt.Laboratorio)
                .HasForeignKey(lt => lt.LaboratorioId);

            builder.HasMany(l => l.Reservas)
                .WithOne(r => r.Laboratorio)
                .HasForeignKey(r => r.LaboratorioId);
        }
    }
}
