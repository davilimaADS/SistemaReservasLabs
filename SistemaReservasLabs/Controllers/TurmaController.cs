using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReservasLabs.DTOs.Turma;
using SistemaReservasLabs.Services.Turma;

namespace SistemaReservasLabs.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "CoordenadorCurso, CoordenadorLaboratorio")]
public class TurmaController : ControllerBase
{
    private readonly ITurmaService _turmaService;
    public TurmaController(ITurmaService service) 
    {
        _turmaService = service;
    }

    [HttpPost("criar")]
    public async Task<ActionResult<TurmaDTO>> CriarTurma([FromBody] CriarTurmaDTO dto)
    {
        try
        {
            var turmaCriada = await _turmaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = turmaCriada!.Id }, turmaCriada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Erro ao criar turma: {ex.Message}");
        }
    }

    [HttpPut("atualizar/{id}")]
    public async Task<ActionResult<TurmaDTO>> Atualizar(int id, [FromBody] AtualizarTurmaDTO dto)
    {
        try
        {
            var turmaAtualizada = await _turmaService.AtualizarAsync(id, dto);
            if (turmaAtualizada == null)
                return NotFound();

            return Ok(turmaAtualizada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Erro ao atualizar turma: {ex.Message}");
        }
    }

    [HttpGet("obterPorId/{id}")]
    public async Task<ActionResult<TurmaDTO>> ObterPorId(int id)
    {
        try
        {
            var turma = await _turmaService.ObterPorIdAsync(id);
            if (turma == null)
                return NotFound();

            return Ok(turma);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Erro ao obter turma: {ex.Message}");
        }
    }

    [HttpGet("obterTodas")]
    public async Task<ActionResult<List<TurmaDTO>>> ObterTodas()
    {
        try
        {
            var turmas = await _turmaService.ObterTodasAsync();
            return Ok(turmas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Erro ao obter turmas: {ex.Message}");
        }
    }

    [HttpDelete("deletar/{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            var sucesso = await _turmaService.DeletarAsync(id);
            if (!sucesso)
                return NotFound("Turma nao encontrada.");

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Erro ao deletar turma: {ex.Message}");
        }
    }
}
