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
using NeoAPI.Models.PolybaseBPCSCen;

namespace NeoAPI.Controllers.OEE;


[ApiController]
[Route("api/[controller]")]

public class OEEController : ControllerBase
{
    private readonly PolybaseBPCSVenContext _context;

    private readonly IMaquinasGesplineLogic _maquinasGesplineLogic;

    public OEEController(PolybaseBPCSVenContext context, IMaquinasGesplineLogic maquinasGesplineLogic)
    {
        _context = context;
        _maquinasGesplineLogic = maquinasGesplineLogic;
    }

    [HttpGet("GetMaquinaProductosProduccionActual1Turno")]
    public async Task<ActionResult<List<MaquinaProduccionDTO>>> GetMaquinaProductosProduccionActual1Turno()
    {
        var today = DateTime.Today;
        int fechaActualInt = today.Year * 10000 + today.Month * 100 + today.Day;
        int inicioNum = 6000;
        int finalNum = 180000;
        List<string> maquinasActivas1Turno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos1Turno();
        var resultado = await _context.Iths
            .AsNoTracking()
            .Where(th => th.Ttype == "R" &&
                        th.Ttdte == fechaActualInt &&
                        th.Twhs.Trim() == "PT" &&
                        th.Thtime >= inicioNum &&
                        th.Thtime < finalNum &&
                        maquinasActivas1Turno.Contains(th.Thwrkc.ToString()))
            .GroupBy(th => new { th.Thwrkc, Tprod = th.Tprod.Trim() })
            .Select(g => new MaquinaProduccionDTO
            {
                Thwrkc = g.Key.Thwrkc,
                Tprod = g.Key.Tprod,
                Produccion = g.Sum(th => th.Tqty)
            })
            .OrderBy(x => x.Thwrkc)
            .ToListAsync();
        return Ok(resultado);
    }

    [HttpGet("GetMaquinaProductosProduccionActual2TurnoAntes0am")]
    public async Task<ActionResult<List<MaquinaProduccionDTO>>> GetMaquinaProductosProduccionActual2TurnoAntes0am()
    {
        var today = DateTime.Today;
        int fechaActualInt = today.Year * 10000 + today.Month * 100 + today.Day;
        int inicioNum = 180000;
        int finalNum = 235959;
        List<string> maquinasActivas2Turno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2TurnoAntes0am();
        var resultado = await (
            from ith in _context.Iths.AsNoTracking()
            join iim in _context.Iims.AsNoTracking()
                on ith.Tprod equals iim.Iprod
            where ith.Ttype == "R" &&
                ith.Ttdte == fechaActualInt &&
                ith.Twhs.Trim() == "PT" &&
                ith.Thtime >= inicioNum &&
                ith.Thtime <= finalNum &&
                maquinasActivas2Turno.Contains(ith.Thwrkc.ToString())
            group ith by new
            {
                ith.Thwrkc,
                Tprod = ith.Tprod.Trim()
            } into g
            orderby g.Key.Thwrkc
            select new MaquinaProduccionDTO
            {
                Thwrkc = g.Key.Thwrkc,
                Tprod = g.Key.Tprod,
                Produccion = g.Sum(x => x.Tqty)
            }
        ).ToListAsync();
        return Ok(resultado);
    }

    [HttpGet("GetMaquinaProductosProduccionActual2TurnoDespues0am")]
    public async Task<ActionResult<List<MaquinaProduccionDTO>>> GetMaquinaProductosProduccionActual2TurnoDespues0am()
    {
        var today = DateTime.Today;
        int fechaActualInt = today.Year * 10000 + today.Month * 100 + today.Day;
        int inicioNum = 000000;
        int finalNum = 6000;
        List<string> maquinasActivas2Turno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2TurnoDespues0am();
        var resultado = await (
            from ith in _context.Iths.AsNoTracking()
            join iim in _context.Iims.AsNoTracking()
                on ith.Tprod equals iim.Iprod
            where ith.Ttype == "R" &&
                ith.Ttdte == fechaActualInt &&
                ith.Twhs.Trim() == "PT" &&
                ith.Thtime >= inicioNum &&
                ith.Thtime <= finalNum &&
                maquinasActivas2Turno.Contains(ith.Thwrkc.ToString())
            group ith by new
            {
                ith.Thwrkc,
                Tprod = ith.Tprod.Trim()
            } into g
            orderby g.Key.Thwrkc
            select new MaquinaProduccionDTO
            {
                Thwrkc = g.Key.Thwrkc,
                Tprod = g.Key.Tprod,
                Produccion = g.Sum(x => x.Tqty)
            }
            ).ToListAsync();
            return Ok(resultado);     
    }






}   