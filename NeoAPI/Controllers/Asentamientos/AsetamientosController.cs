using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models;
using NeoAPI.DTOs.Asentamientos;

namespace NeoAPI.Controllers.Asentamientos
{
    [ApiController]
    [Route("[controller]")]

    public class AsentamientosController : ControllerBase
    {
        private readonly DbNeoIiContext _context;

        public AsentamientosController(DbNeoIiContext context)
        {
            _context = context;
        }
        

        [HttpGet("GetRangoDeControl")]
        public async Task<ActionResult<List<Rango>>> GetRangoDeControl([FromQuery] Dictionary<string,int> filtros)
        {
            List<Rango> listaVariables;
            int producto = 0;
            int master = 0;
            int tipo = 0;
            int seccion = 0;

            foreach (var item in filtros)
            {
                if(item.Key == "Producto"){
                    producto = item.Value;
                }else if(item.Key == "Master"){
                    master = item.Value;
                }else if(item.Key == "Tipo"){
                    tipo = item.Value;
                }else if(item.Key == "Seccion"){
                    seccion = item.Value;
                }
            }
            
            listaVariables = await this._context.Rangos.Where(
                                x => x.IdProducto == producto && 
                                x.IdMaster == master && 
                                x.IdVariableNavigation.IdTipoVar == tipo && 
                                x.IdVariableNavigation.IdSeccion == seccion && 
                                x.Ractivo == true)
                                .AsNoTracking().ToListAsync();

            return listaVariables;
        }

        [HttpGet("GetIsAsentamientoHoy")]
        public async Task<bool> GetIsAsentamientoHoy([FromQuery] Dictionary<string,string> filtros){
            bool ok = false;
            InfoAse? oka;
            Asentum? primerAsenta;

            oka = await this._context.InfoAses.Where(i => 
                                            i.Iaturno == filtros["Turno"] &&
                                            i.Iagrupo == filtros["Grupo"] && 
                                            i.Asenta.FirstOrDefault() != null &&
                                            i.IafechCrea.Value.Date == DateTime.Now.Date 
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
        public async Task<bool> AddAsentamientosDelDia(InformeConAsentamientos asentamientos){
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

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios(){
            return TimeZoneInfo.GetSystemTimeZones();         
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