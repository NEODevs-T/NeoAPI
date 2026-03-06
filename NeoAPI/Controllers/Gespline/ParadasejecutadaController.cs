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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NeoAPI.DTOs.Gespline;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Globalization;
using NeoAPI.Controllers.Gespline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoAPI.Controllers.Gespline;


[ApiController]
[Route("api/[controller]")]

public class GesplineParadasEjecutadasController : ControllerBase
{
    private readonly GesplineContext _context;

    private readonly IMaquinasGesplineLogic _maquinasGesplineLogic;

    public GesplineParadasEjecutadasController(GesplineContext context, IMaquinasGesplineLogic maquinasGesplineLogic)
    {
        _context = context;
        _maquinasGesplineLogic = maquinasGesplineLogic;
    }

    [HttpGet("GetParadasActuales1Turno")]
    public async Task<ActionResult<List<ParadasActualesDTO>>> GetParadasActuales1Turno(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)
    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out int centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(59).AddSeconds(59);
        DateTime final = DateTime.Today.AddHours(18);
        try
        {
            var resultado = await
                (from pe in _context.Paradasejecutadas
                join p in _context.Paradas
                    on pe.Codigoparada equals p.Codigoparada
                join gp in _context.Gruposdeparadas
                    on p.Codigogrupoparada equals gp.Codigogrupoparada
                join part in _context.Partes
                    on pe.Codigoparada.Substring(0, 2).ToUpper().Trim() equals part.Codigo.Trim() into partJoin
                from pa in partJoin.DefaultIfEmpty()
                join ee in _context.Entradaejecucions
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions
                    on ee.Codigotupla equals te.Codigotupla
                where pe.Codigoregistrso != null &&
                    p.Nombreparada != null &&
                    gp.Codigogrupoparada != null &&
                    ee.Fechaentrada >= inicio &&
                    ee.Fechaentrada < final &&
                    !p.Codigoparada.EndsWith("0114") &&
                    te.Codigoproceso == centroCostoStr
                select new ParadasActualesDTO
                {
                    CodigoRegistro = pe.Codigoregistrso.ToString(),
                    CodigoGrupoParada = gp.Codigogrupoparada,
                    NombreParada = p.Nombreparada,
                    TiempoPerdido = (pe.Demoraparada ?? 0) * 60,
                    ParteNombre = pa != null ? pa.ParteNombre : null,
                    CodigoParte = pa != null ? pa.Codigo : null
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .ToListAsync();
            if (resultado.Count == 0)
                return NotFound($"No se encontraron registros para {centroCostoStr}. Introduzca un centroCosto válido.");
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales1TurnoAgrupados")]
    public async Task<ActionResult<List<ParadasActualesAgrupadasDTO>>> GetParadasActuales1TurnoAgrupados(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)
    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out int centroCosto))
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
        try
        {
            var query = 
                from pe in _context.Paradasejecutadas
                join ee in _context.Entradaejecucions
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions
                    on ee.Codigotupla equals te.Codigotupla
                join p in _context.Paradas
                    on pe.Codigoparada equals p.Codigoparada
                join gp in _context.Gruposdeparadas
                    on p.Codigogrupoparada equals gp.Codigogrupoparada
                join a in _context.Areas
                    on pe.Codigoparada.Substring(0, 4).Trim() equals a.AcodGes.Trim() into aJoin
                from pa in aJoin.DefaultIfEmpty()
                where pe.Codigoregistrso != null
                    && p.Nombreparada != null
                    && gp.Codigogrupoparada != null
                    && ee.Fechaentrada >= inicio
                    && ee.Fechaentrada < final
                    && !p.Codigoparada.EndsWith("0114")
                    && te.Codigoproceso == centroCostoStr  
                select new
                {
                    p.Codigoparada,
                    gp.Codigogrupoparada,
                    ACodGes = pa.AcodGes,   // puede ser null
                    p.Nombreparada,
                    Aparte = pa.Aparte,     // puede ser null
                    Minutos = (pe.Demoraparada ?? 0) * 60
                };
            var result = await query
                .GroupBy(x => new
                {
                    x.Codigoparada,
                    x.Codigogrupoparada,
                    ACodGes = x.ACodGes ?? "NULL",
                    x.Nombreparada,
                    Aparte = x.Aparte ?? "NULL"
                })
                .Select(grp => new ParadasActualesAgrupadasDTO
                {
                    CodigoParada = grp.Key.Codigoparada,
                    CodigoGrupoParada = grp.Key.Codigogrupoparada,
                    ACodGes = grp.Key.ACodGes,
                    NombreParada = grp.Key.Nombreparada,
                    Aparte = grp.Key.Aparte,
                    TiempoPerdido = grp.Sum(x => x.Minutos)
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .ToListAsync();
            if (result.Count == 0)
                return NotFound($"No se encontraron registros para {centroCostoStr}. Introduzca un centroCosto válido.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2TurnoAntesDeLas0am")]
    public async Task<ActionResult<List<ParadasActualesDTO>>> GetParadasActuales2TurnoAntesDeLas0am(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)
    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out _))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }

