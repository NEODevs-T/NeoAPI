using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic;

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
        

        [HttpGet("GetIsAsentamientoHoy")]
        public async Task<ActionResult<bool>> GetIsAsentamientoHoy([FromQuery] FiltrosRangoControlDTO filtros,[FromQuery] FiltroGTDTO filtroGT){
            InfoAse? oka;
            Asentum? primerAsenta;

            oka = await this._context.InfoAses.Where(i => 
                                            i.Iaturno == filtroGT.turno &&
                                            i.Iagrupo == filtroGT.grupo && 
                                            i.Asenta.Where(a => 
                                                            a.IdRangoNavigation.IdVariableNavigation.IdSeccion == filtros.seccion &&
                                                            a.IdRangoNavigation.IdVariableNavigation.IdTipoVar == filtros.tipo &&
                                                            a.IdRangoNavigation.IdMaster == filtros.master &&
                                                            a.IdRangoNavigation.IdProducto == filtros.producto
                                                        ).FirstOrDefault() != null &&
                                            i.IafechCrea.Date == DateTime.Now.Date 
                                            ).AsNoTracking().FirstOrDefaultAsync();

            if(oka != null){
                return true;
            }
            return false;
        }

        [HttpPost("AddAsentamientosDelDia")]
        public async Task<ActionResult<bool>> AddAsentamientosDelDia(int idEmpresa,InformeConAsentamientosDTO asentamientos){

            //TODO: Pendiente cambio de horario segun pais
            //var info = TimeZoneInfo.FindSystemTimeZoneById("Venezuela Standard Time"); 
            //DateTimeOffset localServerTime = DateTimeOffset.Now;
            //informeDeAsentamientos.IafechCrea = TimeZoneInfo.ConvertTime(DateTime.Now, info)

            InfoAse informeDeAsentamientos = new InfoAse();
            InfoAseDTO informeDeAsentamientosDTO = asentamientos.InformaDeAsentamientosDTO ?? new InfoAseDTO();
            IRotacionLogic rotacion = new RotacionLogic();

            informeDeAsentamientos.Iagrupo = informeDeAsentamientosDTO.Iagrupo;
            informeDeAsentamientos.Iaturno = informeDeAsentamientosDTO.Iaturno;
            informeDeAsentamientos.Iaficha = informeDeAsentamientosDTO.Iaficha;
            informeDeAsentamientos.Iaobser = informeDeAsentamientosDTO.Iaobser;
            informeDeAsentamientos.IafechCrea = DateTime.Now;
            informeDeAsentamientos.IafechBpcs = rotacion.ObtenerFechaBPCS(idEmpresa);

            List<AsentumDTO> listaAsentamientosDTO = asentamientos.AsentamientosDTO ?? new List<AsentumDTO>();
            List<Asentum> listaAsentamientos = new List<Asentum>(listaAsentamientosDTO.Count);
            for (int i = 0; i < listaAsentamientosDTO.Count; i++)
            {
                listaAsentamientos.Add(new Asentum());
                listaAsentamientos[i].Avalor =  listaAsentamientosDTO[i].Avalor;
                listaAsentamientos[i].AisActivo = listaAsentamientosDTO[i].AisActivo;
                listaAsentamientos[i].IdRango = listaAsentamientosDTO[i].IdRango;
                listaAsentamientos[i].Aobserv = listaAsentamientosDTO[i].Aobserv;
            }

            try{
                foreach (var item in listaAsentamientos)
                {
                    item.IdInfoAseNavigation = informeDeAsentamientos;
                    _context.Asenta.Add(item);
                }
                return await _context.SaveChangesAsync() > 0;    
            }catch(Exception e){
                return BadRequest(e);
            }
        }

        // [HttpPut("UpdateAsentamientosDelDia")]
        // public async Task<ActionResult> UpdateAsentamientosDelDia(long idInforme,InformeConAsentamientosDTO asentamientos){
        //     InfoAse? data;
        //     bool correcto;

        //     if (asentamientos.InformaDeAsentamientos == null || idInforme != asentamientos.InformaDeAsentamientos.IdInfoAse) 
        //     {
        //         return BadRequest();
        //     }

        //     data = await _context.InfoAses.Where(i => i.IdInfoAse == idInforme).AsNoTracking().FirstOrDefaultAsync();

        //     if(data == null){
        //         return NotFound();
        //     }

        //     foreach (AsentumDTO item in asentamientos.AsentamientosDTO ?? new List<AsentumDTO>())
        //     {
        //         //item.IdInfoAseNavigation = asentamientos.InformaDeAsentamientos;
        //         var asenta = 
        //         _context.Entry(item).State = EntityState.Modified;
        //     }

        //     try
        //     {
        //         correcto = await _context.SaveChangesAsync() > 0;

        //         if(!correcto){
        //             return  BadRequest();
        //         }
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         return NotFound();
        //     }
        //     return NoContent();
        // }
        
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