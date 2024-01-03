using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Maestra;

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
            bool ok = false;
            InfoAse? oka;
            Asentum? primerAsenta;

            oka = await this._context.InfoAses.Where(i => 
                                            i.Iaturno == filtroGT.turno &&
                                            i.Iagrupo == filtroGT.grupo && 
                                            i.Asenta.FirstOrDefault() != null &&
                                            i.IafechCrea.Date == DateTime.Now.Date 
                                            ).AsNoTracking().FirstOrDefaultAsync();
            if(oka != null){

                primerAsenta = await this._context.Asenta.Where(a => 
                                                        a.IdInfoAse == oka.IdInfoAse &&
                                                        a.IdRangoNavigation.IdVariableNavigation.IdSeccion == filtros.seccion &&
                                                        a.IdRangoNavigation.IdVariableNavigation.IdTipoVar == filtros.tipo &&
                                                        a.IdRangoNavigation.IdMaster == filtros.master &&
                                                        a.IdRangoNavigation.IdProducto == filtros.producto
                                                        ).AsNoTracking().FirstOrDefaultAsync();
                if(primerAsenta != null){
                    return  true;
                }
            }
            return false;
        }

        [HttpPost("AddAsentamientosDelDia")]
        public async Task<ActionResult<bool>> AddAsentamientosDelDia(InformeConAsentamientosDTO asentamientos){
            InfoAse informeDeAsentamientos = asentamientos.InformaDeAsentamientos;
            List<Asentum> listaAsentamientos = asentamientos.Asentamientos;

            //TODO: Pendiente cambio de horario segun pais
            //var info = TimeZoneInfo.FindSystemTimeZoneById("Venezuela Standard Time"); 
            //DateTimeOffset localServerTime = DateTimeOffset.Now;
            //informeDeAsentamientos.IafechCrea = TimeZoneInfo.ConvertTime(DateTime.Now, info)

            asentamientos.InformaDeAsentamientos.IafechCrea = DateTime.Now;

            foreach (var item in listaAsentamientos)
            {
                item.IdInfoAseNavigation = informeDeAsentamientos;
                if(item.IdRangoNavigation != null){
                    item.IdRango = item.IdRangoNavigation.IdRango;
                    item.IdRangoNavigation = null;
                }
                _context.Asenta.Add(item);
            }
            return await _context.SaveChangesAsync() > 0;            
        }

        [HttpPut("UpdateAsentamientosDelDia")]
        public async Task<ActionResult> UpdateAsentamientosDelDia(long idInforme,InformeConAsentamientosDTO asentamientos){
            InfoAse? data;
            bool correcto;

            if (asentamientos.InformaDeAsentamientos == null || idInforme != asentamientos.InformaDeAsentamientos.IdInfoAse) 
            {
                return BadRequest();
            }

            data = await _context.InfoAses.Where(i => i.IdInfoAse == idInforme).AsNoTracking().FirstOrDefaultAsync();

            if(data == null){
                return NotFound();
            }

            foreach (Asentum item in asentamientos.Asentamientos ?? new List<Asentum>())
            {
                //item.IdInfoAseNavigation = asentamientos.InformaDeAsentamientos;
                _context.Entry(item).State = EntityState.Modified;
            }

            try
            {
                correcto = await _context.SaveChangesAsync() > 0;

                if(!correcto){
                    return  BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
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

            infoAsentamientos.Asentamientos = new List<Asentum>();
            infoAsentamientos.Asentamientos.Add(new Asentum());
            infoAsentamientos.Asentamientos.Add(new Asentum());
            infoAsentamientos.InformaDeAsentamientos = new InfoAse();

            return infoAsentamientos;
        }
    }
}