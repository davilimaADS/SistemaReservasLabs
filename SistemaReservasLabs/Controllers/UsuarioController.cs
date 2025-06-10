
using Microsoft.AspNetCore.Mvc;
using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Services.Usuario.Login;
using SistemaReservasLabs.Services.Usuario.Registrar;

namespace SistemaReservasLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILoginService _loginUsuarioService;
    public UsuarioController(IUsuarioService usuarioService, ILoginService loginUsuarioService)
    {
        _usuarioService = usuarioService;
        _loginUsuarioService = loginUsuarioService;
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
}
