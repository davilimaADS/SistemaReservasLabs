namespace SistemaReservasLabs.DTOs.Reserva
{
    public class CriarReservaDTO
    {
        public int LaboratorioId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
