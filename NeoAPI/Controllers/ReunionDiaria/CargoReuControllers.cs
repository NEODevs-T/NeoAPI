using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.GetCentroDiv;


namespace NeoAPI.Controllers.CargoReuControllers;

[ApiController]
[Route("api/[controller]")]

public class CargoReuControllers: ControllerBase
{
    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public CargoReuControllers(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpGet("GetAsistencia/{centro}/{empresa}")]
    public async Task<ActionResult<List<CargoReuDTO>>> GetAsistencia(string centro, string empresa)
    {

        List<CargoReu> cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro & a.Crempresa == empresa & a.Cresta == true)
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        return Ok(_mapper.Map<List<CargoReuDTO>>(cargoreus));
    }

}