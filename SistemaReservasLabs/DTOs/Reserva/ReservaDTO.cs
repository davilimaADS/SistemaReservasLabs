using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.DTOs.Reserva
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int LaboratorioId { get; set; }
        public string LaboratorioNome { get; set; } = string.Empty;
        public int TurmaId { get; set; }
        public string TurmaCodigo { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
        public string ProfessorNome { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public StatusReserva Status { get; set; }
    }
}
