using SistemaReservasLabs.DTOs.Usuario;

namespace SistemaReservasLabs.Services.Usuario.Login
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}
