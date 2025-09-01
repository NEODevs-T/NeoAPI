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

public class GesplineEntradaEjecucionController : ControllerBase
{
    private readonly GesplineContext _context;
    private readonly IMaquinasGesplineLogic _maquinasGesplineLogic;

    public GesplineEntradaEjecucionController(GesplineContext context, IMaquinasGesplineLogic maquinasGesplineLogic)
    {
        _context = context;
        _maquinasGesplineLogic = maquinasGesplineLogic;
    }

    [HttpGet("GetMaquinasGesplineActivos1Turno")]
        public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos1Turno()
        {
            try
            {
                var listaCodigoProceso = await _maquinasGesplineLogic.GetMaquinasGesplineActivos1Turno();
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

    [HttpGet("GetMaquinasGesplineActivos2TurnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos2TurnoDespues0am()
    {
        try
            {
                var listaCodigoProceso = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2TurnoDespues0am();
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

    [HttpGet("GetMaquinasGesplineActivos2TurnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos2TurnoAntes0am()
    {
        try
            {
                var listaCodigoProceso = await _maquinasGesplineLogic.GetMaquinasGesplineActivos2TurnoAntes0am();
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
}