using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;

namespace NeoAPI.Controllers.Asentamientos
{
    [AllowAnonymous]
    [Route("/[controller]")]
    [ApiController]



    public class CorteDiscrepanciaController : ControllerBase
    {
        private readonly DbNeoIiContext _context;

        public CorteDiscrepanciaController(DbNeoIiContext _DbNeo)
        {
            _context = _DbNeo;
        }

       
        //Obtener Asentamientos fuera de rango 
        [HttpGet("AsentamientoFueraRango")]
        public async Task<ActionResult<List<Asentum>>> GetAsentamientosFueraRango()
        {
            var result = await _context.Asenta
           .Include(r => r.IdRangoNavigation)
           .Where(f => (f.Avalor > f.IdRangoNavigation.Rmax | f.Avalor < f.IdRangoNavigation.Rmin == true) && f.IdInfoAseNavigation.IafechCrea==DateTime.Now.Date)
           .ToListAsync();

            return Ok(result);
        }
    }
}