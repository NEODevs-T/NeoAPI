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

public class KsfControllers : ControllerBase
{

    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public KsfControllers(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetKsf")]
    public async Task<ActionResult<List<KsfDTO>>> GetKsf()
    {

        List<Ksf> ksfs = await _context.Ksfs
            .Where(a => a.KsfEsta == true)
            .ToListAsync();

        return Ok(_mapper.Map<List<KsfDTO>>(ksfs));
    }
}