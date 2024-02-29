using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using NeoAPI.DTOs.Asentamientos;

namespace NeoAPI.Controllers.RangoControl
{
    [ApiController]
    [Route("api/[controller]")]

    public class RangoControlController : ControllerBase
    {   
        private readonly DbNeoIiContext _context;
        private readonly ViewsContext _views;

        public RangoControlController(DbNeoIiContext context,ViewsContext views)
        {
            _context = context;
            _views = views;
        }

        [HttpGet("GetProductosPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<ProductosV>>> GetProductosPorLinea(int idLinea)
        {
            try{
                return await this._views.ProductosVs.Where(p => p.IdLinea == idLinea && p.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetSeccionesPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<SeccionesV>>> GetSeccionesPorLinea(int idLinea)
        {
            try{
                return await this._views.SeccionesVs.Where(s => s.IdLinea == idLinea && s.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetTipoVariblePorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<VarTipoV>>> GetTipoVariblePorLinea(int idLinea)
        {
            try{
                return await this._views.VarTipoVs.Where(t => t.IdLinea == idLinea && t.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetClasificacionVariblePorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<VarClasificacionV>>> GetClasificacionVariblePorLinea(int idLinea)
        {
            try{
                return await this._views.VarClasificacionVs.Where(c => c.IdLinea == idLinea && c.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
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
            if(listaVariables.Count == 0){
                return NotFound();
            }
            return listaVariables;
        }
    }
}