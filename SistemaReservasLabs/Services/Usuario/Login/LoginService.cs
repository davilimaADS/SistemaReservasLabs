using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Services.Token;

namespace SistemaReservasLabs.Services.Usuario.Login
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginService(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Matricula == loginDTO.MatriculaOuEmail || u.EmailInstitucional == loginDTO.MatriculaOuEmail);

            if (usuario == null)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado.");
            }

            bool senhaValida = BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuario.SenhaHash);

            if (!senhaValida)
            {
                throw new UnauthorizedAccessException("Senha incorreta.");
            }

            var token = _tokenService.GerarToken(usuario);

            return token;
        }
    }
}
