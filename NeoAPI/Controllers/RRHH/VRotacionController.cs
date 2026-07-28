using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class VRotacionController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public VRotacionController(DbRRHHContext context)
    {
        _context = context;

        // Vista pesada
        _context.Database.SetCommandTimeout(
            TimeSpan.FromMinutes(20));
    }

    private static (int Inicio, int Fin) ObtenerRangoPeriodos(
        int anio,
        int mes)
    {
        var primerDiaMes = new DateTime(anio, mes, 1);

        var ultimoDiaMes = primerDiaMes
            .AddMonths(1)
            .AddDays(-1);

        int inicio = ISOWeek.GetWeekOfYear(primerDiaMes);
        int fin = ISOWeek.GetWeekOfYear(ultimoDiaMes);

        return (inicio, fin);
    }

    // =========================================
    // CONSULTA DETALLADA
    // =========================================
    [HttpGet]
    public async Task<IActionResult> GetRotacion(
        decimal? anio,
        string? ficha,
        string? departamento,
        decimal? periodo,
        int page = 1,
        int pageSize = 100)
    {
        ficha = ficha?.Trim();
        departamento = departamento?.Trim();

        if (page < 1)
            page = 1;

        if (pageSize < 1)
            pageSize = 100;

        if (pageSize > 1000)
            pageSize = 1000;

        IQueryable<VRotacion> query = _context.VRotacions
            .AsNoTracking();

        if (anio.HasValue)
        {
            query = query.Where(x =>
                x.Añohnh == anio.Value);
        }
        else
        {
            query = query.Where(x =>
                x.Añohnh == 2026);
        }

        if (!string.IsNullOrWhiteSpace(ficha))
        {
            query = query.Where(x =>
                x.Fichnh == ficha);
        }

        if (periodo.HasValue)
        {
            query = query.Where(x =>
                x.Prdhnh == periodo.Value);
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                x.Dpthnh == departamento);
        }

        var sw = Stopwatch.StartNew();

        var totalRegistros = await query.CountAsync();

        var result = await query
            .OrderBy(x => x.Prdhnh)
            .ThenBy(x => x.Fichnh)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new VRotacionDTO
            {
                Ciahnh = x.Ciahnh ?? "",
                Tpnhnh = x.Tpnhnh ?? "",
                Fichnh = x.Fichnh ?? "",

                Dg01hh = x.Dg01hh ?? "",
                Dg02hh = x.Dg02hh ?? "",
                Dg03hh = x.Dg03hh ?? "",
                Dg04hh = x.Dg04hh ?? "",
                Dg05hh = x.Dg05hh ?? "",
                Dg06hh = x.Dg06hh ?? "",
                Dg07hh = x.Dg07hh ?? "",
                Dg08hh = x.Dg08hh ?? "",
                Dg09hh = x.Dg09hh ?? "",
                Dg10hh = x.Dg10hh ?? "",
                Dg11hh = x.Dg11hh ?? "",
                Dg12hh = x.Dg12hh ?? "",
                Dg13hh = x.Dg13hh ?? "",
                Dg14hh = x.Dg14hh ?? "",
                Dg15hh = x.Dg15hh ?? "",

                Añohnh = x.Añohnh,
                Prdhnh = x.Prdhnh,
                Dpthnh = x.Dpthnh ?? ""
            })
            .ToListAsync();

        sw.Stop();

        Console.WriteLine(
            $"[VRotacion] Página:{page} Registros:{result.Count} Total:{totalRegistros} Tiempo:{sw.ElapsedMilliseconds}ms");

        if (!result.Any())
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(new
        {
            TotalRegistros = totalRegistros,
            PaginaActual = page,
            TamanioPagina = pageSize,
            TotalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize),
            TiempoMs = sw.ElapsedMilliseconds,
            Data = result
        });
    }

    // =========================================
    // RESUMEN DASHBOARD
    // =========================================
    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen(
        int? mes = null)
    {
        var query = _context.VRotacions
            .AsNoTracking()
            .Where(x => x.Añohnh == 2026);

        if (mes.HasValue)
        {
            var rango = ObtenerRangoPeriodos(
                2026,
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
                x.Dg07hh,
                x.Dg08hh,
                x.Dg09hh,
                x.Dg10hh,
                x.Dg11hh,
                x.Dg12hh,
                x.Dg13hh,
                x.Dg14hh,
                x.Dg15hh
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
                item.Dg07hh,
                item.Dg08hh,
                item.Dg09hh,
                item.Dg10hh,
                item.Dg11hh,
                item.Dg12hh,
                item.Dg13hh,
                item.Dg14hh,
                item.Dg15hh
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
            Anio = 2026,
            Mes = mes,
            Permisos = permisos,
            Faltas = faltas
        });
    }
}