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

public class Gespline_EntradaejecucionController : ControllerBase
{
    private readonly GesplineContext _context;
    private readonly IMaquinasGesplineLogic _maquinasGesplineLogic;

    public Gespline_EntradaejecucionController(GesplineContext context, IMaquinasGesplineLogic maquinasGesplineLogic)
    {
        _context = context;
        _maquinasGesplineLogic = maquinasGesplineLogic;
    }

    [HttpGet("GetMaquinasGesplineActivos1turno")]
        public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos1turno()
        {
            try
            {
                var listaCodigoProceso = await _maquinasGesplineLogic.GetMaquinasGesplineActivos1turno();
                if (listaCodigoProceso == null || listaCodigoProceso.Count == 0)
                {
                    return BadRequest("No se encontraron registros.");
                }
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
        try
            {
                var listaCodigoProceso = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2turnoDespues0am();
                if (listaCodigoProceso == null || listaCodigoProceso.Count == 0)
                {
                    return BadRequest("No se encontraron registros.");
                }
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
        try
            {
                var listaCodigoProceso = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2turnoAntes0am();
                if (listaCodigoProceso == null || listaCodigoProceso.Count == 0)
                {
                    return BadRequest("No se encontraron registros.");
                }
                return Ok(listaCodigoProceso);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error inesperado: {ex.Message}");
            }
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