using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Usuario;

namespace SistemaReservasLabs.Services.Usuario.Perfil
{
    public class PerfilService : IPerfilService
    {
        private readonly AppDbContext _context;

        public PerfilService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDTO> ObterPerfilAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Curso)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado");

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Matricula = usuario.Matricula,
                EmailInstitucional = usuario.EmailInstitucional,
                CursoId = usuario.CursoId,
                Funcao = usuario.Funcao
            };
        }
    }
}
