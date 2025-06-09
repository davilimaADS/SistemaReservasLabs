using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string EmailInstitucional { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public Funcao Funcao { get; set; }

        // Relacionamentos
        public int? CursoId { get; set; }
        public Curso? Curso { get; set; }

        public ICollection<LaboratorioTecnico>? LaboratoriosTecnico { get; set; } = new List<LaboratorioTecnico>();
    }
}
