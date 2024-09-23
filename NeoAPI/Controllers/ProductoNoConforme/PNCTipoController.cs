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

  public class PNCIdentificacionController : ControllerBase
    {

        private readonly DbNeoIiContext _context;

        private readonly IMapper _mapper;

        public PNCIdentificacionController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("GetTodosLosIdentifi")]
        public async Task<List<IdentifDTO>> ObtenerTodosLosIdentifi()
        {
            List<Identifi> identifsLista = await this._context.Identifis.Where(i => i.Iestado == true).ToListAsync();

            return _mapper.Map<List<IdentifDTO>>(identifsLista);
        }
    }



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
    



       public class PNCDisposicionDefinitivaController : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;
         private readonly IMapper _mapper;


        public PNCDisposicionDefinitivaController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }

        [HttpGet("GetTodosLasDisposicionDefinitiva")]
        public async Task<List<DisDefiDTO>> ObtenerTodosLasDisposicionDefinitiva()
        {
            List<DispDefi> disdefisLista = await this._cotext.DispDefis.Where(d => d.Ddestado == true).ToListAsync();

            return _mapper.Map<List<DisDefiDTO>>(disdefisLista);
        }
    }



        public class PNCCausanteController : ControllerBase
    {

        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public PNCCausanteController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        [HttpGet("GetTodosLosCausantes")]
        public async Task<List<CausanteDTO>> ObtenerTodosLosCausantes()
        {
            List<Causante> causantelista = await this._context.Causantes.Where(c => c.Cestado == true).ToListAsync();

            return _mapper.Map<List<CausanteDTO>>(causantelista);
        }

    }



        public class PNCPropuestaDisposicionController : ControllerBase
        
    {


        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public PNCPropuestaDisposicionController(DbNeoIiContext context, IMapper maper)
        {
            _context = context;
            _mapper = maper;
        }

         [HttpGet("GetTodasLasPropuestaDisposicion")]

        public async Task<List<ProDispDTO>> ObtenerTodasLasPropuestaDisposicion()
        {
            List<PropDisp> proDispsLista = await this._context.PropDisps.Where(p => p.Pdestado == true).ToListAsync();

            return _mapper.Map<List<ProDispDTO>>(proDispsLista);
        }

        [HttpPost("AddPropuestaDisposicion")]
        public async Task<bool> AddPropuestaDisposicion(ProDispDTO registro)
        {
            var entidad = _mapper.Map<PropDisp>(registro);
            
            this._context.PropDisps.Add(entidad);
            
            return await _context.SaveChangesAsync() > 0;
        }
    
    }



        public class PNCUnidadController : ControllerBase 
    {

        private readonly DbNeoIiContext _cotext;

        private readonly IMapper _mapper;

        public PNCUnidadController (DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;       
        }

        [HttpGet("GetTodosLasUnidades")]
        public async Task<List<UnidadeDTO>> ObtenerTodosLasUnidades()
        {
            List<Unidad> UnidadLista =  await this._cotext.Unidads.Where(u => u.Uestado == true).ToListAsync();

            return _mapper.Map<List<UnidadeDTO>>(UnidadLista);
        }
    }


}   
