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

public class Gespline_ProcesoController : ControllerBase
{
    private readonly GesplineContext _context;

    public Gespline_ProcesoController(GesplineContext context)
    {
        _context = context;
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
        DateTime final = DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59).AddSeconds(59);
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

}   
