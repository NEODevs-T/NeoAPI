using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;
using System.Diagnostics;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class PermisosNomDiariaVController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public PermisosNomDiariaVController(DbRRHHContext context)
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
        string? departamento,
        int page = 1,
        int pageSize = 5000)
    {
        ficha = ficha?.Trim();

        bool consultaPorPeriodo =
            anio.HasValue &&
            anio.Value > 0 &&
            periodo.HasValue;

        bool consultaPorFicha =
            !string.IsNullOrWhiteSpace(ficha);

        if (!consultaPorPeriodo && !consultaPorFicha)
        {
            return BadRequest(new
            {
                Mensaje = "Debe indicar Año y Período o una Ficha para realizar la consulta."
            });
        }

        var query = _context.PermisosNomDiariaVs
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(ciahnh))
        {
            query = query.Where(x =>
                (x.Ciahnh ?? "").Trim() == ciahnh.Trim());
        }

        if (!string.IsNullOrWhiteSpace(tpnhnh))
        {
            query = query.Where(x =>
                (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
        }

        if (consultaPorPeriodo)
        {
            query = query.Where(x =>
                x.Añohnh == anio!.Value &&
                x.Prdhnh == periodo!.Value);
        }

        if (consultaPorFicha)
        {
            query = query.Where(x =>
                (x.Fichnh ?? "").Trim() == ficha);
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                (x.Dpthnh ?? "").Trim() == departamento.Trim());
        }

        var sw = Stopwatch.StartNew();

        var totalRegistros = await query.CountAsync();

        var result = await query
            .OrderBy(x => x.Añohnh)
            .ThenBy(x => x.Prdhnh)
            .ThenBy(x => x.Fichnh)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PermisosNomDiariaVDTO
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

        sw.Stop();

        if (!result.Any())
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(new
        {
            TotalRegistros = totalRegistros,
            PaginaActual = page,
            TamanioPagina = pageSize,
            TotalPaginas = (int)Math.Ceiling(
                (double)totalRegistros / pageSize),
            TiempoMs = sw.ElapsedMilliseconds,
            Data = result
        });
    }
}