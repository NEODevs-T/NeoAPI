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
        private (int K10, int K129) centroPAINSA {get; set;} = (K10: 18,K129: 19);

        public GlobalController(DOCIngContext DOCIng)
        {
            _DOCIng = DOCIng;
        }

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        [HttpGet("GetConversionHorarios/{idPais:int}")]
        public ActionResult<DateTime>  GetConversionHorarios(int idPais){
            DateTime date = DateTime.Now;
            DateTime dateReal = new DateTime();

            if(idPais == empresas.PAVECA){
                dateReal = date;
            }else if(idPais == empresas.CHEMPRO){
                dateReal = date;
            }else if(idPais == empresas.PANASA){
                dateReal = date.AddHours(-1);
            }else if(idPais == empresas.PAINSA){
                dateReal = date.AddHours(-2);
            }else{
                return BadRequest();
            }
            return dateReal;
        }

        /*
            //*Consideraciones de Carga de Infomacion:
                1) Siempre la info se va a guardar en Horario Venezuela
                2) La definicion del turno actual se va a realizar segun el horario del pais
                3) La fecha de guardado de los asentamientos va a hacer siempre el dia de comienzo del turno
        */

        [HttpGet("GetRotacion/{idPais:int}/{idCentro:int}")]
        public async Task<ActionResult<RotaCalidum>> GetRotacion(int idPais,int idCentro)
        {
            DateTime dateReal = this.GetConversionHorarios(idPais).Value;
            int hora = dateReal.Hour;
            int turno = 0;
            int fecha = 0;


            if(idPais == this.empresas.PAVECA){
                if(hora >= 6 && hora < 18){
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else if(hora >= 0 && hora < 6){
                    turno = 2;
                    fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                }else{
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }

            }else if(idPais == this.empresas.CHEMPRO){
                if(hora >= 6 && hora < 18){
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else if(hora >= 0 && hora < 6){
                    turno = 2;
                    fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                }else{
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }

            }else if(idPais == this.empresas.PANASA){
                if(hora >= 6 && hora < 14){
                    turno = 1;
                    fecha  = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else if(hora >= 14 && hora < 22){
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }else{
                    turno = 3;
                    if(hora >= 22 && hora < 24){
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else{
                        fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                    }
                }

            }else if(idPais == this.empresas.PAINSA){
                if(idCentro == this.centroPAINSA.K10){

                    if(hora >= 6 && hora < 13){
                        turno = 1;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else if(hora >= 13 && hora < 20){
                        turno = 2;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else{
                        turno = 3;
                        if(hora >= 20 && hora < 24){
                            fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                        }else{
                            fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                        }
                    }

                }else if(idCentro == this.centroPAINSA.K129){
                    
                    if(hora >= 7 && hora < 15){
                        turno = 1;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else if(hora >= 15 && hora < 23){
                        turno = 2;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }else{
                        turno = 3;
                        if(hora >= 23 && hora < 24){
                            fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                        }else{
                            fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                        }
                    }

                }else{
                    return BadRequest();
                }

            }else{
                return BadRequest();
            }

            try
            {
                if(idPais == empresas.PAVECA){
                    return await this._DOCIng.RotaCalida.Where(r => r.Rcturno == turno && r.Rcfecha == fecha).FirstOrDefaultAsync() ?? new RotaCalidum();
                }else{
                    RotaCalidum rotaCalidum = new RotaCalidum();
                    rotaCalidum.RcidRotCal = 0;
                    rotaCalidum.Rcfecha = fecha;
                    rotaCalidum.Rcturno = turno;
                    rotaCalidum.Rcgrupo = "0";
                    return rotaCalidum;
                }
            }
            catch
            {
                return NotFound();
            }
        }
    }
}