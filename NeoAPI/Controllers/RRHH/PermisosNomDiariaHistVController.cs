using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;
using System.Diagnostics;

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
    // FECHAS EXCLUIDAS (BASE FUTURA)
    // ======================================

    private static DateTime ObtenerInicioPeriodo(
        int anio,
        int periodo)
    {
        return ISOWeek.ToDateTime(
            anio,
            periodo,
            DayOfWeek.Monday);
    }

    // ======================================
    // CONSULTA DETALLADA
    // ======================================

    [HttpGet]
    public async Task<IActionResult> GetPermisos(
        string? ciahnh,
        string? tpnhnh,
        [FromQuery] decimal anio,
        decimal? periodo,
        string? ficha,
        string? departamento,
        int page = 1,
        int pageSize = 5000)
    {
        if (anio <= 0)
        {
            return BadRequest(new
            {
                Mensaje = "Debe seleccionar un año para realizar la consulta."
            });
        }

        ficha = ficha?.Trim();

        var query = _context.PermisosNomDiariaHistVs
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

            query = query.Where(x =>
        x.Añohnh == anio);

        if (periodo.HasValue)
        {
            query = query.Where(x =>
                x.Prdhnh == periodo.Value);
        }

        if (!string.IsNullOrWhiteSpace(ficha))
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

    // ======================================
    // RESUMEN DASHBOARD
    // ======================================

    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen(
        int anio,
        string? tpnhnh,
        int? mes = null)
    {
        if (anio <= 0)
        {
            return BadRequest(new
            {
                Mensaje = "Debe seleccionar un año para realizar la consulta."
            });
        }
        var query = _context.PermisosNomDiariaHistVs
        .AsNoTracking();

        query = query.Where(x => x.Añohnh == anio);

        if (!string.IsNullOrWhiteSpace(tpnhnh))
        {
            query = query.Where(x =>
                (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
        }

        if (mes.HasValue)
        {
var rango = ObtenerRangoPeriodos(anio, mes.Value);

Console.WriteLine(
    $"Año:{anio} Mes:{mes.Value} Inicio:{rango.Inicio} Fin:{rango.Fin}");

            if (rango.Fin >= rango.Inicio)
            {
                query = query.Where(x =>
                    x.Prdhnh >= rango.Inicio &&
                    x.Prdhnh <= rango.Fin);
            }
            else
            {
                query = query.Where(x =>
                    x.Prdhnh >= rango.Inicio ||
                    x.Prdhnh <= rango.Fin);
            }
        }

        var data = await query
            .Select(x => new
            {
                x.Prdhnh,

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
            if (item.Prdhnh == null)
                continue;

            var inicioPeriodo =
                ObtenerInicioPeriodo(
                    (int)anio,
                    Convert.ToInt32(item.Prdhnh));

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

            for (int i = 0; i < dias.Length; i++)
            {
                var fecha =
                    inicioPeriodo.AddDays(i);

                // DG01HH no se cuenta
                if (i == 0)
                    continue;

                var valor = (dias[i] ?? "")
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