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
    public class TipoParadaController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public TipoParadaController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //TODO: Cambiar prefijo a GET
        [HttpGet("ObtenerTipoParadaId/{IdGespline}")]
        public async Task<ActionResult<TiParTpDTO>> ObtenerTipoParadaId(string IdGespline)
        {
            TiParTp data = await this._context.TiParTps.Where(t => t.Tpcodigo == IdGespline).FirstOrDefaultAsync() ?? new TiParTp();
            return Ok(_mapper.Map<TiParTpDTO>(data));
        }

        //TODO: Cambiar prefijo a GET
        [HttpGet("ObtenerTodosTiposNovedad")]
        public async Task<ActionResult<List<TiParTpDTO>>> ObtenerTodosTiposNovedad()
        {
            List<TiParTp> tiParTp = new List<TiParTp>();
            tiParTp = await this._context.TiParTps.Where(t => t.Tpestado == true).ToListAsync();
            return Ok(_mapper.Map<List<TiParTpDTO>>(tiParTp));
        }
    }

}