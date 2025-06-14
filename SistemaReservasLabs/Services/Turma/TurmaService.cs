using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Turma;

namespace SistemaReservasLabs.Services.Turma;

public class TurmaService : ITurmaService
{
    private readonly AppDbContext _context;
    public TurmaService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<TurmaDTO?> CriarAsync(CriarTurmaDTO dto)
    {
        var turmaExistente = await _context.Turmas.FirstOrDefaultAsync(t => t.Codigo == dto.Codigo);

        if (turmaExistente != null)
        {
            throw new ArgumentException("Turma já cadastrada.");
        }
        var turma = new SistemaReservasLabs.Models.Entities.Turma
        {
            Codigo = dto.Codigo,
            Disciplina = dto.Disciplina,
            Horario = dto.Horario,
            PeriodoLetivo = dto.PeriodoLetivo,
            CursoId = dto.CursoId
        };

        _context.Turmas.Add(turma);
        await _context.SaveChangesAsync();

        return new TurmaDTO
        {
            Id = turma.Id,
            Codigo = turma.Codigo,
            Disciplina = turma.Disciplina,
            Horario = turma.Horario,
            PeriodoLetivo = turma.PeriodoLetivo,
            CursoId = turma.CursoId
        };
    }

    public async Task<TurmaDTO?> AtualizarAsync(int id, AtualizarTurmaDTO dto)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma == null) throw new ArgumentException("Turma não encontrada.");

        turma.Codigo = dto.Codigo;
        turma.Disciplina = dto.Disciplina;
        turma.Horario = dto.Horario;
        turma.PeriodoLetivo = dto.PeriodoLetivo;
        turma.CursoId = dto.CursoId;

        await _context.SaveChangesAsync();

        return new TurmaDTO
        {
            Id = turma.Id,
            Codigo = turma.Codigo,
            Disciplina = turma.Disciplina,
            Horario = turma.Horario,
            PeriodoLetivo = turma.PeriodoLetivo,
            CursoId = turma.CursoId
        };
    }

    public async Task<TurmaDTO?> ObterPorIdAsync(int id)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma == null) throw new ArgumentException("Turma não encontrada.");

        return new TurmaDTO
        {
            Id = turma.Id,
            Codigo = turma.Codigo,
            Disciplina = turma.Disciplina,
            Horario = turma.Horario,
            PeriodoLetivo = turma.PeriodoLetivo,
            CursoId = turma.CursoId
        };
    }

    public async Task<List<TurmaDTO>> ObterTodasAsync()
    {
        return await _context.Turmas
                .Select(t => new TurmaDTO
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Disciplina = t.Disciplina,
                    Horario = t.Horario,
                    PeriodoLetivo = t.PeriodoLetivo,
                    CursoId = t.CursoId
                }).ToListAsync();
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var turma = await _context.Turmas.FindAsync(id);
        if (turma == null) throw new ArgumentException("Turma não encontrada.");

        _context.Turmas.Remove(turma);
        await _context.SaveChangesAsync();
        return true;
    }
}
