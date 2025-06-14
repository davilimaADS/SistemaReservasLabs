using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Models.Enums;
using SistemaReservasLabs.Services.Usuario.Registrar;

namespace SistemaReservasLabs.Services.Usuario;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;
    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }
    private string GerarMatriculaUnica()
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        string matricula;

        do
        {
            matricula = new string(Enumerable.Repeat(caracteres, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        while (_context.Usuarios.Any(u => u.Matricula == matricula));

        return matricula;
    }
    public async Task<UsuarioDTO> RegistrarUsuarioAsync(RegistroUsuarioDTO registroUsuarioDTO)
    {
        if ((registroUsuarioDTO.Funcao == Funcao.Professor || registroUsuarioDTO.Funcao == Funcao.CoordenadorCurso)
            && registroUsuarioDTO.CursoId == null)
        {
            throw new ArgumentException("O curso é obrigatório para professores e coordenadores de curso.");
        }

        var usuarioExiste = await _context.Usuarios.AnyAsync(u =>
            u.EmailInstitucional == registroUsuarioDTO.EmailInstitucional);

        if (usuarioExiste)
        {
            throw new ArgumentException("Já existe um usuário com esse e-mail institucional.");
        }

        if (registroUsuarioDTO.CursoId.HasValue)
        {
            var cursoExiste = await _context.Cursos.AnyAsync(c => c.Id == registroUsuarioDTO.CursoId);
            if (!cursoExiste)
            {
                throw new ArgumentException("O curso informado não existe.");
            }
        }

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(registroUsuarioDTO.Senha);

        var matricula = GerarMatriculaUnica();

        var usuario = new Models.Entities.Usuario
        {
            Nome = registroUsuarioDTO.Nome,
            Matricula = matricula,
            EmailInstitucional = registroUsuarioDTO.EmailInstitucional,
            SenhaHash = senhaHash,
            Funcao = registroUsuarioDTO.Funcao,
            CursoId = registroUsuarioDTO.CursoId
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return new UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Matricula = usuario.Matricula,
            EmailInstitucional = usuario.EmailInstitucional,
            CursoId = usuario.CursoId
        };
    }

}
