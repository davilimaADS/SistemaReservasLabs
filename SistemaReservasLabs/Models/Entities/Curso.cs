namespace SistemaReservasLabs.Models.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<Usuario>? Professores { get; set; }
        public ICollection<Turma>? Turmas { get; set; }
    }
}
