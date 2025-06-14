using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Laboratorio;
using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.Services.Laboratorio
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly AppDbContext _context;

        public LaboratorioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LaboratorioDTO> CriarAsync(CriarLaboratorioDTO dto)
        {
            var coordenadorExiste = await _context.Usuarios
                .AnyAsync(u => u.Id == dto.CoordenadorId && u.Funcao == Funcao.CoordenadorLaboratorio);

            if (!coordenadorExiste)
                throw new ArgumentException("Coordenador de laboratório inválido.");

            var lab = new SistemaReservasLabs.Models.Entities.Laboratorio
            {
                Nome = dto.Nome,
                Capacidade = dto.Capacidade,
                Localizacao = dto.Localizacao,
                Area = dto.Area,
                CoordenadorLaboratorioId = dto.CoordenadorId
            };

            _context.Laboratorios.Add(lab);
            await _context.SaveChangesAsync();

            return new LaboratorioDTO
            {
                Id = lab.Id,
                Nome = lab.Nome,
                Capacidade = lab.Capacidade,
                Localizacao = lab.Localizacao,
                Area = lab.Area,
                CoordenadorId = lab.CoordenadorLaboratorioId
            };
        }

        public async Task<List<LaboratorioDTO>> ListarTodosAsync()
        {
            return await _context.Laboratorios
                .Select(l => new LaboratorioDTO
                {
                    Id = l.Id,
                    Nome = l.Nome,
                    Capacidade = l.Capacidade,
                    Localizacao = l.Localizacao,
                    Area = l.Area,
                    CoordenadorId = l.CoordenadorLaboratorioId
                })
                .ToListAsync();
        }

        public async Task<LaboratorioDTO?> ObterPorIdAsync(int id)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return null;

            return new LaboratorioDTO
            {
                Id = lab.Id,
                Nome = lab.Nome,
                Capacidade = lab.Capacidade,
                Localizacao = lab.Localizacao,
                Area = lab.Area,
                CoordenadorId = lab.CoordenadorLaboratorioId
            };
        }

        public async Task<bool> AtualizarAsync(int id, CriarLaboratorioDTO dto)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return false;

            lab.Nome = dto.Nome;
            lab.Capacidade = dto.Capacidade;
            lab.Localizacao = dto.Localizacao;
            lab.Area = dto.Area;
            lab.CoordenadorLaboratorioId = dto.CoordenadorId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return false;

            _context.Laboratorios.Remove(lab);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
