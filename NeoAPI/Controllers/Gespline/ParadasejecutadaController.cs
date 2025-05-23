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

namespace NeoAPI.Controllers.Gespline;


[ApiController]
[Route("api/[controller]")]

public class Gespline_ParadasejecutadaController : ControllerBase
{
    private readonly GesplineContext _context;

    private readonly IMaquinasGesplineLogic _maquinasGesplineLogic;

    public Gespline_ParadasejecutadaController(GesplineContext context, IMaquinasGesplineLogic maquinasGesplineLogic)
    {
        _context = context;
        _maquinasGesplineLogic = maquinasGesplineLogic;
    }

    [HttpGet("GetParadasActuales1Turno")]
    public async Task<ActionResult<List<ParadaActual1TurnoDTO>>> GetParadasActuales1Turno([FromQuery] string? centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
        try
        {
            var query = from pe in _context.Paradasejecutadas
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
                            ee.Fechaentrada.HasValue &&
                            ee.Fechaentrada.Value.Hour < 17 &&
                            !p.Codigoparada.EndsWith("0114") &&
                            te.Codigoproceso == centroCosto
                        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
                        select new ParadaActual1TurnoDTO
                        {
                            CodigoRegistro = pe.Codigoregistrso.ToString(),
                            CodigoGrupoParada = gp.Codigogrupoparada,
                            NombreParada = p.Nombreparada,
                            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0/ 60.0)
                                            .ToString(),
                            ParteNombre = pa != null ? pa.ParteNombre : null,
                            CodigoParte = pa != null ? pa.Codigo : null
                        };
            var resultado = await query.ToListAsync();
            if (resultado == null || !resultado.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}. Introduzca un centroCosto valido.");
            }
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales1TurnoAgrupados")]
    public async Task<ActionResult<List<ParadaActual1TurnoAgrupadoDTO>>> GetParadasActuales1TurnoAgrupados([FromQuery] string? centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
        try
        {
            var query = from pe in _context.Paradasejecutadas
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
                        where pe.Codigoregistrso != null &&
                        p.Nombreparada != null &&
                        gp.Codigogrupoparada != null &&
                        ee.Fechaentrada >= inicio &&
                        ee.Fechaentrada < final &&
                        !p.Codigoparada.EndsWith("0114") &&
                        te.Codigoproceso == centroCosto
                        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
                        select new
                        {
                            pe,
                            p,
                            gp,
                            pa,
                            TiempoPerdido = EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0/ 60.0
                        };
            var data = await query.ToListAsync();
            var result = data
                .GroupBy(x => new
                {
                    x.p.Codigoparada,
                    x.gp.Codigogrupoparada,
                    ACodGes = x.pa?.AcodGes ?? "NULL",
                    x.p.Nombreparada,
                    Aparte = x.pa?.Aparte ?? "NULL"
                })
                .Select(grp => new
                {
                    CodigoParada = grp.Key.Codigoparada,
                    CodigoGrupoParada = grp.Key.Codigogrupoparada,
                    ACodGes = grp.Key.ACodGes,
                    NombreParada = grp.Key.Nombreparada,
                    Aparte = grp.Key.Aparte,
                    TiempoPerdido = grp.Sum(x => x.TiempoPerdido)
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .Select(grp => new ParadaActual1TurnoAgrupadoDTO
                {
                    CodigoParada = grp.CodigoParada,
                    CodigoGrupoParada = grp.CodigoGrupoParada,
                    ACodGes = grp.ACodGes,
                    NombreParada = grp.NombreParada,
                    Aparte = grp.Aparte,
                    TiempoPerdido = grp.TiempoPerdido.ToString()
                });
            if (result == null || !result.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}. Introduzca un centroCosto valido.");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2turnoAntesDeLas0am")]
    public async Task<ActionResult<List<ParadasActuales2turnoDTO>>> GetParadasActuales2turnoAntesDeLas0am([FromQuery] string? centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddHours(18);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        try
        {
            var query = from pe in _context.Paradasejecutadas
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
                        ee.Fechaentrada.HasValue &&
                        ee.Fechaentrada.Value.Hour >= 17 &&
                        !p.Codigoparada.EndsWith("0114") &&
                        te.Codigoproceso == centroCosto
                        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
                        select new ParadasActuales2turnoDTO
                        {
                            CodigoRegistro = pe.Codigoregistrso.ToString(),
                            CodigoGrupoParada = gp.Codigogrupoparada,
                            NombreParada = p.Nombreparada,
                            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0/ 60.0)
                                        .ToString(),
                            ParteNombre = pa != null ? pa.ParteNombre : null,
                            CodigoParte = pa != null ? pa.Codigo : null
                        };
            var resultado = await query.ToListAsync();
            if (resultado == null || !resultado.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}. Introduzca un centroCosto valido.");
            }
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2turnoAntesDeLas0amAgrupadas")]
    public async Task<ActionResult<List<ParadasActuales2turnoAntesDeLas0amAgrupadasDTO>>> GetParadasActuales2TurnoAntesDeLas0AmAgrupadas([FromQuery] string? centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddHours(18);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        try
        {
            var query = from pe in _context.Paradasejecutadas
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
                        where pe.Codigoregistrso != null &&
                        p.Nombreparada != null &&
                        gp.Codigogrupoparada != null &&
                        ee.Fechaentrada >= inicio &&
                        ee.Fechaentrada < final &&
                        !p.Codigoparada.EndsWith("0114") &&
                        te.Codigoproceso == centroCosto
                        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
                        select new
                        {
                            pe,
                            p,
                            gp,
                            pa,
                            TiempoPerdido = EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0/ 60.0
                        };
            var data = await query.ToListAsync();
            var result = data
                .GroupBy(x => new
                {
                    x.p.Codigoparada,
                    x.gp.Codigogrupoparada,
                    ACodGes = x.pa?.AcodGes ?? "NULL",
                    x.p.Nombreparada,
                    Aparte = x.pa?.Aparte ?? "NULL"
                })
                .Select(grp => new
                {
                    CodigoParada = grp.Key.Codigoparada,
                    CodigoGrupoParada = grp.Key.Codigogrupoparada,
                    ACodGes = grp.Key.ACodGes,
                    NombreParada = grp.Key.Nombreparada,
                    Aparte = grp.Key.Aparte,
                    TiempoPerdido = grp.Sum(x => x.TiempoPerdido)
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .Select(grp => new ParadasActuales2turnoAntesDeLas0amAgrupadasDTO
                {
                    CodigoParada = grp.CodigoParada,
                    CodigoGrupoParada = grp.CodigoGrupoParada,
                    ACodGes = grp.ACodGes,
                    NombreParada = grp.NombreParada,
                    Aparte = grp.Aparte,
                    TiempoPerdido = grp.TiempoPerdido.ToString()
                });
            if (result == null || !result.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}. Introduzca un centroCosto valido.");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2turnoDespuesDeLas0am")]
    public async Task<ActionResult<List<ParadasActuales2turnoDTO>>> GetParadasActuales2turnoDespuesDeLas0am([FromQuery] string? centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(18);
        DateTime final = DateTime.Today.AddHours(6);
        try
        {
            var query = from pe in _context.Paradasejecutadas
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
                        ee.Fechaentrada.HasValue &&
                        ee.Fechaentrada.Value.Hour >= 17 &&
                        !p.Codigoparada.EndsWith("0114") &&
                        te.Codigoproceso == centroCosto
                        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
                        select new ParadasActuales2turnoDTO
                        {
                            CodigoRegistro = pe.Codigoregistrso.ToString(),
                            CodigoGrupoParada = gp.Codigogrupoparada,
                            NombreParada = p.Nombreparada,
                            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0/ 60.0)
                                        .ToString(),
                            ParteNombre = pa != null ? pa.ParteNombre : null,
                            CodigoParte = pa != null ? pa.Codigo : null
                        };
            var resultado = await query.ToListAsync();
            if (resultado == null || !resultado.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}. Introduzca un centroCosto valido.");
            }
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2turnoDespuesDeLas0amAgrupadas")]
    public async Task<ActionResult<List<ParadasActuales2turnoDespuesDeLas0amAgrupadasDTO>>> GetParadasActuales2turnoDespuesDeLas0amAgrupadas([FromQuery] string? centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(18);
        DateTime final = DateTime.Today.AddHours(6);
        try
        {
            var query = from pe in _context.Paradasejecutadas
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
                        where pe.Codigoregistrso != null &&
                        p.Nombreparada != null &&
                        gp.Codigogrupoparada != null &&
                        ee.Fechaentrada >= inicio &&
                        ee.Fechaentrada < final &&
                        !p.Codigoparada.EndsWith("0114") &&
                        te.Codigoproceso == centroCosto
                        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
                        select new
                        {
                            pe,
                            p,
                            gp,
                            pa,
                            TiempoPerdido = EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0/ 60.0
                        };
            var data = await query.ToListAsync();
            var result = data
                .GroupBy(x => new
                {
                    x.p.Codigoparada,
                    x.gp.Codigogrupoparada,
                    ACodGes = x.pa?.AcodGes ?? "NULL",
                    x.p.Nombreparada,
                    Aparte = x.pa?.Aparte ?? "NULL"
                })
                .Select(grp => new
                {
                    CodigoParada = grp.Key.Codigoparada,
                    CodigoGrupoParada = grp.Key.Codigogrupoparada,
                    ACodGes = grp.Key.ACodGes,
                    NombreParada = grp.Key.Nombreparada,
                    Aparte = grp.Key.Aparte,
                    TiempoPerdido = grp.Sum(x => x.TiempoPerdido)
                })
                .OrderByDescending(x => x.TiempoPerdido)
                .Select(grp => new ParadasActuales2turnoDespuesDeLas0amAgrupadasDTO
                {
                    CodigoParada = grp.CodigoParada,
                    CodigoGrupoParada = grp.CodigoGrupoParada,
                    ACodGes = grp.ACodGes,
                    NombreParada = grp.NombreParada,
                    Aparte = grp.Aparte,
                    TiempoPerdido = grp.TiempoPerdido.ToString()
                });
            if (result == null || !result.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}. Introduzca un centroCosto valido.");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetParadasActuales2turno")]
    public async Task<ActionResult<List<List<string>>>> GetParadasActuales2turno(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || !Regex.IsMatch(centroCosto, @"^\d+$"))
        {
            return BadRequest("El centro de costo es obligatorio y debe contener solo valores numéricos.");
        }
        DateTime hoy = DateTime.Now;
        try
        {
            ActionResult<List<ParadasActuales2turnoDTO>> actionResult;
            if (hoy.Hour < 6)
            {
                actionResult = await GetParadasActuales2turnoDespuesDeLas0am(centroCosto);
            }
            else if (hoy.Hour >= 18)
            {
                actionResult = await GetParadasActuales2turnoAntesDeLas0am(centroCosto);
            }
            else
            {
                return NotFound("El servicio está disponible solo durante los turnos definidos (00:00-06:00 y 18:00-23:59).");
            }
            if (actionResult.Value is null)
            {
                return NotFound();
            }
            var resultado = actionResult.Value.Select(dto => new List<string>
    {
        dto.CodigoRegistro,
        dto.CodigoGrupoParada,
        dto.NombreParada,
        dto.TiempoPerdido,
        dto.ParteNombre,
        dto.CodigoParte
    }).ToList();
    if (resultado == null || !resultado.Any())
            {
                return NotFound($"No se encontraron registros para {centroCosto}.");
            }
            return resultado;
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetPrimeraParadaporLinea")]
    public async Task<ActionResult<PrimeraParadaporLineaDTO>> GetPrimeraParadaporLinea(string proceso)
    {
        if (string.IsNullOrWhiteSpace(proceso))
        {
            return BadRequest("El código de proceso es obligatorio.");
        }
        var horaActual = DateTime.Now.Hour;
        ActionResult<List<string>> resultadoTurno;
        if (horaActual >= 6 && horaActual < 18)
        {
            resultadoTurno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos1turno();
        }
        else if (horaActual >= 18 && horaActual < 22)
        {
            resultadoTurno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2turnoAntes0am();
        }
        else
        {
            resultadoTurno = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2turnoDespues0am();
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
                join ee in _context.Entradaejecucions on pe.Codigoentradaejecucion equals ee.Codigoentradaejecucion
                join te in _context.Tuplaejecucions on ee.Codigotupla equals te.Codigotupla
                join tw in _context.Transmicionwebs on ee.Codigoentradaejecucion equals tw.Codigoentradaejecucion
                where te.Codigoproceso == proceso
                orderby pe.Fechayhoraparada
                select new PrimeraParadaporLineaDTO
                {
                    CodigoProceso = te.Codigoproceso,
                    FechaYHoraParada = pe.Fechayhoraparada,
                    Timespan = pe.Timespan
                })
                .FirstOrDefaultAsync();
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
