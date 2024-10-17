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
using NeoAPI.Models.SPI;
using NeoAPI.ModelsDOCIng;
using NeoAPI.Interface;
using NeoAPI.Logic;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using NeoAPI.DTOs.GlobalCo;
using Microsoft.Identity.Client;

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
        private readonly DbSPIContext _SPI;

        private (int PAVECA, int CHEMPRO, int PANASA, int PAINSA) empresas { get; set; } = (PAVECA: 1, CHEMPRO: 2, PANASA: 3, PAINSA: 4);
        private (int K10, int K129) centroPAINSA { get; set; } = (K10: 18, K129: 19);

        public GlobalController(DOCIngContext DOCIng, PolybaseBPCSVenContext polybaseBPCSVen, PolybaseBPCSColContext polybaseBPCSVCol, PolybaseBPCSCenContext polybaseBPCSVCen, IMapper mapper, DbNeoIiContext DbNeo,DbSPIContext SPI)
        {
            _DOCIng = DOCIng;
            _PolybaseBPCSVen = polybaseBPCSVen;
            _PolybaseBPCSVCol = polybaseBPCSVCol;
            _PolybaseBPCSVCen = polybaseBPCSVCen;
            _mapper = mapper;
            _context = DbNeo;
            _SPI = SPI;
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
                if (idEmpresa == empresas.PAVECA)
                {
                    RotaCalidum data = await this._DOCIng.RotaCalida.Where(r => r.Rcturno == turno && r.Rcfecha == fecha).FirstOrDefaultAsync() ?? new RotaCalidum();
                    if (data.Rcgrupo == null){
                        data.RcidRotCal = 0;
                        data.Rcfecha = fecha;
                        data.Rcturno = turno;
                        data.Rcgrupo = "0";
                    }
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
        //TODO: Hacer metodos para otros paises

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
            CentroTrabajo = Int32.Parse(maestra.CentroDeTrabajo ?? "0");
            
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
        //TODO: Hacer metodos para otros paises

        [HttpGet("GetProductosActualesPorCentroDeTrabajoVen/{centroTrabajo}")]
        public ActionResult<List<OrdenFabricacionDTO>> GetProductosActualesPorCentroDeTrabajoVen(string centroTrabajo)
        {
            int centroTrabajoInt;
            const string OrdenesAbiertas = "5";
            List<OrdenFabricacionDTO> ordenesFabricacionDTOList = new List<OrdenFabricacionDTO>();
            OrdenFabricacionDTO ordenFabricacionDTO;

            centroTrabajoInt = Int32.Parse(centroTrabajo);

            var result = from Fso in _PolybaseBPCSVen.Fsos
                        join Iim in _PolybaseBPCSVen.Iims
                        on Fso.Sprod equals Iim.Iprod
                        where Fso.Swrkc == centroTrabajoInt && Fso.Sstat.Contains(OrdenesAbiertas)
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

        [HttpGet("GetNombreProductoPorCodigoVen/{codigo}")]
        public string GetNombreProductoPorCodigoVen(string codigo)
        {
            string producto = "";
            var result = from Iim in _PolybaseBPCSVen.Iims
                        where Iim.Iprod == codigo
                        select new { Iim.Iprod, Iim.Idesc };
            foreach (var item in result)
            {
                producto = item.Idesc;
            }
            return producto;
        }

        [HttpGet("GetPersonalPorFicha/{ficha}")]

        public async Task<ActionResult<PersonalDTO>> GetPersonalPorFicha(string ficha)
        {
            PersonalDTO? persona = new PersonalDTO();

            var result = from Mt in _SPI.MaestroTrabajadors 
                        join Dep in _SPI.Departamentos on Mt.Dptfic equals Dep.Coddpt
                        join Car in _SPI.Cargos on Mt.Cgofic equals Car.Codcgo
                        join Com in _SPI.Compañias on Mt.Ciafic equals Com.Codcia
                        where Car.Ciacgo == Mt.Ciafic && Mt.Codfic.Contains(ficha)
                        select new { Mt.Nomfi1, Mt.Nomfi2, Mt.Apefi1,Mt.Apefi2,Dep.Desdpt,Car.Descgo,Com.Nomci2 };

            foreach (var item in result)
            {
                persona.primerNombre = item.Nomfi1;
                persona.segundoNombre = item.Nomfi2;
                persona.primerApellido = item.Apefi1;
                persona.segundoApellido = item.Apefi2;
                persona.departamento = item.Desdpt;
                persona.cargo = item.Descgo;
                persona.compania = item.Nomci2;
                break;
            }

            return Ok(persona);
        }


    }
}