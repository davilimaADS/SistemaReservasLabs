using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class HistoricoReservaMap : IEntityTypeConfiguration<HistoricoReserva>
    {
        public void Configure(EntityTypeBuilder<HistoricoReserva> builder)
        {
            builder.HasNoKey();

            builder.ToView("View_HistoricoReservas");

            builder.Property(h => h.Professor).HasColumnName("Professor");
            builder.Property(h => h.Turma).HasColumnName("Turma");
            builder.Property(h => h.Disciplina).HasColumnName("Disciplina");
            builder.Property(h => h.Curso).HasColumnName("Curso");
            builder.Property(h => h.Laboratorio).HasColumnName("Laboratorio");
            builder.Property(h => h.DataHoraInicio).HasColumnName("DataHoraInicio");
            builder.Property(h => h.DataHoraFim).HasColumnName("DataHoraFim");
            builder.Property(h => h.Status).HasColumnName("Status");
        }
    }
}
