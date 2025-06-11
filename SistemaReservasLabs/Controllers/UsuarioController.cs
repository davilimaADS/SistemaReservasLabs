
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Services.Usuario.Login;
using SistemaReservasLabs.Services.Usuario.Perfil;
using SistemaReservasLabs.Services.Usuario.Registrar;
using System.Security.Claims;

namespace SistemaReservasLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILoginService _loginUsuarioService;
    private readonly IPerfilService _perfilService; 
    public UsuarioController(IUsuarioService usuarioService, ILoginService loginUsuarioService, IPerfilService perfilService)
    {
        _usuarioService = usuarioService;
        _loginUsuarioService = loginUsuarioService;
        _perfilService = perfilService;
    }
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] RegistroUsuarioDTO registroUsuarioDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var response = await _usuarioService.RegistrarUsuarioAsync(registroUsuarioDTO);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _loginUsuarioService.LoginAsync(loginDto);
            return Ok(response); 
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }
    [Authorize]
    [HttpGet("perfil")]
    public async Task<IActionResult> ObterPerfil()
    {
        var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var perfil = await _perfilService.ObterPerfilAsync(usuarioId);
        return Ok(perfil);
    }
}
