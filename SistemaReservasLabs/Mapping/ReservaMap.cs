using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class ReservaMap : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.DataHoraInicio).IsRequired();
            builder.Property(r => r.DataHoraFim).IsRequired();
            builder.Property(r => r.Status).IsRequired();

            builder.HasOne(r => r.Laboratorio)
                .WithMany(l => l.Reservas)
                .HasForeignKey(r => r.LaboratorioId);

            builder.HasOne(r => r.Turma)
                .WithMany(t => t.Reservas)
                .HasForeignKey(r => r.TurmaId);

            builder.HasOne(r => r.Professor)
                .WithMany()
                .HasForeignKey(r => r.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