        DateTime inicio = DateTime.Today.AddHours(18);       // HOY 18:00
        DateTime final  = DateTime.Today.AddDays(1).Date;    // MAÑANA 00:00

        try
        {
            var resultado = await
                (from pe in _context.Paradasejecutadas.AsNoTracking()
                join p in _context.Paradas.AsNoTracking()
                    on pe.Codigoparada equals p.Codigoparada
                join gp in _context.Gruposdeparadas.AsNoTracking()
                    on p.Codigogrupoparada equals gp.Codigogrupoparada
                join part in _context.Partes.AsNoTracking()
                    on pe.Codigoparada.Substring(0, 2).ToUpper().Trim() equals part.Codigo.Trim() into partJoin
                from pa in partJoin.DefaultIfEmpty()
                join ee in _context.Entradaejecucions.AsNoTracking()
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions.AsNoTracking()
                    on ee.Codigotupla equals te.Codigotupla
                where pe.Codigoregistrso != null
                    && p.Nombreparada != null
                    && gp.Codigogrupoparada != null
                    && ee.Fechaentrada >= inicio
                    && ee.Fechaentrada < final
                    && !p.Codigoparada.EndsWith("0114")
                    && te.Codigoproceso == centroCostoStr
                select new ParadasActualesDTO
                {
                    CodigoRegistro    = pe.Codigoregistrso.ToString(),
                    CodigoGrupoParada = gp.Codigogrupoparada,
                    NombreParada      = p.Nombreparada,
                    TiempoPerdido     = (pe.Demoraparada ?? 0) * 60,
                    ParteNombre       = pa != null ? pa.ParteNombre : null,
                    CodigoParte       = pa != null ? pa.Codigo : null
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .ToListAsync();

            if (resultado.Count == 0)
                return NotFound($"No se encontraron registros para {centroCostoStr}. Introduzca un centroCosto válido.");

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2TurnoAntesDeLas0amAgrupadas")]
    public async Task<ActionResult<List<ParadasActualesAgrupadasDTO>>> GetParadasActuales2TurnoAntesDeLas0AmAgrupadas(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)
    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out _))
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");

        DateTime inicio = DateTime.Today.AddHours(18);       // HOY 18:00
        DateTime final  = DateTime.Today.AddDays(1).Date;    // MAÑANA 00:00

        try
        {
            var query =
                from pe in _context.Paradasejecutadas.AsNoTracking()
                join ee in _context.Entradaejecucions.AsNoTracking()
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions.AsNoTracking()
                    on ee.Codigotupla equals te.Codigotupla
                join p in _context.Paradas.AsNoTracking()
                    on pe.Codigoparada equals p.Codigoparada
                join gp in _context.Gruposdeparadas.AsNoTracking()
                    on p.Codigogrupoparada equals gp.Codigogrupoparada
                join a in _context.Areas.AsNoTracking()
                    on pe.Codigoparada.Substring(0, 4).Trim() equals a.AcodGes.Trim() into aJoin
                from pa in aJoin.DefaultIfEmpty()
                where pe.Codigoregistrso != null
                && p.Nombreparada != null
                && gp.Codigogrupoparada != null
                && ee.Fechaentrada >= inicio
                && ee.Fechaentrada < final
                && !p.Codigoparada.EndsWith("0114")
                && te.Codigoproceso == centroCostoStr  
                select new
                {
                    p.Codigoparada,
                    gp.Codigogrupoparada,
                    ACodGes = pa.AcodGes,
                    p.Nombreparada,
                    Aparte  = pa.Aparte,  
                    Minutos = (pe.Demoraparada ?? 0) * 60 
                };

            var result = await query
                .GroupBy(x => new
                {
                    x.Codigoparada,
                    x.Codigogrupoparada,
                    ACodGes = x.ACodGes ?? "NULL",
                    x.Nombreparada,
                    Aparte  = x.Aparte ?? "NULL"
                })
                .Select(grp => new ParadasActualesAgrupadasDTO
                {
                    CodigoParada      = grp.Key.Codigoparada,
                    CodigoGrupoParada = grp.Key.Codigogrupoparada,
                    ACodGes           = grp.Key.ACodGes,
                    NombreParada      = grp.Key.Nombreparada,
                    Aparte            = grp.Key.Aparte,
                    TiempoPerdido     = grp.Sum(x => x.Minutos)
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .ToListAsync();

            if (result.Count == 0)
                return NotFound($"No se encontraron registros para {centroCostoStr}. Introduzca un centroCosto válido.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2TurnoDespuesDeLas0am")]
    public async Task<ActionResult<List<ParadasActualesDTO>>> GetParadasActuales2TurnoDespuesDeLas0am(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)

    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out _))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }

