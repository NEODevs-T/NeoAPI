using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.ReunionDia;


namespace NeoAPI.Controllers.CausaCalidad;

[ApiController]
[Route("api/[controller]")]
    public class CausaCalidadController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public CausaCalidadController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetCausasCalidad")]
        public async Task<ActionResult<List<CausaCalDTO>>> GetCausasCalidad()
        {

            List<CausaCal> causaCals = await _context.CausaCals
                .Where(a => a.Ccestado == true)
                .ToListAsync();

            return Ok(_mapper.Map<List<CausaCalDTO>>(causaCals));
        }



          [HttpGet("GetCausasCali1221dad")]
        public async Task<ActionResult<List<CausaCalDTO>>> GetCausasCali1221dad()
        {

            List<CausaCal> causaCals = await _context.CausaCals
                .Where(a => a.Ccestado == true)
                .ToListAsync();

            return Ok(_mapper.Map<List<CausaCalDTO>>(causaCals));
        }



    }