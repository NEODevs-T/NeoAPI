using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.RRHHModels;
using NeoAPI.DTOs.RRHH;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic.Global;
using NeoAPI.Controllers.Maestras;
using NeoAPI.Interface;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class PeriodosVController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public PeriodosVController(DbRRHHContext context)
    {
        _context = context;
    }

[HttpGet]
public async Task<IActionResult> GetPeriodos(
    string? ciafpr,
    string? tpnfpr,
    decimal? anio,
    decimal? mes,
    decimal? periodo,
    decimal? fechaInicio,
    decimal? fechaFin)
{
    var query = _context.PeriodosVs.AsQueryable();

    if (!string.IsNullOrEmpty(ciafpr))
        query = query.Where(x => x.Ciafpr == ciafpr);

    if (!string.IsNullOrEmpty(tpnfpr))
        query = query.Where(x => x.Tpnfpr == tpnfpr);

    if (anio.HasValue)
        query = query.Where(x => x.Añofpr == anio);

    if (mes.HasValue)
        query = query.Where(x => x.Mesfpr == mes);

    if (periodo.HasValue)
        query = query.Where(x => x.Prdfpr == periodo);

    // ✅ 🔥 RANGO DE FECHAS
    if (fechaInicio.HasValue)
        query = query.Where(x => x.Fecifp >= fechaInicio);

    if (fechaFin.HasValue)
        query = query.Where(x => x.Fecffp <= fechaFin);

    var result = await query
        .Select(x => new PeriodosVDTO
        {
            Ciafpr = x.Ciafpr,
            Tpnfpr = x.Tpnfpr,
            Añofpr = x.Añofpr,
            Prdfpr = x.Prdfpr,
            Fecifp = x.Fecifp,
            Fecffp = x.Fecffp,
            Mesfpr = x.Mesfpr
        })
        .ToListAsync();

    if (!result.Any())
        return NotFound("No hay periodos en ese rango");

    return Ok(result);
}

}
