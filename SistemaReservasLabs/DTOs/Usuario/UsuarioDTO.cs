using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.DTOs.Usuario;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string EmailInstitucional { get; set; } = string.Empty;
    public int? CursoId { get; set; }
    public Funcao Funcao { get; set; }
}
