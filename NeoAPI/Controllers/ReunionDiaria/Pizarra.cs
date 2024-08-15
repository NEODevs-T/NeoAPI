using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;


namespace NeoAPI.Controllers.Pizarra
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizarraController : ControllerBase
    {

        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;


        public PizarraController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("AddRegistros")]
        public async Task<ActionResult<bool>> InsertarRegistros(List<ReuDiumDTO> reunionDia)
        {
            List<ReuDium> reunionDia1 = _mapper.Map<List<ReuDium>>(reunionDia);
            foreach (var item in reunionDia1)
            {
                this._context.ReuDia.Add(item);
            }
            return Ok(await _context.SaveChangesAsync() > 0);
        }
    }
}