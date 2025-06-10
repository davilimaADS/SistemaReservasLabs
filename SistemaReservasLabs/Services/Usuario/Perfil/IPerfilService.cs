using SistemaReservasLabs.DTOs.Usuario;

namespace SistemaReservasLabs.Services.Usuario.Perfil
{
    public interface IPerfilService
    {
        Task<UsuarioDTO> ObterPerfilAsync(int usuarioId);
    }
}
