using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models.Gespline;
using NeoAPI.ModelsDOCIng;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic.Global;
using NeoAPI.Controllers.Maestras;
using NeoAPI.Interface;
using NeoAPI.Models.PolybaseBPCSVen;
using System.Drawing.Drawing2D;
using NeoAPI.DTOs.OEE;

namespace NeoAPI.Controllers.OEE;


[ApiController]
[Route("api/[controller]")]

public class OEEController : ControllerBase
{
    private readonly PolybaseBPCSVenContext _context;

    public OEEController(PolybaseBPCSVenContext context)
    {
        _context = context;
    }
    [HttpGet("GetMaquinaProductosProduccionActual1Turno")]
    public async Task<ActionResult<List<MaquinaProduccionDTO>>> GetMaquinaProductosProduccionActual1Turno()
    {
        var today = DateTime.Today;
        int fechaActualInt = today.Year * 10000 + today.Month * 100 + today.Day;
        decimal fechaActual = fechaActualInt;
        int inicioNum = 6000;
        int finalNum = 180000;
        var registros = await _context.Iths
            .AsNoTracking()
            .Where(th => th.Ttype == "R" &&
                        th.Ttdte == fechaActual &&
                        th.Twhs.Trim() == "PT" &&
                        th.Thtime >= inicioNum &&
                        th.Thtime < finalNum)
            .Select(th => new
            {
                th.Thwrkc,
                Tprod = th.Tprod.Trim(),
                th.Tqty
            })
            .ToListAsync();
        var resultado = registros
            .GroupBy(x => new { x.Thwrkc, x.Tprod })
            .OrderBy(g => g.Key.Thwrkc)
            .Select(g => new MaquinaProduccionDTO
            {
                Thwrkc = g.Key.Thwrkc,
                Tprod = g.Key.Tprod,
                Produccion = g.Sum(x => x.Tqty)
            })
            .ToList();

        return Ok(resultado);
    }
}   