using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;


namespace NeoAPI.Controllers.LibroNovedades
{
    [ApiController]
    [Route("api/[controller]")]
    public class TPMController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public TPMController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetClasificacionTPM")]
        public async Task<ActionResult<List<ClasifiTpmDTO>>> GetClasificacionTPM()
        {
            List<ClasifiTpm> listaClasiTPM;
            listaClasiTPM = await _context.ClasifiTpms.Where(c => c.Ctpmestado == true).ToListAsync();
            return _mapper.Map<List<ClasifiTpmDTO>>(listaClasiTPM);
        }

    }
}

