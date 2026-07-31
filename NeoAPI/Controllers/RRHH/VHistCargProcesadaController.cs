using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.RRHHModels;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class VHistCargProcesadasController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public VHistCargProcesadasController(DbRRHHContext context)
    {
        _context = context;
    }

    // =========================================
    // RESUMEN
    // =========================================

    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen()
    {
        var totalPersonas = await _context.VHistCargProcesadas
            .AsNoTracking()
            .Select(x => x.Codfic.Trim())
            .Distinct()
            .CountAsync();

        return Ok(new
        {
            TotalPersonas = totalPersonas
        });
    }

    // =========================================
    // DETALLE
    // =========================================

    [HttpGet]
    public async Task<IActionResult> GetDetalle(string? ficha = null)
    {
        ficha = ficha?.Trim();

        var query = _context.VHistCargProcesadas
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(ficha))
        {
            query = query.Where(x =>
                x.Codfic.Trim() == ficha);
        }

        var data = await query
            .Select(x => new
            {
                Ciafic = x.Ciafic.Trim(),
                Tpnfic = x.Tpnfic.Trim(),
                Codfic = x.Codfic.Trim(),
                Nacfic = x.Nacfic.Trim(),
                Cedfic = x.Cedfic.Trim(),
                Diadlf = x.Diadlf.Trim(),
                Divdlf = x.Divdlf.Trim(),
                Diidlf = x.Diidlf.Trim(),
                Dimdlf = x.Dimdlf.Trim(),
                Dmedlf = x.Dmedlf.Trim(),
                Otddlf = x.Otddlf.Trim(),
                Dgadlf = x.Dgadlf.Trim()
            })
            .OrderBy(x => x.Codfic)
            .ToListAsync();

        return Ok(data);
    }
}