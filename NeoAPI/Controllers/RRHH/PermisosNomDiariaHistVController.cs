using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;

namespace NeoAPI.Controllers.RRHH;


[ApiController]
[Route("api/[controller]")]
public class PermisosNomDiariaHistVController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public PermisosNomDiariaHistVController(DbRRHHContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPermisos(
        string? ciahnh,
        string? tpnhnh,
        decimal? anio,
        decimal? periodo,
        string? ficha,
        string? departamento)
    {
        var query = _context.PermisosNomDiariaHistVs.AsQueryable();

        // ✅ FILTROS

        if (!string.IsNullOrEmpty(ciahnh))
        {
            query = query.Where(x =>
                (x.Ciahnh ?? "").Trim() == ciahnh.Trim());
        }

        if (!string.IsNullOrEmpty(tpnhnh))
        {
            query = query.Where(x =>
                (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
        }

        if (anio.HasValue)
        {
            query = query.Where(x =>
                x.Añohnh == anio);
        }

        if (periodo.HasValue)
        {
            query = query.Where(x =>
                x.Prdhnh == periodo);
        }

        if (!string.IsNullOrEmpty(ficha))
        {
            query = query.Where(x =>
                x.Fichnh == ficha);
        }

        if (!string.IsNullOrEmpty(departamento))
        {
            query = query.Where(x =>
                x.Dpthnh == departamento);
        }

        // ✅ RESPUESTA

        var result = await query
            .Select(x => new PermisosNomDiariaHistVDTO
            {
                Ciahnh = (x.Ciahnh ?? "").Trim(),
                Tpnhnh = (x.Tpnhnh ?? "").Trim(),
                Añohnh = x.Añohnh,
                Prdhnh = x.Prdhnh,
                Fichnh = (x.Fichnh ?? "").Trim(),
                Dpthnh = (x.Dpthnh ?? "").Trim(),

                Dg01hh = (x.Dg01hh ?? "").Trim(),
                Dg02hh = (x.Dg02hh ?? "").Trim(),
                Dg03hh = (x.Dg03hh ?? "").Trim(),
                Dg04hh = (x.Dg04hh ?? "").Trim(),
                Dg05hh = (x.Dg05hh ?? "").Trim(),
                Dg06hh = (x.Dg06hh ?? "").Trim(),
                Dg07hh = (x.Dg07hh ?? "").Trim()
            })
            .ToListAsync();

        if (!result.Any())
        {
            return NotFound("No se encontraron registros");
        }

        return Ok(result);
    }
}