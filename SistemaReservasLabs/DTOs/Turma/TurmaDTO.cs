namespace SistemaReservasLabs.DTOs.Turma;

public class TurmaDTO
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Disciplina { get; set; } = string.Empty;
    public string Horario { get; set; } = string.Empty;
    public string PeriodoLetivo { get; set; } = string.Empty;
    public int CursoId { get; set; }
}
