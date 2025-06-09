namespace SistemaReservasLabs.Models.Entities
{
    public class Turma
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public string Horario { get; set; } = string.Empty;
        public string PeriodoLetivo { get; set; } = string.Empty;   

        public int CursoId { get; set; }
        public Curso? Curso { get; set; } 

        public ICollection<Reserva>? Reservas { get; set; } = new List<Reserva>();
    }
}
