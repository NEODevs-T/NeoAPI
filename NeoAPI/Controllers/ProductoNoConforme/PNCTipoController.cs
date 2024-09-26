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

        private readonly DbNeoIiContext _cotext;

        private readonly IMapper _mapper;

        public PNCIdentificacionController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }
        [HttpGet("GetTodosLosIdentifi")]
        public async Task<List<IdentifDTO>> ObtenerTodosLosIdentifi()
        {
            List<Identifi> identifsLista = await this._cotext.Identifis.Where(i => i.Iestado == true).ToListAsync();

            return _mapper.Map<List<IdentifDTO>>(identifsLista);
        }
    }



    public class PNCTipoController : ControllerBase
    {
        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCTipoController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }

        [HttpGet("GetTodosLosTipos")]
        public async Task<List<TipoDTO>> ObtenerTodosLosTipos()
        {
            List<Tipo> tipoLista = await this._cotext.Tipos.Where(t => t.Testado == true).ToListAsync();

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

        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCCausanteController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;

            _mapper = mapper;
        }

        [HttpGet("GetTodosLosCausantes")]
        public async Task<List<CausanteDTO>> ObtenerTodosLosCausantes()
        {
            List<Causante> causantelista = await this._cotext.Causantes.Where(c => c.Cestado == true).ToListAsync();

            return _mapper.Map<List<CausanteDTO>>(causantelista);
        }

    }



    public class PNCPropuestaDisposicionController : ControllerBase
    
    {


        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCPropuestaDisposicionController(DbNeoIiContext context, IMapper maper)
        {
            _cotext = context;
            _mapper = maper;
        }

        [HttpGet("GetTodasLasPropuestaDisposicion")]

        public async Task<List<ProDispDTO>> ObtenerTodasLasPropuestaDisposicion()
        {
            List<PropDisp> proDispsLista = await this._cotext.PropDisps.Where(p => p.Pdestado == true).ToListAsync();

            return _mapper.Map<List<ProDispDTO>>(proDispsLista);
        }

        [HttpPost("AddPropuestaDisposicion")]
        public async Task<bool> AddPropuestaDisposicion(ProDispDTO registro)
        {
            var entidad = _mapper.Map<PropDisp>(registro);
            
            this._cotext.PropDisps.Add(entidad);
            
            return await _cotext.SaveChangesAsync() > 0;
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



    public class ProductoNoConformeController : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;

        private readonly IMapper _mapper;
        public ProductoNoConformeController (DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;

            _mapper = mapper;
        }

        [HttpPost("AddProductoNoConforme")]
        public async Task<bool> AddProductoNoConforme(ProNoConDTO registro)
        {
            var entidados = _mapper.Map<ProNoCon>(registro);
            this._cotext.ProNoCons.Add(entidados);
            return await _cotext.SaveChangesAsync() > 0;
        }


        [HttpPut("PutActualizarProductoNoConforme")]
        public async Task<bool> ActualizarProductoNoConforme(int idProNoCon, ProNoConDTO registro){
            ProNoCon? data = await this._cotext.ProNoCons.Where(p => p.IdProNoCon == idProNoCon).FirstOrDefaultAsync();
            if(data != null){
                data.IdDisDefi = registro.IdDisDefi;
                data.IdEstado = registro.IdEstado;
                data.IdIdentif = registro.IdIdentif;
                data.IdLugaEven = registro.IdLugaEven;
                data.IdProDisp = registro.IdProDisp;
                data.IdTipo = registro.IdTipo;
                data.IdCaUnidad = registro.IdUnidad;
                data.Pnccantida = registro.Pnccantida;
                data.Pnccargador = registro.Pnccargador;
                data.PnccauLibe = registro.PnccauLibe;
                data.PncordFabr = registro.PncordFabr;
                data.PnccodProd = registro.PnccodProd;
                data.PncdesProd = registro.PncdesProd;
                data.Pncfecha = DateOnly.FromDateTime(registro.Pncfecha);
                data.IdCausa = registro.IdCausa;
                data.PncindLibe = registro.PncindLibe;
                data.Pnclote = registro.Pnclote;
                return 0 < await _cotext.SaveChangesAsync();
            }
            return false;
        }





        [HttpGet("GetProductoNoConformePorFecha")]
        public async Task<List<NeoAPI.Models.Neo.ProNoCon>> ObtenerProductoNoConformePorFecha(DateTime Fecha){ 
        DateOnly fechaOnly = DateOnly.FromDateTime(Fecha);
        return await this._cotext.ProNoCons.Where(p => p.Pncfecha == fechaOnly).ToListAsync();
        }


        [HttpGet("GetProductoNoConformeEntreFechas")]
        public async Task<List<NeoAPI.Models.Neo.ProNoCon>> ObtenerProductoNoConformeEntreFechas(DateTime fechaInicio, DateTime fechaFinal){ 
            DateOnly fechaInicioOnly = DateOnly.FromDateTime(fechaInicio);
            DateOnly fechaFinalOnly = DateOnly.FromDateTime(fechaFinal);
            return await this._cotext.ProNoCons.Where(p => p.Pncfecha >= fechaInicioOnly && p.Pncfecha <= fechaFinalOnly).ToListAsync();
        }

        [HttpGet("GetProductoNoConformePorFiltro")]
        public async Task<List<NeoAPI.Models.Neo.ProNoCon>> ObtenerProductoNoConformePorFiltro(DateTime fechaInicio, DateTime fechaFinal){
            if(fechaInicio.Date == fechaFinal.Date){
                return await this.ObtenerProductoNoConformePorFecha(fechaInicio);
            }else if(fechaInicio.Date < fechaFinal.Date){
                return await this.ObtenerProductoNoConformeEntreFechas(fechaInicio,fechaFinal);
            }
            return await this.ObtenerProductoNoConformeEntreFechas(fechaFinal,fechaInicio);
        }



        [HttpGet("GetProductoNoConforme")]
        public async Task<ProNoConDTO?> ObtenerProductoNoConforme(int idRegistro)
{
    var proNoCon = await this._cotext.ProNoCons.Where(p => p.IdProNoCon == idRegistro).Include(p => p.IdCausaNavigation).ThenInclude(c => c.IdCausanteNavigation).FirstOrDefaultAsync();

    if (proNoCon == null)
    {
        return null;
    }

    return _mapper.Map<ProNoConDTO>(proNoCon);
}



        [HttpGet("GetProductoNoConformeConTodaLaData")]
        public async Task<ProNoConDTO?> ObtenerProductoNoConformeConTodaLaData(int idRegistro){
            var reg = await this._cotext.ProNoCons.Where(p => p.IdProNoCon == idRegistro).Include(p => p.IdDisDefiNavigation).Include(p => p.IdEstadoNavigation)
            .Include(p => p.IdIdentifNavigation).Include(p => p.IdProDispNavigation).Include(p => p.IdTipoNavigation)
            .Include(p => p.IdCausaNavigation).ThenInclude(c => c.IdCausanteNavigation).FirstOrDefaultAsync();
            
            if (reg == null)
            {
                return null;
            }
            return _mapper.Map<ProNoConDTO>(reg);
        }
    }

    public class PNCCausa : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCCausa (DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }

        [HttpGet("GetTodosLasCausas")]
        public async Task<List<causaDTO>> ObtenerTodasLasCausas()
        {
            List<Causa> causaLista = await this._cotext.Causas.Where(p => p.Cestado == true).ToListAsync();
            
            return _mapper.Map<List<causaDTO>>(causaLista);
        }

        [HttpGet("GetTodosLasCausasPorCausante")]
        public async Task<List<causaDTO>> ObtenerLasCausasPorCausante(int idCausante)
        {
            List<Causa> CausasPorCausanteLista = await this._cotext.Causas.Where(p => p.Cestado == true && p.IdCausante == idCausante).ToListAsync();
            return _mapper.Map<List<causaDTO>>(CausasPorCausanteLista);
        }


        [HttpPost("AddPropuestaDisposicion")]
                public async Task<bool> AddPropuestaDisposicion(causaDTO registro)
        {
            var entidades = _mapper.Map<Causa>(registro);
            
            this._cotext.Causas.Add(entidades);
            
            return await _cotext.SaveChangesAsync() > 0;
        }
    
    }


    }
