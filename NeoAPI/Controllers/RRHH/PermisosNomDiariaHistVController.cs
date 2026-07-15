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

    // ======================================
    // CONSULTA DETALLADA CON PAGINACION
    // ======================================
    [HttpGet]
    public async Task<IActionResult> GetPermisos(
        string? ciahnh,
        string? tpnhnh,
        decimal? anio,
        decimal? periodo,
        string? ficha,
        string? departamento,
        int page = 1,
        int pageSize = 500)
    {
        page = page <= 0 ? 1 : page;

        pageSize = pageSize switch
        {
            <= 0 => 500,
            > 2000 => 2000,
            _ => pageSize
        };

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

        var totalRecords = await query.CountAsync();

        int totalPages = (int)Math.Ceiling(
            totalRecords / (double)pageSize);

        if (page > totalPages && totalPages > 0)
        {
            return BadRequest(new
            {
                Message = $"La página solicitada ({page}) excede el total de páginas disponibles ({totalPages}).",
                TotalRecords = totalRecords,
                TotalPages = totalPages
            });
        }

        var result = await query
            .OrderBy(x => x.Fichnh)
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

        if (!result.Any())
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(new PagedResponse<PermisosNomDiariaHistVDTO>
        {
            Page = page,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Data = result
        });
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
            query = query.Where(x =>
                x.Prdhnh == mes.Value);
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
        int vacaciones = 0;
        int libres = 0;
        int utilidades = 0;

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

                switch (valor)
                {
                    case "P":
                        permisos++;
                        break;

                    case "F":
                        faltas++;
                        break;

                    case "V":
                        vacaciones++;
                        break;

                    case "L":
                        libres++;
                        break;

                    case "U":
                        utilidades++;
                        break;
                }
            }
        }

        var repososQuery = _context.RepososVs
            .AsNoTracking()
            .Where(x =>
                x.FechaDesde != null &&
                x.FechaDesde.Value.Year == (int)anio);

        if (mes.HasValue)
        {
            repososQuery = repososQuery.Where(x =>
                x.FechaDesde != null &&
                x.FechaDesde.Value.Month == mes.Value);
        }

        int reposos = await repososQuery
            .SumAsync(x => x.CanDiasReposo ?? 0);

        int ausencias = permisos + faltas + reposos;

        return Ok(new IndicadoresResumenDTO
        {
            Anio = anio,
            Mes = mes,
            Permisos = permisos,
            Faltas = faltas,
            Vacaciones = vacaciones,
            Libres = libres,
            Reposos = reposos,
            Ausencias = ausencias,
            Utilidades = utilidades,
            TotalRegistros = data.Count
        });
    }
}