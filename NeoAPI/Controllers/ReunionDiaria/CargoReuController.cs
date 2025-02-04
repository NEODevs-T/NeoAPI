using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.ReunionDia;


namespace NeoAPI.Controllers.CargoReuControllers;

[ApiController]
[Route("api/[controller]")]

public class CargoReuController: ControllerBase
{
    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public CargoReuController(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


        [HttpGet("GetAsistencia/{centro}/{empresa}")]
        public async Task<ActionResult<List<CargoReuDTO>>> GetAsistencia(string centro, string empresa)
{
    try
    {
        List<CargoReu> cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro 
                     && a.Crempresa == empresa 
                     && a.Cresta == true 
                     && a.IdTipReu == 1)  // Filtramos solo los que tienen IdTipReu = 1
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        if (cargoreus == null || !cargoreus.Any())
        {
            return NotFound($"No se encontraron registros con centro '{centro}', empresa '{empresa}' y IdTipReu = 3.");
        }

        return Ok(_mapper.Map<List<CargoReuDTO>>(cargoreus));
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
}

[HttpGet("GetAsistenciadetTurno/{centro}/{empresa}")]
public async Task<ActionResult<List<CargoReuDTO>>> GetAsistenciadetTurno(string centro, string empresa)    //GetAsistencia
{
    try
    {
        List<CargoReu> cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro 
                     && a.Crempresa == empresa 
                     && a.Cresta == true 
                     && a.IdTipReu == 2)  // Filtramos solo los que tienen IdTipReu = 2
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        if (cargoreus == null || !cargoreus.Any())
        {
            return NotFound($"No se encontraron registros con centro '{centro}', empresa '{empresa}' y IdTipReu = 2.");
        }

        return Ok(_mapper.Map<List<CargoReuDTO>>(cargoreus));
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
}


[HttpGet("GetAsistenciaQuincenal/{centro}/{empresa}")]
public async Task<ActionResult<List<CargoReuDTO>>> GetAsistenciaQuincenal(string centro, string empresa)
{
    try
    {
        List<CargoReu> cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro 
                     && a.Crempresa == empresa 
                     && a.Cresta == true 
                     && a.IdTipReu == 3)  // Filtramos solo los que tienen IdTipReu = 3
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        if (cargoreus == null || !cargoreus.Any())
        {
            return NotFound($"No se encontraron registros con centro '{centro}', empresa '{empresa}' y IdTipReu = 3.");
        }

        return Ok(_mapper.Map<List<CargoReuDTO>>(cargoreus));
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
}


}