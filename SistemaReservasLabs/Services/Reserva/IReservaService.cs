using SistemaReservasLabs.DTOs.Reserva;
using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.Services.Reserva
{
    public interface IReservaService
    {
        Task<ReservaDTO> CriarAsync(int professorId, CriarReservaDTO dto);
        Task<List<ReservaDTO>> ListarAsync();
        Task<ReservaDTO?> ObterPorIdAsync(int id);
        Task<bool> AprovarAsync(int id, Funcao aprovador);
        Task<bool> RejeitarAsync(int id);
    }
}

