using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.Models.Entities
{
    public class Reserva
    {
        public int Id { get; set; }

        public int LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; } 

        public int TurmaId { get; set; }
        public Turma? Turma { get; set; }

        public int ProfessorId { get; set; }
        public Usuario? Professor { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public StatusReserva Status { get; set; }
    }
}
