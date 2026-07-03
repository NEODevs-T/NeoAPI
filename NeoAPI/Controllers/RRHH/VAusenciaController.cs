using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class VAusenciaController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public VAusenciaController(DbRRHHContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAusencias(
        string? compania,
        string? tipoNomina,
        string? ficha,
        string? departamento,
        decimal? periodo,
        string? grupo)
    {
        var query = _context.VAusencias.AsQueryable();

        if (!string.IsNullOrWhiteSpace(compania))
        {
            query = query.Where(x =>
                (x.Ciahnh ?? "").Trim() == compania.Trim());
        }

        if (!string.IsNullOrWhiteSpace(tipoNomina))
        {
            query = query.Where(x =>
                (x.Tpnhnh ?? "").Trim() == tipoNomina.Trim());
        }

        if (!string.IsNullOrWhiteSpace(ficha))
        {
            query = query.Where(x =>
                (x.Fichnh ?? "").Trim() == ficha.Trim());
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                (x.Dptdnh ?? "").Trim() == departamento.Trim());
        }

        if (periodo.HasValue)
        {
            query = query.Where(x =>
                x.Prddnh == periodo.Value);
        }

        if (!string.IsNullOrWhiteSpace(grupo))
        {
            query = query.Where(x =>
                (x.Gpohnh ?? "").Trim() == grupo.Trim());
        }

        var result = await query
            .Select(x => new VAusenciaDTO
            {
                Candnh = x.Candnh,
                Ctoddh = x.Ctoddh,
                Ciahnh = (x.Ciahnh ?? "").Trim(),
                Tpnhnh = (x.Tpnhnh ?? "").Trim(),
                Fichnh = (x.Fichnh ?? "").Trim(),
                Gpohnh = (x.Gpohnh ?? "").Trim(),
                Cgphnh = x.Cgphnh,
                Ctodnh = x.Ctodnh,
                Prddnh = x.Prddnh,
                Dptdnh = (x.Dptdnh ?? "").Trim()
            })
            .ToListAsync();

        if (!result.Any())
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(result);
    }
}