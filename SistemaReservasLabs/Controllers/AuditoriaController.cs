using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservasLabs.Data;
using SistemaReservasLabs.Models.Entities;

namespace SistemaReservasLabs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuditoriaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("historico")]
    [Authorize(Roles = "Auditor")]
    public async Task<IActionResult> ObterHistoricoReservas()
    {
        try
        {
            var historico = await _context.HistoricoReservas.ToListAsync();
            return Ok(historico);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Erro ao obter histórico de reservas: {ex.Message}");
        }
    }
}
