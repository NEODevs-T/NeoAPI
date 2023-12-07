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

        [HttpGet("GetProductosPorLinea")]
        public async Task<ActionResult<List<ProductosV>>> GetProductosPorLinea(int idLinea)
        {
            try{
                return await this._views.ProductosVs.Where(p => p.IdLinea == idLinea).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetSeccionesPorLinea")]
        public async Task<ActionResult<List<SeccionesV>>> GetSeccionesPorLinea(int idLinea)
        {
            try{
                return await this._views.SeccionesVs.Where(s => s.IdLinea == idLinea).ToListAsync();
            }catch{
                return NotFound();
            }
        }
    }
}