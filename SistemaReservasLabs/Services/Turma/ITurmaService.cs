using SistemaReservasLabs.DTOs.Turma;

namespace SistemaReservasLabs.Services.Turma;

public interface ITurmaService
{
    Task<TurmaDTO?> CriarAsync(CriarTurmaDTO dto);
    Task<TurmaDTO?> AtualizarAsync(int id, AtualizarTurmaDTO dto);
    Task<List<TurmaDTO>> ObterTodasAsync();
    Task<TurmaDTO?> ObterPorIdAsync(int id);
    Task<bool> DeletarAsync(int id);
}
