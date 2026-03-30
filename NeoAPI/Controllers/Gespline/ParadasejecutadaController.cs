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
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(54).AddSeconds(59);
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
                    pe.Fechayhoraparada >= inicio && 
                    pe.Fechayhoraparada < final &&
                    !p.Codigoparada.EndsWith("0114") &&
                    te.Codigoproceso == centroCostoStr
                select new ParadasActualesDTO
                {
                    CodigoRegistro = pe.Codigoregistrso.ToString(),
                    CodigoGrupoParada = gp.Codigogrupoparada,
                    NombreParada = p.Nombreparada,
                    TiempoPerdido = (pe.Demoraparada ?? 0) * 60,
                    ParteNombre = pa != null ? pa.ParteNombre : "Equipo no Registrado",
                    CodigoParte = pa != null ? pa.Codigo : "Código no Registrado"
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
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(54).AddSeconds(59);
        DateTime final = DateTime.Today.AddHours(18);
        bool centroCostoSinEquipo = centroCostoStr == "103103" || centroCostoStr == "103105" || centroCostoStr == "103106";
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
                    && pe.Fechayhoraparada >= inicio
                    && pe.Fechayhoraparada < final
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
                    ACodGes = x.ACodGes ?? "Código no Registrado",
                    x.Nombreparada,
                    Aparte = x.Aparte ?? (centroCostoSinEquipo ? "" : "Equipo no Registrado")
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
        DateTime ahora = DateTime.Now;

        DateTime inicio;
        DateTime final;

        if (ahora.Hour > 17 || (ahora.Hour == 17 && ahora.Minute >= 55))
        {
            inicio = DateTime.Today.AddHours(17).AddMinutes(55);
            final  = DateTime.Today.AddDays(1);
        }
        else if (ahora.Hour < 6)
        {
            inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(55);
            final  = DateTime.Today;
        }
        else
        {
            return BadRequest("El segundo turno solo aplica desde las 17:55 hasta las 06:00.");
        }
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
                    && pe.Fechayhoraparada >= inicio
                    && pe.Fechayhoraparada < final
                    && !p.Codigoparada.EndsWith("0114")
                    && te.Codigoproceso == centroCostoStr
                select new ParadasActualesDTO
                {
                    CodigoRegistro    = pe.Codigoregistrso.ToString(),
                    CodigoGrupoParada = gp.Codigogrupoparada,
                    NombreParada      = p.Nombreparada,
                    TiempoPerdido     = (pe.Demoraparada ?? 0) * 60,
                    ParteNombre       = pa != null ? pa.ParteNombre : "Equipo no Registrado",
                    CodigoParte       = pa != null ? pa.Codigo : "Código no Registrado"
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
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime ahora = DateTime.Now;

        DateTime inicio;
        DateTime final;

        if (ahora.Hour > 17 || (ahora.Hour == 17 && ahora.Minute >= 55))
        {
            inicio = DateTime.Today.AddHours(17).AddMinutes(55);
            final  = DateTime.Today.AddDays(1);
        }
        else if (ahora.Hour < 6)
        {
            inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(55);
            final  = DateTime.Today;
        }
        else
        {
            return BadRequest("El segundo turno solo aplica desde las 17:55 hasta las 06:00.");
        }
        bool centroCostoSinEquipo = centroCostoStr == "103103" || centroCostoStr == "103105" || centroCostoStr == "103106";
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
                && pe.Fechayhoraparada >= inicio
                && pe.Fechayhoraparada < final
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
                    ACodGes = x.ACodGes ?? "Código no Registrado",
                    x.Nombreparada,
                    Aparte = x.Aparte ?? (centroCostoSinEquipo ? "" : "Equipo no Registrado")
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
        DateTime ahora = DateTime.Now;

        DateTime inicio;
        DateTime final;

        if (ahora.Hour < 6)
        {
            inicio = DateTime.Today;           // HOY 00:00
            final  = DateTime.Today.AddHours(6); // HOY 06:00
        }
        else
        {
            return BadRequest("Este método solo aplica de 00:00 a 06:00.");
        }
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
                    && pe.Fechayhoraparada >= inicio
                    && pe.Fechayhoraparada < final
                    && !p.Codigoparada.EndsWith("0114")
                    && te.Codigoproceso == centroCostoStr
                select new ParadasActualesDTO
                {
                    CodigoRegistro    = pe.Codigoregistrso.ToString(),
                    CodigoGrupoParada = gp.Codigogrupoparada,
                    NombreParada      = p.Nombreparada,
                    TiempoPerdido     = (pe.Demoraparada ?? 0) * 60,
                    ParteNombre       = pa != null ? pa.ParteNombre : "Equipo no Registrado",
                    CodigoParte       = pa != null ? pa.Codigo : "Código no Registrado"
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
        DateTime ahora = DateTime.Now;

        DateTime inicio;
        DateTime final;

        if (ahora.Hour < 6)
        {
            inicio = DateTime.Today;           // HOY 00:00
            final  = DateTime.Today.AddHours(6); // HOY 06:00
        }
        else
        {
            return BadRequest("Este método solo aplica de 00:00 a 06:00.");
        }
        bool centroCostoSinEquipo = centroCostoStr == "103103" || centroCostoStr == "103105" || centroCostoStr == "103106";
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
                && pe.Fechayhoraparada >= inicio
                && pe.Fechayhoraparada < final
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
                    ACodGes = x.ACodGes ?? "Código no Registrado",
                    x.Nombreparada,
                    Aparte = x.Aparte ?? (centroCostoSinEquipo ? "" : "Equipo no Registrado")
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
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        var ahora = DateTime.Now;
        var hora = ahora.TimeOfDay;
        try
        {
            var listaFinal = new List<ParadasActualesDTO>();
            if (hora >= TimeSpan.FromHours(18))
            {
                var antes = await GetParadasActuales2TurnoAntesDeLas0am(centroCostoStr);
                if (antes.Result is ObjectResult error)
                    return StatusCode(error.StatusCode ?? 500, error.Value);
                if (antes.Value != null)
                    listaFinal.AddRange(antes.Value);
            }
            else if (hora < TimeSpan.FromHours(6))
            {
                var antes = await GetParadasActuales2TurnoAntesDeLas0am(centroCostoStr);
                var despues = await GetParadasActuales2TurnoDespuesDeLas0am(centroCostoStr);
                if (antes.Result is ObjectResult errorAntes)
                    return StatusCode(errorAntes.StatusCode ?? 500, errorAntes.Value);
                if (despues.Result is ObjectResult errorDespues)
                    return StatusCode(errorDespues.StatusCode ?? 500, errorDespues.Value);
                if (antes.Value != null)
                    listaFinal.AddRange(antes.Value);
                if (despues.Value != null)
                    listaFinal.AddRange(despues.Value);
            }
            else
            {
                return NotFound("El servicio está disponible solamente entre 18:00–05:59.");
            }
            if (!listaFinal.Any())
                return NotFound($"No se encontraron registros para {centroCostoStr}.");
            var resultado = listaFinal.Select(dto => new List<string>
            {
                dto.CodigoRegistro ?? string.Empty,
                dto.CodigoGrupoParada ?? string.Empty,
                dto.NombreParada ?? string.Empty,
                Math.Round(dto.TiempoPerdido, 2).ToString(CultureInfo.InvariantCulture),
                dto.ParteNombre ?? string.Empty,
                dto.CodigoParte ?? string.Empty
            }).ToList();
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }
    
    [HttpGet("GetPrimeraParadaPorLinea")]
    public async Task<ActionResult<List<PrimeraParadaPorLineaDTO>>> GetPrimeraParadaPorLinea()
    {
        try
        {
            var paradas = await (
                from pe in _context.Paradasejecutadas
                join ee in _context.Entradaejecucions
                    on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions
                    on ee.Codigotupla equals te.Codigotupla
                join tw in _context.Transmicionwebs
                    on ee.Codigoentradaejecucion equals tw.Codigoentradaejecucion
                where pe.Fechayhoraparada != null
                select new PrimeraParadaPorLineaDTO
                {
                    CodigoProceso = te.Codigoproceso,
                    FechaYHoraParada = pe.Fechayhoraparada,
                    Timespan = pe.Timespan
                }
            )
            .GroupBy(x => x.CodigoProceso)

            .Select(g => g
                .OrderBy(x => x.FechaYHoraParada)
                .FirstOrDefault()
            )
            .ToListAsync();

            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para las líneas.");
            }

            return Ok(paradas);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }
}   
