using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using NeoAPI.DTOs.Asentamientos;

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
        public async Task<ActionResult<bool>> GetIsAsentamientoHoy([FromQuery] Dictionary<string,string> filtros){
            bool ok = false;
            InfoAse? oka;
            Asentum? primerAsenta;

            oka = await this._context.InfoAses.Where(i => 
                                            i.Iaturno == filtros["Turno"] &&
                                            i.Iagrupo == filtros["Grupo"] && 
                                            i.Asenta.FirstOrDefault() != null &&
                                            i.IafechCrea.Date == DateTime.Now.Date 
                                            ).AsNoTracking().FirstOrDefaultAsync();
            if(oka != null){

                primerAsenta = await this._context.Asenta.Where(a => 
                                                        a.IdInfoAse == oka.IdInfoAse &&
                                                        a.IdRangoNavigation.IdVariableNavigation.IdSeccion == int.Parse(filtros["Seccion"]) &&
                                                        a.IdRangoNavigation.IdVariableNavigation.IdTipoVar == int.Parse(filtros["TipoVar"]) &&
                                                        a.IdRangoNavigation.IdMaster == int.Parse(filtros["Master"]) &&
                                                        a.IdRangoNavigation.IdProducto == int.Parse(filtros["Producto"])
                                                        ).AsNoTracking().FirstOrDefaultAsync();
                if(primerAsenta != null){
                    return  true;
                }
            }
            return false;
        }

        [HttpPost("AddAsentamientosDelDia")]
        public async Task<ActionResult<bool>> AddAsentamientosDelDia(InformeConAsentamientos asentamientos){
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
                _context.Asenta.Add(item);
            }
            return await _context.SaveChangesAsync() > 0;            
        }

        //Todo: Pendiente probar

        [HttpPut("UpdateAsentamientosDelDia")]
        public async Task<ActionResult> UpdateAsentamientosDelDia(long idInforme,InformeConAsentamientos asentamientos){
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


        //TODO: REVISION
        [HttpPost("AddOrUpdateAsentamientosDelDia")]
        public async Task<ActionResult<bool>> AddOrUpdateAsentamientosDelDia([FromQuery] InformeConAsentamientos asentamientos,[FromQuery] Dictionary<string,string> filtros){
            ActionResult<bool> band = false;
            ActionResult<bool> estado = false;

            band = await this.GetIsAsentamientoHoy(filtros);

            if (band.Value == true){
                estado = await this.UpdateAsentamientosDelDia(asentamientos.InformaDeAsentamientos.IdInfoAse,asentamientos) == NoContent();
            }else{
                estado = await this.AddAsentamientosDelDia(asentamientos);
            }

            return estado;
        }

        
        [HttpGet("GetAsentamientosViews")]
        public async Task<ActionResult<List<ValoresDeAsentamientosV>>> GetAsentamientosViews([FromQuery] Dictionary<string,string> filtros){
            InfoAse? informe = new InfoAse();
            List<ValoresDeAsentamientosV>? valores = new List<ValoresDeAsentamientosV>();
            informe = await this._context.InfoAses.Where(i => 
                                        i.Iaturno == filtros["Turno"] &&
                                        i.Iagrupo == filtros["Grupo"] && 
                                        i.IafechCrea.Date == DateTime.Parse(filtros["Fecha"]).Date
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
        public InformeConAsentamientos GetEjemploDataAsentamientos(){
            InformeConAsentamientos infoAsentamientos = new InformeConAsentamientos();

            infoAsentamientos.Asentamientos = new List<Asentum>();
            infoAsentamientos.Asentamientos.Add(new Asentum());
            infoAsentamientos.Asentamientos.Add(new Asentum());
            infoAsentamientos.InformaDeAsentamientos = new InfoAse();

            return infoAsentamientos;
        }
    }
}