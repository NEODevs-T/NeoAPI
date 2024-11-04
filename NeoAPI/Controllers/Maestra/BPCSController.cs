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
using NeoAPI.DTOs.Global;
using Microsoft.Identity.Client;

namespace NeoAPI.Controllers.Maestras
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class BPCSController : ControllerBase
    {
        private readonly PolybaseBPCSVenContext _PolybaseBPCSVen;
        private readonly PolybaseBPCSColContext _PolybaseBPCSVCol;
        private readonly PolybaseBPCSCenContext _PolybaseBPCSVCen;
        private readonly DbNeoIiContext _context;

        private (int PAVECA, int CHEMPRO, int PANASA, int PAINSA) empresas { get; set; } = (PAVECA: 1, CHEMPRO: 2, PANASA: 3, PAINSA: 4);
        private (int K10, int K129) centroPAINSA { get; set; } = (K10: 18, K129: 19);

        public BPCSController(DOCIngContext DOCIng, PolybaseBPCSVenContext polybaseBPCSVen, PolybaseBPCSColContext polybaseBPCSVCol, PolybaseBPCSCenContext polybaseBPCSVCen, IMapper mapper, DbNeoIiContext DbNeo,DbSPIContext SPI)
        {
            _PolybaseBPCSVen = polybaseBPCSVen;
            _PolybaseBPCSVCol = polybaseBPCSVCol;
            _PolybaseBPCSVCen = polybaseBPCSVCen;
            _context = DbNeo;
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

    }

    
}