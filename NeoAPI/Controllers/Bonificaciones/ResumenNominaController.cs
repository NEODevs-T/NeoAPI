using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Bonificaciones;
using NeoAPI.Models.Neo;

namespace NeoAPI.Controllers.Bonificaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumenNominaController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public ResumenNominaController(DbNeoIiContext _DbNeo, IMapper maper)
        {
            _context = _DbNeo;
            _mapper = maper;
        }

        //Obtener data por turno y dia 
        [HttpGet("GetData/{tipo:int}/{fechaInicio:datetime}/{fechaFinal:datetime}")]
        public async Task<ActionResult<List<ResumenGeneralDTO>>> GetResumenGeneral(int tipo, DateTime fechaInicio, DateTime fechaFinal)
        {
            string tipoi = tipo == 1 ? "Bono Producción" : "Incentivo";

            var result = await _context.Resumen
            .Where(r => r.Rfecha >= fechaInicio.Date
             && r.Rfecha <= fechaFinal.Date && r.IdTipIncenNavigation.Tinombre == tipoi)
            .Select(r => new
            {
                Nombre = r.IdPersonalNavigation.PeNombre,
                Apellido = r.IdPersonalNavigation.PeApellido,
                Ficha = r.IdPersonalNavigation.PeFicha,
                Grupo = r.IdPersonalNavigation.PeGrupo,
                Turno = r.Rturno,
                Concepto = r.IdTipIncenNavigation.Tinombre,
                Suplencia = r.IdTipSupleNavigation.Tscausa,
                FichaSuplida = r.Rsuplido,
                Pais = r.IdMontosNavigation.IdLineaNavigation.Master.IdPaisNavigation.Pnombre,
                Empresa = r.IdMontosNavigation.IdLineaNavigation.Master.IdEmpresaNavigation.Enombre,
                Centro = r.IdMontosNavigation.IdLineaNavigation.Master.IdCentroNavigation.Cnom,
                Linea = r.IdMontosNavigation.IdLineaNavigation.Lnom,
                PuestoTrabajo = r.IdMontosNavigation.IdPuesTrabNavigation.Ptnombre,
                Monto = r.IdMontosNavigation.Mmonto,
                Moneda = r.IdMontosNavigation.IdMonedaNavigation.Mtipo,
                FechaResumen = r.Rfecha,
                FechaPago = r.RfecPago,
                FichaResumen = r.RuserVali,
                FichaPago = r.RuserPago,
                AprobacionJefe = r.RaproJef
            })
             .AsNoTracking()
             .ToListAsync();


            return Ok(result);
        }

        //Obtener data total
        [HttpGet("GetDataTotal/{tipo:int}/{fechaInicio:datetime}/{fechaFinal:datetime}")]
        public async Task<ActionResult<List<Object>>> GetResumenGeneralTotal(int tipo, DateTime fechaInicio, DateTime fechaFinal)
        {
            string tipoi = tipo == 1 ? "Bono Producción" : "Incentivo";

            var result = await _context.Resumen
            .Where(r => r.Rfecha >= fechaInicio.Date && r.Rfecha <= fechaFinal.Date && r.IdTipIncenNavigation.Tinombre == tipoi)
            .GroupBy(r => new
            {
              r.IdMontosNavigation.IdLineaNavigation.Master.IdEmpresaNavigation.IdCompania,

                r.IdPersonalNavigation.PeFicha,
                r.IdPersonalNavigation.PeNombre,
                r.IdPersonalNavigation.PeApellido,
                r.IdMontosNavigation.IdLineaNavigation.Master.IdPaisNavigation.Pnombre,
                r.IdMontosNavigation.IdLineaNavigation.Master.IdEmpresaNavigation.Enombre,
                r.IdMontosNavigation.IdLineaNavigation.Master.IdCentroNavigation.Cnom,
                r.IdMontosNavigation.IdMonedaNavigation.Mtipo

            })
            .Select(g => new
            {
                IdCompany = g.Key.IdCompania,
                Ficha = g.Key.PeFicha,
                Nombre = g.Key.PeNombre,
                Apellido = g.Key.PeApellido,               
                Pais = g.Key.Pnombre,
                Empresa = g.Key.Enombre,
                Centro = g.Key.Cnom,
                Moneda = g.Key.Mtipo,
                Monto = g.Sum(x => x.IdMontosNavigation.Mmonto),

            })
            .AsNoTracking()
            .ToListAsync();

            return Ok(result);

        }

    }
}
