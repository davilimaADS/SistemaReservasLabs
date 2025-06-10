using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaReservasLabs.DTOs.Reserva;
using SistemaReservasLabs.Models.Enums;
using SistemaReservasLabs.Services.Reserva;
using System.Security.Claims;

namespace SistemaReservasLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservaController(IReservaService service)
        {
            _service = service;
        }

        private int ObterUsuarioId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.TryParse(claim?.Value, out var id) ? id : 0;
        }

        [HttpPost]
        [Authorize(Roles = "Professor,CoordenadorCurso,CoordenadorLaboratorio,Reitoria")]
        public async Task<IActionResult> Criar([FromBody] CriarReservaDTO dto)
        {
            try
            {
                var professorId = ObterUsuarioId();
                var reserva = await _service.CriarAsync(professorId, dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = reserva.Id }, reserva);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var reservas = await _service.ListarAsync();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var reserva = await _service.ObterPorIdAsync(id);
            return reserva is null ? NotFound() : Ok(reserva);
        }
        [HttpPut("{id}/aprovar")]
        [Authorize(Roles = "CoordenadorCurso,CoordenadorLaboratorio,Reitoria")] // só quem pode aprovar
        public async Task<IActionResult> Aprovar(int id, [FromQuery] Funcao aprovador)
        {
            var claimFuncao = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!Enum.TryParse<Funcao>(claimFuncao, out var funcao))
                return Forbid("Função do usuário não reconhecida.");

            try
            {
                var sucesso = await _service.AprovarAsync(id, funcao);
                return sucesso ? Ok("Reserva aprovada com sucesso.") : NotFound("Reserva não encontrada.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/rejeitar")]
        [Authorize(Roles = "CoordenadorCurso,CoordenadorLaboratorio,Reitoria")] // quem pode rejeitar
        public async Task<IActionResult> Rejeitar(int id)
        {
            try
            {
                var sucesso = await _service.RejeitarAsync(id);
                if (!sucesso)
                    return NotFound("Reserva não encontrada.");

                return Ok("Reserva rejeitada com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
