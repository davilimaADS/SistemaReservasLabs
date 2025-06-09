namespace SistemaReservasLabs.Models.Entities
{
    public class LaboratorioTecnico
    {
        public int LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; } 

        public int TecnicoId { get; set; }
        public Usuario? Tecnico { get; set; }
    }
}
