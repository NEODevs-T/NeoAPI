using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;

namespace NeoAPI.Controllers.RespoReunioController;
[ApiController]
[Route("api/[controller]")]
public class RespoReunionController : ControllerBase
{
    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public RespoReunionController(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("GetResponsables")]
    public async Task<ActionResult<List<RespoReuDTO>>> GetRespon()
    {


        List<RespoReu> resporeu = await _context.RespoReus
            .Where(a => a.Rresta == true)
            .ToListAsync();


        return Ok(_mapper.Map<List<RespoReuDTO>>(resporeu));
    }
}
