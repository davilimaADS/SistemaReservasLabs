using SistemaReservasLabs.DTOs.Laboratorio;

namespace SistemaReservasLabs.Services.Laboratorio
{
    public interface ILaboratorioService
    {
        Task<LaboratorioDTO> CriarAsync(CriarLaboratorioDTO dto);
        Task<List<LaboratorioDTO>> ListarTodosAsync();
        Task<LaboratorioDTO?> ObterPorIdAsync(int id);
        Task<bool> AtualizarAsync(int id, CriarLaboratorioDTO dto);
        Task<bool> DeletarAsync(int id);
    }
}
