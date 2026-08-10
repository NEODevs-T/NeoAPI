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
        ficha = ficha?.Trim();

        var query = _context.PermisosNomDiariaHistVs
            .AsNoTracking()
            .AsQueryable();

        // SOLO NOMINA DIARIA (TPNHNH = 1101)
        query = query.Where(x =>
            !string.IsNullOrWhiteSpace(x.Fichnh));

        // SOLO PERSONAS CON P O F
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
            (x.Dg07hh ?? "").Trim().ToUpper() == "F");

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
                (x.Fichnh ?? "").Trim() == ficha);
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                (x.Dpthnh ?? "").Trim() == departamento.Trim());
        }

        query = query.Where(x =>
            x.Tpnhnh == "1101");

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

        // SOLO NOMINA DIARIA
        query = query.Where(x =>
            x.Tpnhnh == "1101");

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
        );


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
            .ToListAsync();

        var fechasExcluidas =
            await ObtenerFechasExcluidasAsync();

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

                if (FechaExcluida(
                    fecha,
                    fechasExcluidas))
                {
                    continue;
                }

                // Se mantiene la lógica actual:
                // DG01HH (01/01) no se cuenta.

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