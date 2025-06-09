using SistemaReservasLabs.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaReservasLabs.DTOs.Usuario;

public class RegistroUsuarioDTO
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "O campo Matricula é obrigatório")]
    public string Matricula { get; set; } = string.Empty;
    [Required(ErrorMessage = "O campo Email Institucional é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo Email Institucional é inválido")]
    public string EmailInstitucional { get; set; } = string.Empty;
    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    public string Senha { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Funcao Funcao { get; set; }
    public int? CursoId { get; set; }
}
