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
    // FECHAS EXCLUIDAS (BASE FUTURA)
    // =========================================

    private static DateTime ObtenerInicioPeriodo(
        int anio,
        int periodo)
    {
        return ISOWeek.ToDateTime(
            anio,
            periodo,
            DayOfWeek.Monday);
    }

    private async Task<HashSet<DateTime>> ObtenerFechasExcluidasAsync()
    {
        // FUTURO:
        //
        // return await _context.ConfigFechasExcluidas
        //     .AsNoTracking()
        //     .Where(x => x.Activo)
        //     .Select(x => x.Fecha.Date)
        //     .ToHashSetAsync();

        return new HashSet<DateTime>();
    }

    private static bool FechaExcluida(
        DateTime fecha,
        HashSet<DateTime> fechasExcluidas)
    {
        return fechasExcluidas.Contains(fecha.Date);
    }

    // =========================================
    // CONSULTA DETALLADA
    // =========================================
[HttpGet]
public async Task<IActionResult> GetRotacion(
    [FromQuery] decimal anio,
    string? tpnhnh,
    string? ficha,
    string? departamento,
    decimal? periodo,
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
    departamento = departamento?.Trim();

    if (page < 1)
        page = 1;

    if (pageSize < 1)
        pageSize = 100;

    if (pageSize > 1000)
        pageSize = 1000;

    IQueryable<VRotacion> query = _context.VRotacions
        .AsNoTracking();

    query = query.Where(x =>
        x.Tpnhnh == "2101" ||
        x.Tpnhnh == "2102" ||
        x.Tpnhnh == "2301" ||
        x.Tpnhnh == "4101" ||
        x.Tpnhnh == "4401");

    if (!string.IsNullOrWhiteSpace(tpnhnh))
    {
        query = query.Where(x =>
            (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
    }

    query = query.Where(x =>
        (x.Dg01hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg01hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg02hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg02hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg03hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg03hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg04hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg04hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg05hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg05hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg06hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg06hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg07hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg07hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg08hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg08hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg09hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg09hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg10hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg10hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg11hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg11hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg12hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg12hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg13hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg13hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg14hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg14hh ?? "").Trim().ToUpper() == "F" ||
        (x.Dg15hh ?? "").Trim().ToUpper() == "P" ||
        (x.Dg15hh ?? "").Trim().ToUpper() == "F"
    );

    query = query.Where(x =>
        x.Añohnh == anio);

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
        $"[VRotacion] Página:{page} Registros:{result.Count} Tiempo:{sw.ElapsedMilliseconds}ms");

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

    // =========================================
    // RESUMEN DASHBOARD
    // =========================================

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

    var query = _context.VRotacions
        .AsNoTracking()
        .Where(x => x.Añohnh == anio);

    // SOLO NÓMINA MENSUAL VALIDADA
    var nominasValidas = new[]
    {
        "2101",
        "2102",
        "2301",
        "4101",
        "4401"
    };

    query = query.Where(x =>
        nominasValidas.Contains(x.Tpnhnh!));

    query = query.Where(x =>
        (x.Dg01hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg01hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg02hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg02hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg03hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg03hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg04hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg04hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg05hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg05hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg06hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg06hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg07hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg07hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg08hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg08hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg09hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg09hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg10hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg10hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg11hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg11hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg12hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg12hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg13hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg13hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg14hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg14hh ?? "").Trim().ToUpper() == "F"

    || (x.Dg15hh ?? "").Trim().ToUpper() == "P"
    || (x.Dg15hh ?? "").Trim().ToUpper() == "F"
);

        if (!string.IsNullOrWhiteSpace(tpnhnh))
    {
        query = query.Where(x =>
            (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
    }

    query = query.Where(x =>
        x.Añohnh == anio);

    if (mes.HasValue)
    {
        var rango = ObtenerRangoPeriodos(
            anio,
            mes.Value);

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
            x.Tpnhnh,
            x.Fichnh,
            x.Prdhnh,

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

    var fechasExcluidas =
        await ObtenerFechasExcluidasAsync();

    int permisos = 0;
    int faltas = 0;

    foreach (var item in data)
    {
        var inicioPeriodo =
            ObtenerInicioPeriodo(
                anio,
                Convert.ToInt32(item.Prdhnh));

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

        for (int i = 0; i < dias.Length; i++)
        {
            var fecha = inicioPeriodo.AddDays(i);

            if (FechaExcluida(fecha, fechasExcluidas))
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