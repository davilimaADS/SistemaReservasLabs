using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Services.Token
{
    public interface ITokenService
    {
        string GerarToken(SistemaReservasLabs.Models.Entities.Usuario usuario);
    }
}
