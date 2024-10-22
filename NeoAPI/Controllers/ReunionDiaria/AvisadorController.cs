using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.GetCentroDiv;


namespace NeoAPI.Controllers.Avisador;

[ApiController]
[Route("api/[controller]")]
public class AvisadorController : ControllerBase
{
    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public AvisadorController(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("AddRegistros")]
    public async Task<ActionResult<bool>> AddRegistros((List<CambFecDTO> lista1, List<CambStatDTO> lista2) data)
    {
        List<CambFec> _lista1 = _mapper.Map<List<CambFec>>(data.lista1);
        List<CambStat> _lista2 = _mapper.Map<List<CambStat>>(data.lista2);

        foreach (var item in _lista1)
        {
            this._context.CambFecs.Add(item);
        }

        foreach (var item in _lista2)
        {
            this._context.CambStats.Add(item);
        }
        return Ok(await _context.SaveChangesAsync() > 0);
    }

    //Consultas para los cambios de estatus en una discrepancia
    [HttpGet("GetCambioStatus/{idreu:int}")]
    public async Task<ActionResult<List<CambStatDTO>>> GetCambioStatus(int idreu)
    {
        List<CambStat> cambiostatus = await _context.CambStats
            .Where(a => a.IdReuDia == idreu)
            // .OrderByDescending(b => b.IdCambStat)
            .ToListAsync();

        return Ok(_mapper.Map<List<CambStatDTO>>(cambiostatus));
    }

    [HttpGet("GetCambioFecha/{idReu:int}")]
    public async Task<ActionResult<List<CambFecDTO>>> GetCambioFecha(int idReu)
    {
        List<CambFec> cambiofecha = await _context.CambFecs
            .Where(a => a.IdReuDia == idReu)
            //.OrderByDescending(b => b.IdCambFec)
            .ToListAsync();
        return Ok(_mapper.Map<List<CambFecDTO>>(cambiofecha));

    }

    [HttpPost("AddCambioStatus")]
    public async Task<ActionResult<bool>> InsertCambioStatus(CambStatDTO status)
    {
        CambStat data = _mapper.Map<CambStat>(status);
        _context.CambStats.Add(data);

        return Ok(await _context.SaveChangesAsync() > 0);
    }

    [HttpPost("AddCambioFec")]
    public async Task<ActionResult<bool>> InsertCambioFec(CambFecDTO cambiofec)
    {
        CambFec data = _mapper.Map<CambFec>(cambiofec);
        _context.CambFecs.Add(data);
        return Ok(await _context.SaveChangesAsync() > 0);
    }

    //Insertar discrepancia con chismoso
    [HttpPost("AddRegistrosCambios")]
    public async Task<ActionResult<bool>> InsertarRegistros(RegistroCambiosDTO registroCambios)
    {
        if (registroCambios == null)
        {
            return BadRequest("El objeto RegistroCambiosDTO no puede ser nulo.");
        }

        CambFec cambiofec;
        CambStat cambioEstado;

        try
        {
            cambiofec = _mapper.Map<CambFec>(registroCambios.cambFecDTO);
            cambioEstado = _mapper.Map<CambStat>(registroCambios.cambStatDTO);
        }
        catch (AutoMapperMappingException ex)
        {
            return StatusCode(500, $"Error al mapear los datos: {ex.Message}");
        }

        if (cambiofec.IdReuDiaNavigation == null || cambioEstado.IdReuDiaNavigation == null)
        {
            return BadRequest("Las propiedades de navegación IdReuDiaNavigation no pueden ser nulas.");
        }

        cambiofec.IdReuDiaNavigation.IdksfNavigation = null;
        cambiofec.IdReuDiaNavigation.IdMasterNavigation = null;
        cambiofec.IdReuDiaNavigation.IdResReuNavigation = null;

        cambioEstado.IdReuDiaNavigation.IdksfNavigation = null;
        cambioEstado.IdReuDiaNavigation.IdMasterNavigation = null;
        cambioEstado.IdReuDiaNavigation.IdResReuNavigation = null;

        try
        {
            _context.CambStats.Add(cambioEstado);
            _context.CambFecs.Add(cambiofec);
            bool result = await _context.SaveChangesAsync() > 0;

            return Ok(result);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Error al guardar en la base de datos: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error desconocido: {ex.Message}");
        }
    }

}
