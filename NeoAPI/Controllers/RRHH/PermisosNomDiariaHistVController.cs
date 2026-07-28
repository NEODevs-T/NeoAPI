using System.Globalization;
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

    private static (int Inicio, int Fin) ObtenerRangoPeriodos(
        int anio,
        int mes)
    {
        var primerDiaMes = new DateTime(anio, mes, 1);
        var ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);

        int inicio = ISOWeek.GetWeekOfYear(primerDiaMes);
        int fin = ISOWeek.GetWeekOfYear(ultimoDiaMes);

        return (inicio, fin);
    }

    // ======================================
    // CONSULTA DETALLADA
    // ======================================
    [HttpGet]
    public async Task<IActionResult> GetPermisos(
        string? ciahnh,
        string? tpnhnh,
        decimal? anio,
        decimal? periodo,
        string? ficha,
        string? departamento)
    {
        var query = _context.PermisosNomDiariaHistVs
            .AsNoTracking()
            .AsQueryable();

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

        if (anio.HasValue)
        {
            query = query.Where(x =>
                x.Añohnh == anio.Value);
        }

        if (periodo.HasValue)
        {
            query = query.Where(x =>
                x.Prdhnh == periodo.Value);
        }

        if (!string.IsNullOrWhiteSpace(ficha))
        {
            query = query.Where(x =>
                x.Fichnh == ficha);
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                x.Dpthnh == departamento);
        }

        var result = await query
            .OrderBy(x => x.Añohnh)
            .ThenBy(x => x.Prdhnh)
            .ThenBy(x => x.Fichnh)
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
            return NotFound("No se encontraron registros.");
        }

        return Ok(result);
    }

    // ======================================
    // RESUMEN DASHBOARD
    // ======================================
    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen(
        decimal anio,
        int? mes = null)
    {
        var query = _context.PermisosNomDiariaHistVs
            .AsNoTracking()
            .Where(x => x.Añohnh == anio);

        if (mes.HasValue)
        {
            var rango = ObtenerRangoPeriodos(
                (int)anio,
                mes.Value);

            query = query.Where(x =>
                x.Prdhnh >= rango.Inicio &&
                x.Prdhnh <= rango.Fin);
        }

        var data = await query
            .Select(x => new
            {
                x.Dg01hh,
                x.Dg02hh,
                x.Dg03hh,
                x.Dg04hh,
                x.Dg05hh,
                x.Dg06hh,
                x.Dg07hh
            })
            .ToListAsync();

        int permisos = 0;
        int faltas = 0;

        foreach (var item in data)
        {
            var dias = new[]
            {
                item.Dg01hh,
                item.Dg02hh,
                item.Dg03hh,
                item.Dg04hh,
                item.Dg05hh,
                item.Dg06hh,
                item.Dg07hh
            };

            foreach (var dia in dias)
            {
                var valor = (dia ?? "")
                    .Trim()
                    .ToUpper();

                if (valor == "P")
                {
                    permisos++;
                }
                else if (valor == "F")
                {
                    faltas++;
                }
            }
        }

        return Ok(new IndicadoresResumenDTO
        {
            Anio = anio,
            Mes = mes,
            Permisos = permisos,
            Faltas = faltas
        });
    }
}