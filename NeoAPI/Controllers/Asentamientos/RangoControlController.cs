using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.Asentamientos;

namespace NeoAPI.Controllers.RangoControl
{
    [ApiController]
    [Route("api/[controller]")]

    public class RangoControlController : ControllerBase
    {   
        private readonly DbNeoIiContext _context;

        public RangoControlController(DbNeoIiContext context)
        {
            _context = context;

        }

        [HttpGet("GetProductosPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<ProductosV>>> GetProductosPorLinea(int idLinea)
        {
            
            return await this._context.ProductosVs.Where(p => p.IdLinea == idLinea && p.Estado == true).ToListAsync();
            
        }

        [HttpGet("GetSeccionesPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<SeccionesV>>> GetSeccionesPorLinea(int idLinea)
        {
            
            return await this._context.SeccionesVs.Where(s => s.IdLinea == idLinea && s.Estado == true).ToListAsync();
            
        }

        [HttpGet("GetTipoVariblePorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<VarTipoV>>> GetTipoVariblePorLinea(int idLinea)
        {
            return await this._context.VarTipoVs.Where(t => t.IdLinea == idLinea && t.Estado == true).ToListAsync();
        }

        [HttpGet("GetClasificacionVariblePorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<VarClasificacionV>>> GetClasificacionVariblePorLinea(int idLinea)
        {
            return await this._context.VarClasificacionVs.Where(c => c.IdLinea == idLinea && c.Estado == true).ToListAsync();
        }

        [HttpGet("GetRangoDeControl")]
        public async Task<ActionResult<List<Rango>>> GetRangoDeControl([FromQuery] FiltrosRangoControlDTO filtros)
        {
            int VARIABLEMANUAL = 1;
            List<Rango>? listaVariables = null; 
            int producto = filtros.producto;
            int master = filtros.master;
            int tipo = filtros.tipo;
            int seccion = filtros.seccion;

            if(seccion != 0){
                    listaVariables = await this._context.Rangos.Where(
                                    x => x.IdProducto == producto && 
                                    x.IdMaster == master && 
                                    x.IdVariableNavigation.IdTipoVar == tipo && 
                                    x.IdVariableNavigation.IdSeccion == seccion && 
                                    x.IdVariableNavigation.IdClasiVar == VARIABLEMANUAL &&
                                    x.Ractivo == true)
                                    .AsNoTracking()
                                    .Include(r => r.IdVariableNavigation).ThenInclude(v => v.IdSeccionNavigation)
                                    .Include(r => r.IdVariableNavigation).ThenInclude(v => v.IdUnidadNavigation)
                                    .OrderBy(r => r.Rorden)
                                    .ToListAsync();
            }else{
                    listaVariables = await this._context.Rangos.Where(
                                    x => x.IdProducto == producto && 
                                    x.IdMaster == master && 
                                    x.IdVariableNavigation.IdTipoVar == tipo && 
                                    x.IdVariableNavigation.IdClasiVar == VARIABLEMANUAL &&
                                    x.Ractivo == true)
                                    .AsNoTracking()
                                    .Include(r => r.IdVariableNavigation).ThenInclude(v => v.IdSeccionNavigation)
                                    .Include(r => r.IdVariableNavigation).ThenInclude(v => v.IdUnidadNavigation)
                                    .OrderBy(r => r.Rorden)
                                    .ToListAsync();
            }

            if(listaVariables.Count == 0){
                    return NotFound();
            }
            return listaVariables;
        }
    }
}