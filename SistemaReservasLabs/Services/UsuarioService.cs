using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Models.Entities;
using SistemaReservasLabs.Models.Enums;

namespace SistemaReservasLabs.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;
    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<UsuarioDTO> RegistrarUsuarioAsync(RegistroUsuarioDTO registroUsuarioDTO)
    {
        if ((registroUsuarioDTO.Funcao == Funcao.Professor || registroUsuarioDTO.Funcao == Funcao.CoordenadorCurso) && registroUsuarioDTO.CursoId == null)
        {
            throw new ArgumentException("O curso é obrigatório para professores e coordenadores de curso.");
        }
        var usuarioExiste = await _context.Usuarios.AnyAsync(u =>
        u.Matricula == registroUsuarioDTO.Matricula || u.EmailInstitucional == registroUsuarioDTO.EmailInstitucional);

        if (usuarioExiste)
        {
            throw new ArgumentException("Já existe um usuário com essa matrícula ou e-mail institucional.");
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
        var usuario = new Usuario
        {
            Nome = registroUsuarioDTO.Nome,
            Matricula = registroUsuarioDTO.Matricula,
            EmailInstitucional = registroUsuarioDTO.EmailInstitucional,
            SenhaHash = senhaHash,
            Funcao = registroUsuarioDTO.Funcao,
            CursoId = registroUsuarioDTO.CursoId
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return new UsuarioDTO()
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Matricula = usuario.Matricula,
            EmailInstitucional = usuario.EmailInstitucional,
            CursoId = usuario.CursoId
        };
    }
}
