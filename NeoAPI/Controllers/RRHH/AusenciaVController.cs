using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.RRHHModels;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic.Global;
using NeoAPI.Controllers.Maestras;
using NeoAPI.Interface;

namespace NeoAPI.Controllers.RRHH
{
    [ApiController]
    [Route("api/[controller]")]
    public class AusenciaVController : ControllerBase
    {
        private readonly DbRRHHContext _context;

        public AusenciaVController(DbRRHHContext context)
        {
            _context = context;
        }

        // ✅ GET: api/AusenciaV
        [HttpGet]
        public async Task<IActionResult> GetAusencias(
            string? ficha,
            int? anio,
            int? mes,
            decimal? periodo)
        {
            var query = _context.AusenciaVs.AsQueryable();

            // ✅ filtros dinámicos
            if (!string.IsNullOrEmpty(ficha))
            {
                query = query.Where(x => x.Ficdnh == ficha);
            }

            if (anio.HasValue)
            {
                query = query.Where(x => x.Añodnh == (decimal)anio.Value);
            }

            if (mes.HasValue)
            {
                query = query.Where(x => x.Mesdnh == (decimal)mes.Value);
            }

            if (periodo.HasValue)
            {
                query = query.Where(x => x.Prddnh == periodo.Value);
            }

            query = query.Take(500);

            var result = await query.ToListAsync();

            if (!result.Any())
                return NotFound("No hay registros");

            return Ok(result);
        }
    }
}
