using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Mapping
{
    public class LaboratorioTecnicoMap : IEntityTypeConfiguration<LaboratorioTecnico>
    {
        public void Configure(EntityTypeBuilder<LaboratorioTecnico> builder)
        {
            builder.ToTable("LaboratorioTecnicos");

            builder.HasKey(lt => new { lt.LaboratorioId, lt.TecnicoId });

            builder.HasOne(lt => lt.Laboratorio)
                .WithMany(l => l.Tecnicos)
                .HasForeignKey(lt => lt.LaboratorioId);

            builder.HasOne(lt => lt.Tecnico)
                .WithMany(u => u.LaboratoriosTecnico)
                .HasForeignKey(lt => lt.TecnicoId);
        }
    }
}
