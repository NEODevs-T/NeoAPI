using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NeoAPI.ModelsDOCIng;

namespace NeoAPI.Controllers.Maestras
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class GlobalController : ControllerBase
    {
        private readonly DOCIngContext _DOCIng;
        private (int PAVECA, int CHEMPRO, int PANASA, int PAINSA) empresas {get; set;} = (PAVECA: 1,CHEMPRO: 2, PANASA: 3,PAINSA: 4);

        public GlobalController(DOCIngContext DOCIng)
        {
            _DOCIng = DOCIng;
        }

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        [HttpGet("GetGrupo/{idPais:int}")]
        public async Task<ActionResult<RotaCalidum>> GetPaises(int idPais)
        {
            int hora = DateTime.Now.Hour;
            int turno = 0; 
            int fecha = 0;


            if(idPais == this.empresas.PAVECA){
                if(hora >= 6 && hora < 18){
                    turno = 1;
                    fecha  = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                }else if(hora >= 0 && hora < 6){
                    turno = 2;
                    fecha = int.Parse(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"));
                }else{
                    turno = 2;
                    fecha = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                }
            }else if(idPais == this.empresas.CHEMPRO){

            }else if(idPais == this.empresas.PANASA){

            }else if(idPais == this.empresas.PAINSA){
            
            }else{
                return BadRequest();
            }
            return await this._DOCIng.RotaCalida.Where(r => r.Rcturno == turno && r.Rcfecha == fecha).FirstOrDefaultAsync() ?? new RotaCalidum();
        }
    }
}