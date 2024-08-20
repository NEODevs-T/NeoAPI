using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using NeoAPI.DTOs.BPSC;
using NeoAPI.Models.PolybaseBPCSCen;
using NeoAPI.Models.PolybaseBPCSCol;
using NeoAPI.Models.PolybaseBPCSVen;
using NeoAPI.Models.Neo;
using NeoAPI.ModelsDOCIng;
using NeoAPI.Interface;
using NeoAPI.Logic;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeoAPI.DTOs.GlobalCo;

namespace NeoAPI.Controllers.Maestras
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class GlobalController : ControllerBase
    {
        private readonly DOCIngContext _DOCIng;
        private readonly PolybaseBPCSVenContext _PolybaseBPCSVen;
        private readonly PolybaseBPCSColContext _PolybaseBPCSVCol;
        private readonly PolybaseBPCSCenContext _PolybaseBPCSVCen;
        private readonly IMapper _mapper;
        private readonly DbNeoIiContext _context;
        private (int PAVECA, int CHEMPRO, int PANASA, int PAINSA) empresas { get; set; } = (PAVECA: 1, CHEMPRO: 2, PANASA: 3, PAINSA: 4);
        private (int K10, int K129) centroPAINSA { get; set; } = (K10: 18, K129: 19);

        public GlobalController(DOCIngContext DOCIng, PolybaseBPCSVenContext polybaseBPCSVen, PolybaseBPCSColContext polybaseBPCSVCol, PolybaseBPCSCenContext polybaseBPCSVCen, IMapper mapper, DbNeoIiContext DbNeo)
        {
            _DOCIng = DOCIng;
            _PolybaseBPCSVen = polybaseBPCSVen;
            _PolybaseBPCSVCol = polybaseBPCSVCol;
            _PolybaseBPCSVCen = polybaseBPCSVCen;
            _mapper = mapper;
            _context = DbNeo;
        }

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        [HttpGet("GetConversionHorarios/{idEmpresa:int}")]
        public ActionResult<DateTime> GetConversionHorarios(int idEmpresa)
        {
            IRotacionLogic rotacionLogic = new RotacionLogic();
            DateTime? tiempo = rotacionLogic.ConversionHorarios(idEmpresa);
            if (tiempo != null)
            {
                return tiempo;
            }
            return BadRequest();
        }

        /*
            //* Consideraciones de Carga de Infomacion:
                1) Siempre la info se va a guardar en Horario Venezuela
                2) La definicion del turno actual se va a realizar segun el horario del pais
                3) La fecha de guardado de los asentamientos va a hacer siempre el dia de comienzo del turno
        */

        [HttpGet("GetRotacion/{idEmpresa:int}/{idCentro:int}")]

        public async Task<ActionResult<RotaCalidumDTO>> GetRotacion(int idEmpresa, int idCentro)
        {

            DateTime dateReal = this.GetConversionHorarios(idEmpresa).Value;
            int hora = dateReal.Hour;
            int turno = 0;
            int fecha = 0;


            if (idEmpresa == this.empresas.PAVECA)
            {
                if (hora >= 6 && hora < 18)
                {
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }
                else if (hora >= 0 && hora < 6)
                {
                    turno = 2;
                    fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                }
                else
                {
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }

            }
            else if (idEmpresa == this.empresas.CHEMPRO)
            {
                if (hora >= 6 && hora < 18)
                {
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }
                else if (hora >= 0 && hora < 6)
                {
                    turno = 2;
                    fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                }
                else
                {
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }

            }
            else if (idEmpresa == this.empresas.PANASA)
            {
                if (hora >= 6 && hora < 14)
                {
                    turno = 1;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }
                else if (hora >= 14 && hora < 22)
                {
                    turno = 2;
                    fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                }
                else
                {
                    turno = 3;
                    if (hora >= 22 && hora < 24)
                    {
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                    }
                }

            }
            else if (idEmpresa == this.empresas.PAINSA)
            {
                if (idCentro == this.centroPAINSA.K10)
                {

                    if (hora >= 6 && hora < 13)
                    {
                        turno = 1;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }
                    else if (hora >= 13 && hora < 20)
                    {
                        turno = 2;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        turno = 3;
                        if (hora >= 20 && hora < 24)
                        {
                            fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                        }
                        else
                        {
                            fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                        }
                    }

                }
                else if (idCentro == this.centroPAINSA.K129)
                {

                    if (hora >= 7 && hora < 15)
                    {
                        turno = 1;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }
                    else if (hora >= 15 && hora < 23)
                    {
                        turno = 2;
                        fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                    }
                    else
                    {
                        turno = 3;
                        if (hora >= 23 && hora < 24)
                        {
                            fecha = int.Parse(dateReal.ToString("yyyyMMdd"));
                        }
                        else
                        {
                            fecha = int.Parse(dateReal.AddDays(-1).ToString("yyyyMMdd"));
                        }
                    }

                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest();
            }

            try
            {
                if (idEmpresa == empresas.PAVECA)
                {
                    var data = await this._DOCIng.RotaCalida.Where(r => r.Rcturno == turno && r.Rcfecha == fecha).FirstOrDefaultAsync() ?? new RotaCalidum();
                    return Ok(_mapper.Map<RotaCalidumDTO>(data));


                }
                else
                {
                    RotaCalidum rotaCalidum = new RotaCalidum();
                    rotaCalidum.RcidRotCal = 0;
                    rotaCalidum.Rcfecha = fecha;
                    rotaCalidum.Rcturno = turno;
                    rotaCalidum.Rcgrupo = "0";
                    return Ok(_mapper.Map<RotaCalidumDTO>(rotaCalidum));
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("GetProductosActuales/{idLinea:int}")]
        public async Task<ActionResult<List<OrdenFabricacionDTO>>> GetProductosActuales(int idLinea)
        {
            int idEmpresa;
            int CentroTrabajo;
            const string OrdenesAbiertas = "5";
            MaestraV? maestra;
            List<OrdenFabricacionDTO> ordenesFabricacionDTOList = new List<OrdenFabricacionDTO>();
            OrdenFabricacionDTO ordenFabricacionDTO;

            maestra = await _context.MaestraVs.Where(m => m.IdLinea == idLinea).FirstOrDefaultAsync();

            if (maestra == null)
            {
                return BadRequest();
            }

            idEmpresa = maestra.IdEmpresa;
            CentroTrabajo = Int32.Parse(maestra.CentroDeTrabajo);

            var result = from Fso in _PolybaseBPCSVen.Fsos
                         join Iim in _PolybaseBPCSVen.Iims
                         on Fso.Sprod equals Iim.Iprod
                         where Fso.Swrkc == CentroTrabajo && Fso.Sstat.Contains(OrdenesAbiertas)
                         select new { Fso.Sprod, Fso.Sstat, Iim.Idesc };

            foreach (var item in result)
            {
                ordenFabricacionDTO = new OrdenFabricacionDTO();
                ordenFabricacionDTO.Status = item.Sstat;
                ordenFabricacionDTO.DescProducto = item.Idesc.Trim();
                ordenFabricacionDTO.CodProducto = item.Sprod.Trim();
                ordenesFabricacionDTOList.Add(ordenFabricacionDTO);
            }

            ordenesFabricacionDTOList = ordenesFabricacionDTOList.GroupBy(f => f.CodProducto).Select(f => f.First()).ToList();

            return ordenesFabricacionDTOList;
        }
    }
}