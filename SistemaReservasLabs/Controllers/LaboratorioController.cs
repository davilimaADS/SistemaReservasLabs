using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaReservasLabs.DTOs.Laboratorio;
using SistemaReservasLabs.Services.Laboratorio;

namespace SistemaReservasLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioService _service;

        public LaboratorioController(ILaboratorioService service)
        {
            _service = service;
        }

        [Authorize(Roles = "CoordenadorLaboratorio,Reitoria")]
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarLaboratorioDTO dto)
        {
            try
            {
                var result = await _service.CriarAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var labs = await _service.ListarTodosAsync();
            return Ok(labs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var lab = await _service.ObterPorIdAsync(id);
            return lab == null ? NotFound() : Ok(lab);
        }

        [Authorize(Roles = "CoordenadorLaboratorio,Reitoria")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CriarLaboratorioDTO dto)
        {
            var atualizado = await _service.AtualizarAsync(id, dto);
            return atualizado ? NoContent() : NotFound();
        }

        [Authorize(Roles = "CoordenadorLaboratorio,Reitoria")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var deletado = await _service.DeletarAsync(id);
            return deletado ? NoContent() : NotFound();
        }
    }
}
