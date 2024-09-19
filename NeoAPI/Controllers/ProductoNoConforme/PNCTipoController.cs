using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.PNC;


namespace NeoAPI.Controllers.PNC
{
    [ApiController]
    [Route("api/[controller]")]
    public class PNCTipoController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public PNCTipoController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetTodosLosTipos")]
        public async Task<List<TipoDTO>> ObtenerTodosLosTipos()
        {
            List<Tipo> tipoLista = await this._context.Tipos.Where(t => t.Testado == true).ToListAsync();

            return _mapper.Map<List<TipoDTO>>(tipoLista);
        }

    }
    
}   