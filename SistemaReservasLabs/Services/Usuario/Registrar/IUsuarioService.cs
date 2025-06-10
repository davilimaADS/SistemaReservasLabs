using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Services.Usuario.Registrar;

public interface IUsuarioService
{
    Task<UsuarioDTO> RegistrarUsuarioAsync(RegistroUsuarioDTO registroUsuarioDTO);
}
