using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReservasLabs.DTOs.Usuario;
using SistemaReservasLabs.Services;

namespace SistemaReservasLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
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
}
