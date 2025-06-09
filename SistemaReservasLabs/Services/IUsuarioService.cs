using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Services;

public interface IUsuarioService
{
    Task<UsuarioDTO> RegistrarUsuarioAsync(RegistroUsuarioDTO registroUsuarioDTO);
}
