namespace SistemaReservasLabs.DTOs.Laboratorio
{
    public class LaboratorioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Localizacao { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public bool EmManutencao { get; set; }
        public int CoordenadorId { get; set; }
    }
}
