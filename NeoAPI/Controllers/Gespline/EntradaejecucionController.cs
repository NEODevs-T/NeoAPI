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


namespace NeoAPI.Controllers.Gespline;


[ApiController]
[Route("api/[controller]")]

public class EntradaejecucionController : ControllerBase
{
    private readonly GesplineContext _context;


    public EntradaejecucionController(GesplineContext context)
    {
        _context = context;
    }

    [HttpGet("GetMaquinasGesplineActivos1turno")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos1turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);//new DateTime(2025,04,22,5,50,0);
        DateTime final = DateTime.Today.AddHours(18); //new DateTime(2025,04,22,18,0,0);
        List<string> listaCodigoProceso = new List<string>();
        try
        {
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
                .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
                .Include(e => e.CodigotuplaNavigation)
                .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            listaCodigoProceso = listaEjecucion
                .Select(e => e.CodigotuplaNavigation.Codigoproceso)
                .Distinct()
                .OrderBy(l => l)
                .ToList();
            return Ok(listaCodigoProceso);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetMaquinasGesplineActivos2turnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos2turnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(6);
        List<string> listaCodigoProceso = new List<string>();
        try
        {
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
                .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
                .Include(e => e.CodigotuplaNavigation)
                .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            listaCodigoProceso = listaEjecucion
                .Select(e => e.CodigotuplaNavigation.Codigoproceso)
                .Distinct()
                .OrderBy(l => l)
                .ToList();
            return Ok(listaCodigoProceso);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetMaquinasGesplineActivos2turnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos2turnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        List<string> listaCodigoProceso = new List<string>();
        try
        {
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
                .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
                .Include(e => e.CodigotuplaNavigation)
                .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            listaCodigoProceso = listaEjecucion
                .Select(e => e.CodigotuplaNavigation.Codigoproceso)
                .Distinct()
                .OrderBy(l => l)
                .ToList();
            return Ok(listaCodigoProceso);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoPerdidoActual1turno")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual1turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
        try
        {
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .ToListAsync();
            var tiempoPerdido = await this._context.Paradasejecutadas
                .Where(e => e.Timespan != null && e.Fechayhoraparada != null
                        && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio
                        && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
                .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
                .Select(g => new
                {
                    Codigoproceso = g.Key,
                    TiempoPerdido = g.Sum(p => EF.Functions.DateDiffMinute(p.Fechayhoraparada.Value, p.Timespan.Value)) / 60.0f
                })
                .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            List<string> listaTiempoPerdido = tiempoPerdido
            .OrderBy(tp => tp.Codigoproceso)
            .Select(tp => $"{tp.Codigoproceso}: {tp.TiempoPerdido}")
            .ToList();
            return Ok(listaTiempoPerdido);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoPerdidoActual2turnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2turnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        try
        {
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .ToListAsync();
            var tiempoPerdido = await this._context.Paradasejecutadas
            .Where(e => e.Timespan != null && e.Fechayhoraparada != null
                    && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio
                    && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
            .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
            .Select(g => new
            {
                Codigoproceso = g.Key,
                TiempoPerdido = g.Sum(p => EF.Functions.DateDiffMinute(p.Fechayhoraparada.Value, p.Timespan.Value)) / 60.0f
            })
            .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            List<string> listaTiempoPerdido = tiempoPerdido
            .OrderBy(tp => tp.Codigoproceso)
            .Select(tp => $"{tp.Codigoproceso}: {tp.TiempoPerdido}")
            .ToList();
            return Ok(listaTiempoPerdido);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }      
    }


    [HttpGet("GetTiempoPerdidoActual2turnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2turnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(6);
        try
        {
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .ToListAsync();
            var tiempoPerdido = await this._context.Paradasejecutadas
            .Where(e => e.Timespan != null && e.Fechayhoraparada != null
                    && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio
                    && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
            .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
            .Select(g => new
            {
                Codigoproceso = g.Key,
                TiempoPerdido = g.Sum(p => EF.Functions.DateDiffMinute(p.Fechayhoraparada.Value, p.Timespan.Value)) / 60.0f
            })
            .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            List<string> listaTiempoPerdido = tiempoPerdido
            .OrderBy(tp => tp.Codigoproceso)
            .Select(tp => $"{tp.Codigoproceso}: {tp.TiempoPerdido}")
            .ToList();
            return Ok(listaTiempoPerdido);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }


    [HttpGet("GetTiempoEjecutadoActual1Turno")]

    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual1Turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
        try
        {
        List<string> listaTiempoActual = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .OrderBy(e => e.CodigotuplaNavigation.Codigoproceso)
        .ToListAsync();
        if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
        foreach (var item in listaEjecucion)
            {
                var codigo = item.CodigotuplaNavigation.Codigoproceso;
                string tiempo = item.Horasejecutadas.ToString();
                listaTiempoActual.Add($"{codigo}: {tiempo}");
            }
        return Ok(listaTiempoActual);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoEjecutadoActual2turnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2turnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
        try
        {
            List<string> listaTiempoActual = new List<string>();
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .OrderBy(e => e.CodigotuplaNavigation.Codigoproceso)
            .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
        foreach (var item in listaEjecucion)
            {
                var codigo = item.CodigotuplaNavigation.Codigoproceso;
                string tiempo = item.Horasejecutadas.ToString();
                listaTiempoActual.Add($"{codigo}: {tiempo}");
            }
            return Ok(listaTiempoActual);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoEjecutadoActual2turnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2turnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(5).AddMinutes(50);
        try
        {
            List<string> listaTiempoActual = new List<string>();
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .OrderBy(e => e.CodigotuplaNavigation.Codigoproceso)
            .ToListAsync();
            if (!listaEjecucion.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            foreach (var item in listaEjecucion)
            {
                var codigo = item.CodigotuplaNavigation.Codigoproceso;
                string tiempo = item.Horasejecutadas.ToString();
                listaTiempoActual.Add($"{codigo}: {tiempo}");
            }
            return Ok(listaTiempoActual);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoTrabajadoActual1turno")]
    public async Task<ActionResult<List<string>>> TiempoTrabajadoActual1Turno()
    {
        try
        {
            var listaEjecutadoR = await GetTiempoEjecutadoActual1Turno();
            List<string> listaEjecutado;
            if (listaEjecutadoR.Result is OkObjectResult okEjecutado
            && okEjecutado.Value is List<string> dataEjecutado)
            {
                listaEjecutado = dataEjecutado;
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de tiempo ejecutado.");
            }
            var listaPerdidoR = await GetTiempoPerdidoActual1turno();
            List<string> listaPerdido;
            if (listaPerdidoR.Result is OkObjectResult okPerdido
            && okPerdido.Value is List<string> dataEjecutado2)
            {
                listaPerdido = dataEjecutado2;
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de tiempo perdido.");
            }
            var ejecutado = listaEjecutado.Select(e =>
        {
            var parts = e.Split(':');
            return new
            {
                Key = parts[0].Trim(),
                Value = float.Parse(
                    parts[1].Trim(),
                    new CultureInfo("es-VE"))
            };
        }).ToList();
            var perdido = listaPerdido.Select(p =>
            {
                var parts = p.Split(':');
                return new
                {
                    Key = parts[0].Trim(),
                    Value = float.Parse(
                        parts[1].Trim(),
                        new CultureInfo("es-VE"))
                };
            }).ToList();
            var resultado = from ej in ejecutado
                            join p in perdido on ej.Key equals p.Key into perdGroup
                            from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                            let tiempoEjecutado = ej.Value
                            let tiempoPerdido = perd != null ? perd.Value : 0f
                            let tiempoNeto = tiempoEjecutado - tiempoPerdido
                            select $"{tiempoNeto}";
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoTrabajadoActual2turno")]
    public async Task<ActionResult<List<string>>> GetTiempoTrabajadoActual2turno(bool band)
    {
        try
        {
            List<string> listaEjecutado;
            List<string> listaPerdido;
            if (band)
            {
                var listaEjecutadoR = await GetTiempoEjecutadoActual2turnoAntes0am();
                if (listaEjecutadoR.Result is OkObjectResult okEjectudado
                && okEjectudado.Value is List<string> dataEjecutado)
                {
                    listaEjecutado = dataEjecutado;
                }
                else
                {
                    return BadRequest("No se pudo obtener la lista de tiempo ejecutado (antes 0 am).");
                }
                var listaPerdidoR = await GetTiempoPerdidoActual2turnoAntes0am();
                if (listaPerdidoR.Result is OkObjectResult okPerdido
                && okPerdido.Value is List<string> dataEjecutado2)
                {
                    listaPerdido = dataEjecutado2;
                }
                else
                {
                    return BadRequest("No se pudo obtener la lista de tiempo perdido (antes 0 am).");
                }
            }
            else
            {
                var listaEjecutadoR = await GetTiempoEjecutadoActual2turnoDespues0am();
                if (listaEjecutadoR.Result is OkObjectResult okEjectudado
                && okEjectudado.Value is List<string> dataEjecutado)
                {
                    listaEjecutado = dataEjecutado;
                }
                else
                {
                    return BadRequest("No se pudo obtener la lista de tiempo ejecutado (después 0 am).");
                }
                var listaPerdidoR = await GetTiempoPerdidoActual2turnoDespues0am();
                if (listaPerdidoR.Result is OkObjectResult okPerdido
                && okPerdido.Value is List<string> dataEjecutado2)
                {
                    listaPerdido = dataEjecutado2;
                }
                else
                {
                    return BadRequest("No se pudo obtener la lista de tiempo perdido (después 0 am).");
                }
            }
        var ejecutado = listaEjecutado.Select(e =>
        {
            var parts = e.Split(':');
            return new
            {
                Key = parts[0].Trim(),
                Value = float.Parse(
                    parts[1].Trim(),
                    new CultureInfo("es-VE"))
            };
        }).ToList();
        var perdido = listaPerdido.Select(p =>
        {
            var parts = p.Split(':');
            return new
            {
                Key = parts[0].Trim(),
                Value = float.Parse(
                    parts[1].Trim(),
                    new CultureInfo("es-VE"))
            };
        }).ToList();
        var resultado = from ej in ejecutado
                        join p in perdido on ej.Key equals p.Key into perdGroup
                        from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                        let tiempoEjecutado = ej.Value
                        let tiempoPerdido = perd != null ? perd.Value : 0f
                        let tiempoNeto = tiempoEjecutado - tiempoPerdido
                        select $"{tiempoNeto}";
        return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
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
                            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0)
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
                        TiempoPerdido = EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0
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
                        TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0)
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
                            TiempoPerdido = EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0
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
                            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0)
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
                        TiempoPerdido = EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0
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
        DateTime hoy = DateTime.Now;
        //var horaActual = 4;

        ActionResult<List<ParadasActuales2turnoDTO>> actionResult;

        if (hoy.Hour < 6)
        //if (horaActual < 6)
        {
            actionResult = await GetParadasActuales2turnoDespuesDeLas0am(centroCosto);
        }
        else if (hoy.Hour >= 18)
        //else if (horaActual >= 18)
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

        return resultado;
    }


/* [HttpGet("FiltrarDatos")]
    public List<List<string>> FiltrarDatos(List<List<string>> datos, string cadenaIdRegistros)
    {
        string[] filtros = cadenaIdRegistros
            .Replace("[", "")
            .Replace("]", "")
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();
        foreach (var filtro in filtros)
        {
            int index = datos[0].FindIndex(d => d.Contains(filtro));
            if (index >= 0)
            {
                for (int i = 0; i < datos.Count; i++)
                {
                    if (index < datos[i].Count)
                        datos[i].RemoveAt(index);
                }
            }
        }
        return datos;
    }*/

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
            resultadoTurno = await GetMaquinasGesplineActivos1turno();
        }
        else if (horaActual >= 18 && horaActual < 22)
        {
            resultadoTurno = await GetMaquinasGesplineActivos2turnoAntes0am();
        }
        else
        {
            resultadoTurno = await GetMaquinasGesplineActivos2turnoDespues0am();
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

/*    [HttpGet("GetParadasSegundoTurnoPorMaquina/{centroCosto}")]
    public async Task<IActionResult> GetParadasSegundoTurnoPorMaquina(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");
        }
        try
        {
            // Obtiene el ActionResult de GetParadasActuales2turno
            var paradasResult = await GetParadasActuales2turno(centroCosto);
            // Extrae la lista real desde la propiedad Value
            var paradas = paradasResult.Value;

            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }
            return Ok(paradas);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

    [HttpGet("GetParadasGesplienActualesAgrupados1turno/{centroCosto}")]
    public async Task<IActionResult> GetParadasGesplienActualesAgrupados1turno(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");
        }
        try
        {
            var paradas = await GetParadasActuales1TurnoAgrupados(centroCosto);
            if (paradas.Value == null || !paradas.Value.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }
            return Ok(paradas.Value);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

    [HttpGet("GetParadasGesplienActualesAgrupados2turnoAntesDeLas0am/{centroCosto}")]
        public async Task<IActionResult> GetParadasGesplienActualesAgrupados2turnoAntesDeLas0am(string centroCosto)
        {
            if (string.IsNullOrWhiteSpace(centroCosto))
            {
                return BadRequest("El centro de costo es obligatorio.");
            }
            try
            {
                var paradas = await GetParadasActuales2TurnoAntesDeLas0AmAgrupadas(centroCosto);
                if (paradas == null || !paradas.Any())
                {
                    return NotFound("No se encontraron paradas para el centro de costo especificado.");
                }
                return Ok(paradas);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
            }
        }*/

    /*   [HttpGet("GetParadasGesplienActualesAgrupados2turnoDespuesDeLas0am/{centroCosto}")]
        public async Task<IActionResult> GetParadasGesplienActualesAgrupados2turnoDespuesDeLas0am(string centroCosto)
        {
            if (string.IsNullOrWhiteSpace(centroCosto))
            {
                return BadRequest("El centro de costo es obligatorio.");            
            }
            try
            {
                var paradas = await GetParadasActuales2turnoDespuesDeLas0amAgrupadas(centroCosto);
                if (paradas == null || !paradas.Any())
                {
                    return NotFound("No se encontraron paradas para el centro de costo especificado.");
                }
                return Ok(paradas);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
            }
        }*/
    /*
        [HttpGet("GetParadasActuales1turnoPorLinea/{centroCosto}")]

        public async Task<IActionResult> GetParadasActuales1turnoPorLinea(string centroCosto)
        {
            if (string.IsNullOrWhiteSpace(centroCosto))
            {
                return BadRequest("El centro de costo es obligatorio.");
            }

            try 
            {
                var paradas = await GetParadasActuales1Turno(centroCosto);

                if (paradas == null || !paradas.Any())
                {
                    return NotFound("No se encontraron paradas para el centro de costo especificado.");
                }

                return Ok(paradas);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
            }
        }

        [HttpGet("GetLaPrimeraParadaPorLinea")]

        public async Task<IActionResult> GetLaPrimeraParadaPorLinea()
        {
            try
            {
                var resultado = await Task.Run(() => GetLaPrimeraParadaPorLinea());

                if (resultado == null)
                {
                    return NotFound("No se encontró la información solicitada.");
                }

                return Ok(resultado);

            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
            }
        }

        [HttpGet("GetParadasSegundoTurnoPorMaquina/{centroCosto}/{cadenas}")]
        public async Task<IActionResult> GetParadasSegundoTurnoPorMaquina(string centroCosto, string cadenas)
        {
            if (string.IsNullOrWhiteSpace(centroCosto) || string.IsNullOrWhiteSpace(cadenas))
            {
                return BadRequest("El centro de costo y la cadena son obligatorios.");
            }        
            try

            {
                var paradasSinFiltro = await Task.Run(() => GetParadasActuales2turno(centroCosto));
                if (paradasSinFiltro == null || !paradasSinFiltro.Any())
                {
                    return NotFound("No se encontraron paradas para el centro de costo especificado.");
                }


                var paradasFiltradas = await Task.Run(() => FiltrarDatos(paradasSinFiltro, cadenas));
                if (paradasFiltradas == null || !paradasFiltradas.Any())
                {
                    return NotFound("No se encontraron paradas filtradas para el centro de costo especificado.");
                }

                return Ok(paradasFiltradas);
                }
                catch (Exception ex)
                {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
            }
        }

        [HttpGet("GetParadasPrimerTurnoPorMaquina/{centroCosto}/{cadenas}")]

        public async Task<IActionResult> GetParadasPrimerTurnoPorMaquina(string centroCosto, string cadenas)
        {
            if(string.IsNullOrWhiteSpace(centroCosto) || string.IsNullOrWhiteSpace(cadenas))
            {
                return BadRequest("El centro de costo y la cadena son obligaotirios.");
            }
            try
            {
                var paradasFiltradas = await Task.Run(() => GetParadasActuales1Turno(centroCosto));
                if (paradasFiltradas == null || !paradasFiltradas.Any())
                {
                    return NotFound("No se encontraron paradas filtradas para el centro de costo especificado.");
                }

                return Ok(paradasFiltradas);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
                }
        }*/

}