        DateTime inicio = DateTime.Today.AddDays(1).Date;        // 00:00 de mañana
        DateTime final  = DateTime.Today.AddDays(1).AddHours(6); // 06:00 de mañana

        try
        {
            var resultado = await
                (from pe in _context.Paradasejecutadas.AsNoTracking()
                join p in _context.Paradas.AsNoTracking()
                    on pe.Codigoparada equals p.Codigoparada
                join gp in _context.Gruposdeparadas.AsNoTracking()
                    on p.Codigogrupoparada equals gp.Codigogrupoparada
                join part in _context.Partes.AsNoTracking()
                    on pe.Codigoparada.Substring(0, 2).ToUpper().Trim() equals part.Codigo.Trim() into partJoin
                from pa in partJoin.DefaultIfEmpty()
                join ee in _context.Entradaejecucions.AsNoTracking()
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions.AsNoTracking()
                    on ee.Codigotupla equals te.Codigotupla
                where pe.Codigoregistrso != null
                    && p.Nombreparada != null
                    && gp.Codigogrupoparada != null
                    && ee.Fechaentrada >= inicio
                    && ee.Fechaentrada < final
                    && !p.Codigoparada.EndsWith("0114")
                    && te.Codigoproceso == centroCostoStr
                select new ParadasActualesDTO
                {
                    CodigoRegistro    = pe.Codigoregistrso.ToString(),
                    CodigoGrupoParada = gp.Codigogrupoparada,
                    NombreParada      = p.Nombreparada,
                    TiempoPerdido     = (pe.Demoraparada ?? 0) * 60,
                    ParteNombre       = pa != null ? pa.ParteNombre : null,
                    CodigoParte       = pa != null ? pa.Codigo : null
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .ToListAsync();

            if (resultado.Count == 0)
                return NotFound($"No se encontraron registros para {centroCostoStr} en la ventana {inicio:yyyy-MM-dd HH:mm} — {final:yyyy-MM-dd HH:mm}.");

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2TurnoDespuesDeLas0amAgrupadas")]
    public async Task<ActionResult<List<ParadasActualesAgrupadasDTO>>> GetParadasActuales2TurnoDespuesDeLas0amAgrupadas(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)
    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out _))
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");

     DateTime inicio = DateTime.Today.AddDays(1).Date;        // 00:00 de mañana 
        DateTime final  = DateTime.Today.AddDays(1).AddHours(6); // 06:00 de mañana

        try
        {
            var query =
                from pe in _context.Paradasejecutadas.AsNoTracking()
                join ee in _context.Entradaejecucions.AsNoTracking()
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions.AsNoTracking()
                    on ee.Codigotupla equals te.Codigotupla
                join p in _context.Paradas.AsNoTracking()
                    on pe.Codigoparada equals p.Codigoparada
                join gp in _context.Gruposdeparadas.AsNoTracking()
                    on p.Codigogrupoparada equals gp.Codigogrupoparada
                join a in _context.Areas.AsNoTracking()
                    on pe.Codigoparada.Substring(0, 4).Trim() equals a.AcodGes.Trim() into aJoin
                from pa in aJoin.DefaultIfEmpty()
                where pe.Codigoregistrso != null
                && p.Nombreparada != null
                && gp.Codigogrupoparada != null
                && ee.Fechaentrada >= inicio
                && ee.Fechaentrada < final
                && !p.Codigoparada.EndsWith("0114")
                && te.Codigoproceso == centroCostoStr  
                select new
                {
                    p.Codigoparada,
                    gp.Codigogrupoparada,
                    ACodGes = pa.AcodGes,
                    p.Nombreparada,
                    Aparte  = pa.Aparte,  
                    Minutos = (pe.Demoraparada ?? 0) * 60 
                };

            var result = await query
                .GroupBy(x => new
                {
                    x.Codigoparada,
                    x.Codigogrupoparada,
                    ACodGes = x.ACodGes ?? "NULL",
                    x.Nombreparada,
                    Aparte  = x.Aparte ?? "NULL"
                })
                .Select(grp => new ParadasActualesAgrupadasDTO
                {
                    CodigoParada      = grp.Key.Codigoparada,
                    CodigoGrupoParada = grp.Key.Codigogrupoparada,
                    ACodGes           = grp.Key.ACodGes,
                    NombreParada      = grp.Key.Nombreparada,
                    Aparte            = grp.Key.Aparte,
                    TiempoPerdido     = grp.Sum(x => x.Minutos)
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .ToListAsync();

            if (result.Count == 0)
                return NotFound($"No se encontraron registros para {centroCostoStr}. Introduzca un centroCosto válido.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2Turno")]
    public async Task<ActionResult<List<List<string>>>> GetParadasActuales2Turno(
        [FromQuery(Name = "centroCosto")] string? centroCostoStr)
    {
        centroCostoStr = centroCostoStr?.Trim();
        if (!int.TryParse(centroCostoStr, out _))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }

        var ahora = DateTime.Now; 

        try
        {
            ActionResult<List<ParadasActualesDTO>> actionResult;

            if (ahora.Hour < 6)
            {
                actionResult = await GetParadasActuales2TurnoDespuesDeLas0am(centroCostoStr);
            }
            else if (ahora.Hour >= 18)
            {
                actionResult = await GetParadasActuales2TurnoAntesDeLas0am(centroCostoStr);
            }
            else
            {
                return NotFound("El servicio está disponible solo durante los turnos definidos (00:00–06:00 y 18:00–23:59).");
            }

            if (actionResult.Result is ObjectResult orr)
            {
                var status = orr.StatusCode ?? StatusCodes.Status500InternalServerError;
                return StatusCode(status, orr.Value);
            }

            var lista = actionResult.Value;
            if (lista is null || !lista.Any())
                return NotFound($"No se encontraron registros para {centroCostoStr}.");

            var resultado = lista.Select(dto => new List<string>
            {
                dto.CodigoRegistro ?? string.Empty,
                dto.CodigoGrupoParada ?? string.Empty,
                dto.NombreParada ?? string.Empty,
                Math.Round(dto.TiempoPerdido, 2).ToString(CultureInfo.InvariantCulture),
                dto.ParteNombre ?? string.Empty,
                dto.CodigoParte ?? string.Empty
            }).ToList();

            if (!resultado.Any())
                return NotFound($"No se encontraron registros para {centroCostoStr}.");

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetPrimeraParadaPorLinea")]
    public async Task<ActionResult<PrimeraParadaPorLineaDTO>> GetPrimeraParadaPorLinea()
    {
        var horaActual = DateTime.Now.Hour;
        ActionResult<List<string>> resultadoTurno;
        if (horaActual >= 6 && horaActual < 18)
        {
            resultadoTurno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos1Turno();
        }
        else if (horaActual >= 18 && horaActual < 22)
        {
            resultadoTurno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2TurnoAntes0am();
        }
        else
        {
            resultadoTurno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2TurnoDespues0am();
        }
        List<string> turnoList = null;
        if (resultadoTurno.Result is OkObjectResult okResult)
        {
            turnoList = okResult.Value as List<string>;
        }
        else
        {
            turnoList = resultadoTurno.Value;
        }
        if (turnoList == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "El resultado del turno es nulo o inválido.");
        }
        try
        {
            var registro = await (
    from pe in _context.Paradasejecutadas
    join ee in _context.Entradaejecucions
        on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
    join te in _context.Tuplaejecucions
        on ee.Codigotupla equals te.Codigotupla
    join tw in _context.Transmicionwebs
        on ee.Codigoentradaejecucion equals tw.Codigoentradaejecucion
    orderby pe.Fechayhoraparada
    select new PrimeraParadaPorLineaDTO
    {
        CodigoProceso = te.Codigoproceso,
        FechaYHoraParada = pe.Fechayhoraparada,
        Timespan = pe.Timespan
    })
    .GroupBy(dto => dto.CodigoProceso)
    .Select(grupo => grupo.OrderBy(dto => dto.FechaYHoraParada).FirstOrDefault())
    .ToListAsync();
            if (registro == null)
            {
                return NotFound("No se encontró registro para el proceso especificado.");
            }
            return Ok(registro);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }
}   
