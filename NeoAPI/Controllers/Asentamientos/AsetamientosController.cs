using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models.Neo;
using NeoAPI.ModelsViews;
using NeoAPI.ModelsDOCIng;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic;
using NeoAPI.Controllers.Maestras;
using NeoAPI.Interface;

namespace NeoAPI.Controllers.Asentamientos
{
    [ApiController]
    [Route("api/[controller]")]

    public class AsentamientosController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly ViewsContext _views;

        public AsentamientosController(DbNeoIiContext context,ViewsContext views)
        {
            _context = context;
            _views = views;
        }
        

        [HttpGet("GetIsAsentamientoHoy/{idEmpresa:int}/{idCentro:int}")]
        public async Task<ActionResult<bool>> GetIsAsentamientoHoy([FromQuery] FiltrosRangoControlDTO filtros, int idEmpresa, int idCentro){
            InfoAse? oka;
            Asentum? primerAsenta;
            IRotacionLogic rotacionLogic = new RotacionLogic();
            RotaCalidum rotacion = rotacionLogic.Rotacion(idEmpresa,idCentro);

            int anio = Int32.Parse(rotacion.Rcfecha.ToString().Substring(0,4));
            int mes = Int32.Parse(rotacion.Rcfecha.ToString().Substring(4,2));
            int dia = Int32.Parse(rotacion.Rcfecha.ToString().Substring(6,2));

            DateTime fecha = new DateTime(anio,mes,dia);

            oka = await this._context.InfoAses.Where(i => 
                                            i.Iaturno == rotacion.Rcturno.ToString() &&
                                            i.Asenta.Where(a => 
                                                            a.IdRangoNavigation.IdVariableNavigation.IdSeccion == filtros.seccion &&
                                                            a.IdRangoNavigation.IdVariableNavigation.IdTipoVar == filtros.tipo &&
                                                            a.IdRangoNavigation.IdMaster == filtros.master &&
                                                            a.IdRangoNavigation.IdProducto == filtros.producto
                                                        ).FirstOrDefault() != null &&
                                            i.IafechCrea.Date == fecha
                                            ).AsNoTracking().FirstOrDefaultAsync();

            if(oka != null){
                return true;
            }
            return false;
        }

        [HttpPost("AddAsentamientosDelDia/{idEmpresa:int}/{idPais:int}/{idCentro:int}")]
        public async Task<ActionResult<bool>> AddAsentamientosDelDia(int idEmpresa,int idPais,int idCentro,InformeConAsentamientosDTO asentamientos){

            InfoAse informeDeAsentamientos = new InfoAse();
            InfoAseDTO informeDeAsentamientosDTO = asentamientos.InformaDeAsentamientosDTO;
            IRotacionLogic rotacionLogic = new RotacionLogic();
            RotaCalidum rotacion = rotacionLogic.Rotacion(idEmpresa,idCentro);

            int anio = Int32.Parse(rotacion.Rcfecha.ToString().Substring(0,4));
            int mes = Int32.Parse(rotacion.Rcfecha.ToString().Substring(4,2));
            int dia = Int32.Parse(rotacion.Rcfecha.ToString().Substring(6,2));

            informeDeAsentamientos.Iagrupo = informeDeAsentamientosDTO.Iagrupo;
            informeDeAsentamientos.Iaturno = informeDeAsentamientosDTO.Iaturno;
            informeDeAsentamientos.Iaficha = informeDeAsentamientosDTO.Iaficha;
            informeDeAsentamientos.Iaobser = informeDeAsentamientosDTO.Iaobser;
            informeDeAsentamientos.IafechCrea = new DateTime(anio,mes,dia);
            informeDeAsentamientos.IafechReal = DateTime.Now;

            List<AsentumDTO> listaAsentamientosDTO = asentamientos.AsentamientosDTO ?? new List<AsentumDTO>();
            List<Asentum> listaAsentamientos = new List<Asentum>(listaAsentamientosDTO.Count);
            for (int i = 0; i < listaAsentamientosDTO.Count; i++)
            {
                listaAsentamientos.Add(new Asentum());
                listaAsentamientos[i].Avalor =  listaAsentamientosDTO[i].Avalor;
                listaAsentamientos[i].AisActivo = listaAsentamientosDTO[i].AisActivo;
                listaAsentamientos[i].IdRango = listaAsentamientosDTO[i].IdRango;
                listaAsentamientos[i].Aobserv = listaAsentamientosDTO[i].Aobserv;
                listaAsentamientos[i].IdInfoAseNavigation = informeDeAsentamientos;
            }

            try{
                
                _context.Asenta.AddRange(listaAsentamientos);
                return await _context.SaveChangesAsync() > 0;    
            
            }catch(Exception e){
                return BadRequest(e);
            }
        }
        
        [HttpGet("GetAsentamientosViews")]
        public async Task<ActionResult<List<ValoresDeAsentamientosV>>> GetAsentamientosViews([FromQuery] FiltroGTDTO filtro, [FromQuery] string Fecha){
            InfoAse? informe = new InfoAse();
            List<ValoresDeAsentamientosV>? valores = new List<ValoresDeAsentamientosV>();
            informe = await this._context.InfoAses.Where(i => 
                                        i.Iaturno == filtro.turno &&
                                        i.Iagrupo == filtro.grupo && 
                                        i.IafechCrea.Date == DateTime.Parse(Fecha).Date
                                        ).AsNoTracking().FirstOrDefaultAsync();
            if(informe != null){
                valores = await this._views.ValoresDeAsentamientosVs.Where(v => v.IdInfoAse == informe.IdInfoAse).AsNoTracking().ToListAsync();
            }

            if(valores == null || informe == null){
                return NotFound();
            }
            return valores;
        }

        [HttpGet("GetEjemploDataAsentamientos")]
        public InformeConAsentamientosDTO GetEjemploDataAsentamientos(){
            InformeConAsentamientosDTO infoAsentamientos = new InformeConAsentamientosDTO();

            infoAsentamientos.AsentamientosDTO = new List<AsentumDTO>();
            infoAsentamientos.AsentamientosDTO.Add(new AsentumDTO());
            infoAsentamientos.AsentamientosDTO.Add(new AsentumDTO());
            infoAsentamientos.InformaDeAsentamientosDTO = new InfoAseDTO();

            return infoAsentamientos;
        }
    }
}