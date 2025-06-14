using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Reserva;
using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.Services.Reserva
{
    public class ReservaService : IReservaService
    {
        private readonly AppDbContext _context;

        public ReservaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AprovarAsync(int id, Funcao aprovador)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            switch (reserva.Status)
            {
                case StatusReserva.Pendente:
                    if (aprovador != Funcao.CoordenadorLaboratorio)
                        throw new UnauthorizedAccessException("Somente o Coordenador de Laboratório pode aprovar essa etapa.");
                    reserva.Status = StatusReserva.AprovadoCoordenadorLab;
                    break;

                case StatusReserva.AprovadoCoordenadorLab:
                    if (aprovador != Funcao.CoordenadorCurso)
                        throw new UnauthorizedAccessException("Somente o Coordenador de Curso pode aprovar essa etapa.");
                    reserva.Status = StatusReserva.AprovadoCoordenadorCurso;
                    break;

                case StatusReserva.AprovadoCoordenadorCurso:
                    if (aprovador != Funcao.Reitoria)
                        throw new UnauthorizedAccessException("Somente a Reitoria pode aprovar essa etapa.");
                    reserva.Status = StatusReserva.AprovadoReitoria;
                    break;

                case StatusReserva.AprovadoReitoria:
                    throw new InvalidOperationException("A reserva já foi totalmente aprovada.");

                default:
                    throw new InvalidOperationException("Não é possível aprovar uma reserva rejeitada.");
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReservaDTO> CriarAsync(int professorId, CriarReservaDTO dto)
        {
            // Validar conflito de horário
            bool conflito = await _context.Reservas.AnyAsync(r =>
                r.LaboratorioId == dto.LaboratorioId &&
                r.DataHoraInicio < dto.DataHoraFim &&
                r.DataHoraFim > dto.DataHoraInicio);

            if (conflito)
                throw new ArgumentException("O laboratório já está reservado nesse horário.");

            var reserva = new SistemaReservasLabs.Models.Entities.Reserva
            {
                ProfessorId = professorId,
                LaboratorioId = dto.LaboratorioId,
                TurmaId = dto.TurmaId,
                DataHoraInicio = dto.DataHoraInicio,
                DataHoraFim = dto.DataHoraFim,
                Status = StatusReserva.Pendente
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return await ObterPorIdAsync(reserva.Id) ?? throw new Exception("Erro ao buscar reserva criada.");
        }

        public async Task<List<ReservaDTO>> ListarAsync()
        {
            return await _context.Reservas
        .Include(r => r.Laboratorio)
        .Include(r => r.Turma)
        .Include(r => r.Professor)
        .Select(reserva => new ReservaDTO
        {
            Id = reserva.Id,
            LaboratorioId = reserva.LaboratorioId,
            LaboratorioNome = reserva.Laboratorio != null ? reserva.Laboratorio.Nome : string.Empty,
            TurmaId = reserva.TurmaId,
            TurmaCodigo = reserva.Turma != null ? reserva.Turma.Codigo : string.Empty,
            ProfessorId = reserva.ProfessorId,
            ProfessorNome = reserva.Professor != null ? reserva.Professor.Nome : string.Empty,
            DataHoraInicio = reserva.DataHoraInicio,
            DataHoraFim = reserva.DataHoraFim,
            Status = reserva.Status,
        })
        .ToListAsync();
        }

        public async Task<ReservaDTO?> ObterPorIdAsync(int id)
        {
            var reserva = await _context.Reservas
         .Include(r => r.Laboratorio)
         .Include(r => r.Turma)
         .Include(r => r.Professor)
         .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null) return null;

            return new ReservaDTO
            {
                Id = reserva.Id,
                LaboratorioId = reserva.LaboratorioId,
                LaboratorioNome = reserva.Laboratorio?.Nome ?? string.Empty,
                TurmaId = reserva.TurmaId,
                TurmaCodigo = reserva.Turma?.Codigo ?? string.Empty,
                ProfessorId = reserva.ProfessorId,
                ProfessorNome = reserva.Professor?.Nome ?? string.Empty,
                DataHoraInicio = reserva.DataHoraInicio,
                DataHoraFim = reserva.DataHoraFim,
                Status = reserva.Status,
            };
        }

        public async Task<bool> RejeitarAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            if (reserva.Status == StatusReserva.AprovadoReitoria || reserva.Status == StatusReserva.Rejeitado)
                throw new InvalidOperationException("Essa reserva não pode ser rejeitada.");

            reserva.Status = StatusReserva.Rejeitado;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
