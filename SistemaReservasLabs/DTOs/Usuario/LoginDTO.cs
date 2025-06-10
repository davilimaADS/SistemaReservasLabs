using System.ComponentModel.DataAnnotations;

namespace SistemaReservasLabs.DTOs.Usuario
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo Matrícula ou Email é obrigatório")]
        public string MatriculaOuEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; } = string.Empty;
    }
}
