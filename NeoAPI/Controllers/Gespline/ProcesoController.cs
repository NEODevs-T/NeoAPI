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

public class GesplineProcesoController : ControllerBase
{
    private readonly GesplineContext _context;

    private readonly ITiempoTrabajoGesplineLogic _tiempoTrabajo;

    public GesplineProcesoController(GesplineContext context, ITiempoTrabajoGesplineLogic tiempoTrabajo)
    {
        _context = context;
        _tiempoTrabajo = tiempoTrabajo;
    }

[HttpGet("GetTiempoPerdidoActual1Turno")]
public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual1Turno()
{
    try
    {
        var resultado = await _tiempoTrabajo.GetTiempoPerdidoActual1Turno();
        List<string> listaTiempoPerdido = resultado.Value;
        if (listaTiempoPerdido == null || !listaTiempoPerdido.Any())
        {
            return BadRequest("No se encontraron registros.");
        }
        return Ok(listaTiempoPerdido);
    }
    catch (Exception ex)
    {
        return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
    }
}


    [HttpGet("GetTiempoPerdidoActual2TurnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2TurnoAntes0am()
    {
        try
    {
            var resultado = await _tiempoTrabajo.GetTiempoPerdidoActual2TurnoAntes0am();
        List<string> listaTiempoPerdido = resultado.Value;
        if (listaTiempoPerdido == null || !listaTiempoPerdido.Any())
        {
            return BadRequest("No se encontraron registros.");
        }
        return Ok(listaTiempoPerdido);
    }
    catch (Exception ex)
    {
        return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
    }
    }

    [HttpGet("GetTiempoPerdidoActual2TurnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2TurnoDespues0am()
    {
        try
        {
            var resultado = await _tiempoTrabajo.GetTiempoPerdidoActual2TurnoDespues0am();
            List<string> listaTiempoPerdido = resultado.Value;
            if (listaTiempoPerdido == null || !listaTiempoPerdido.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
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
        try
        {
            var resultado = await _tiempoTrabajo.GetTiempoEjecutadoActual1Turno();
            List<string> listaTiempoEjecutado = resultado.Value;
            if (listaTiempoEjecutado == null || !listaTiempoEjecutado.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            return Ok(listaTiempoEjecutado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoEjecutadoActual2TurnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2TurnoAntes0am()
    {
        try
        {
            var resultado = await _tiempoTrabajo.GetTiempoEjecutadoActual2TurnoAntes0am();
            List<string> listaTiempoEjecutado = resultado.Value;
            if (listaTiempoEjecutado == null || !listaTiempoEjecutado.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            return Ok(listaTiempoEjecutado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoEjecutadoActual2TurnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2TurnoDespues0am()
    {
        try
        {
            var resultado = await _tiempoTrabajo.GetTiempoEjecutadoActual2TurnoDespues0am();
            List<string> listaTiempoEjecutado = resultado.Value;
            if (listaTiempoEjecutado == null || !listaTiempoEjecutado.Any())
            {
                return BadRequest("No se encontraron registros.");
            }
            return Ok(listaTiempoEjecutado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [HttpGet("GetTiempoTrabajadoActual1Turno")]
    public async Task<ActionResult<List<string>>> TiempoTrabajadoActual1Turno()
    {
        try
        {
            var resultadoR = await _tiempoTrabajo.GetTiempoTrabajadoActual1Turno();
            if (resultadoR?.Value == null)
            {
                return BadRequest("No se pudo obtener la lista de tiempo trabajado (turno 1).");
            }
            List<string> resultado = resultadoR.Value;
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }
    [HttpGet("GetTiempoTrabajadoActual2Turno")]
    public async Task<ActionResult<List<string>>> GetTiempoTrabajadoActual2Turno(bool band)
    {
        try
        {
            if (band)
            {
                TimeSpan ahora = DateTime.Now.TimeOfDay;
                TimeSpan inicioTurno = TimeSpan.FromHours(18);
                TimeSpan finTurno = TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59)).Add(TimeSpan.FromSeconds(59));
                if (ahora < inicioTurno || ahora > finTurno)
                {
                    return BadRequest("No se pudo obtener el tiempo trabajado (2 turno).");
                }
            }
            var resultadoR = await _tiempoTrabajo.GetTiempoTrabajadoActual2Turno(band);
            if (resultadoR?.Value == null)
            {
                return BadRequest("No se pudo obtener el tiempo trabajado (2 turno).");
            }
            return Ok(resultadoR.Value);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
        }
    }
}   
