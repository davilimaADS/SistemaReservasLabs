using SistemaReservasLabs.Models.Enums;
using System.Text.Json.Serialization;

namespace SistemaReservasLabs.DTOs.Usuario
{
    public class PerfilUsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string EmailInstitucional { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Funcao Funcao { get; set; }

        public int? CursoId { get; set; }
    }
}
