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

    
    
    public class ProductoNoConformeController : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;

        private readonly IMapper _mapper;
        public ProductoNoConformeController (DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;

            _mapper = mapper;
        }

        [HttpPost("AddProductoNoConforme/{registro}")]
        public async Task<bool> AddProductoNoConforme(ProNoConDTO registro)
        {
            var entidad1 = _mapper.Map<ProNoCon>(registro);
            this._cotext.ProNoCons.Add(entidad1);
            return await _cotext.SaveChangesAsync() > 0;
        }


    
        [HttpPut("PutActualizarProductoNoConforme/{idProNoCon}/{registro}")]
public async Task<bool> ActualizarProductoNoConforme(int idProNoCon, ProNoConDTO registro)
{
    ProNoCon? data = await this._cotext.ProNoCons.Where(p => p.IdProNoCon == idProNoCon).FirstOrDefaultAsync();
    if (data != null)
    {
        data.IdDisDefi = registro.IdDisDefi;
        data.IdEstado = registro.IdEstado;
        data.IdIdentif = registro.IdIdentif;
        data.IdLugaEven = registro.IdLugaEven;
        data.IdProDisp = registro.IdProDisp;
        data.IdTipo = registro.IdTipo;
        data.IdCaUnidad = registro.IdCaUnidad;
        data.Pnccantida = registro.Pnccantida;
        data.Pnccargador = registro.Pnccargador;
        data.PnccauLibe = registro.PnccauLibe;
        data.PncordFabr = registro.PncordFabr;
        data.PnccodProd = registro.PnccodProd;
        data.PncdesProd = registro.PncdesProd;
        data.Pncfecha = registro.Pncfecha;
        data.IdCausa = registro.IdCausa;
        data.PncindLibe = registro.PncindLibe;
        data.Pnclote = registro.Pnclote;
        return 0 < await _cotext.SaveChangesAsync();
    }
    return false;
}


        [HttpGet("GetProductoNoConformePorFecha/{Fecha}")]
        public async Task<List<NeoAPI.Models.Neo.ProNoCon>> ObtenerProductoNoConformePorFecha(DateTime Fecha){ 

        return await this._cotext.ProNoCons.Where(p => p.Pncfecha == Fecha).ToListAsync();
        }


        [HttpGet("GetProductoNoConformeEntreFechas/{fechaInicio}/{fechaFinal}")]
        public async Task<List<NeoAPI.Models.Neo.ProNoCon>> ObtenerProductoNoConformeEntreFechas(DateTime fechaInicio, DateTime fechaFinal){ 
  
            return await this._cotext.ProNoCons.Where(p => p.Pncfecha >= fechaInicio && p.Pncfecha <= fechaFinal).ToListAsync();
        }

        [HttpGet("GetProductoNoConformePorFiltro/{fechaInicio}/{fechaFinal}")]
        public async Task<List<NeoAPI.Models.Neo.ProNoCon>> ObtenerProductoNoConformePorFiltro(DateTime fechaInicio, DateTime fechaFinal){
            if(fechaInicio.Date == fechaFinal.Date){
                return await this.ObtenerProductoNoConformePorFecha(fechaInicio);
            }else if(fechaInicio.Date < fechaFinal.Date){
                return await this.ObtenerProductoNoConformeEntreFechas(fechaInicio,fechaFinal);
            }
            return await this.ObtenerProductoNoConformeEntreFechas(fechaFinal,fechaInicio);
        }



        [HttpGet("GetProductoNoConforme/{idRegistro}")]
        public async Task<ProNoConDTO?> ObtenerProductoNoConforme(int idRegistro)
{
    var proNoCon = await this._cotext.ProNoCons.Where(p => p.IdProNoCon == idRegistro).Include(p => p.IdCausaNavigation).ThenInclude(c => c.IdCausanteNavigation).FirstOrDefaultAsync();

    if (proNoCon == null)
    {
        return null;
    }

    return _mapper.Map<ProNoConDTO>(proNoCon);
}


        [HttpGet("GetProductoNoConformeConTodaLaData/{idRegistro}")]
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
}