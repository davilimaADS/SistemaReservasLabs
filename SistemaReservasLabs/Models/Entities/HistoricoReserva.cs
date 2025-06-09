using Microsoft.EntityFrameworkCore;

namespace SistemaReservasLabs.Models.Entities
{


    [Keyless]
    public class HistoricoReserva
    {
        public string Professor { get; set; } = string.Empty;
        public string Turma { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public string Curso { get; set; } = string.Empty;
        public string Laboratorio { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
