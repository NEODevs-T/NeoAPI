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

        // Aumentar timeout porque la vista es pesada
        _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(20));
    }

    [HttpGet]
    public async Task<IActionResult> GetRotacion(
        string? compania,
        string? tipoNomina,
        string? ficha,
        string? departamento,
        decimal? anio,
        decimal? periodo)
    {
        compania = compania?.Trim();
        tipoNomina = tipoNomina?.Trim();
        ficha = ficha?.Trim();
        departamento = departamento?.Trim();

        if (!anio.HasValue &&
            string.IsNullOrWhiteSpace(ficha))
        {
            return BadRequest(
                "Debe indicar al menos año o ficha.");
        }

        var query = _context.VRotacions
            .AsNoTracking()
            .AsQueryable();

        // Buscar primero por ficha
        if (!string.IsNullOrWhiteSpace(ficha))
        {
            query = query.Where(x => x.Fichnh == ficha);
        }

        if (anio.HasValue)
        {
            query = query.Where(x => x.Añohnh == anio.Value);
        }

        if (periodo.HasValue)
        {
            query = query.Where(x => x.Prdhnh == periodo.Value);
        }

        if (!string.IsNullOrWhiteSpace(compania))
        {
            query = query.Where(x => x.Ciahnh == compania);
        }

        if (!string.IsNullOrWhiteSpace(tipoNomina))
        {
            query = query.Where(x => x.Tpnhnh == tipoNomina);
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x => x.Dpthnh == departamento);
        }

        var result = await query
            .Take(100)
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

        if (result.Count == 0)
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(result);
    }
}