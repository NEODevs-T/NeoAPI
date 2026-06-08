using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic.Global;
using NeoAPI.Controllers.Maestras;
using NeoAPI.Interface;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;


namespace NeoAPI.Controllers.RRHH
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepososVController : ControllerBase
    {
        private readonly DbRRHHContext _context;

        public RepososVController(DbRRHHContext context)
        {
            _context = context;
        }

        // ✅ FILTRO PRINCIPAL (CORREGIDO)
        [HttpGet("GetRepososFiltrados")]
        public async Task<ActionResult<List<RepososVDTO>>> GetRepososFiltrados(
            [FromQuery] DateTime? desde,
            [FromQuery] DateTime? hasta,
            [FromQuery] string? compania,
            [FromQuery] string? enfermedad,
            [FromQuery] string? departamento,
            [FromQuery] string? ficha,
            [FromQuery] string? nombre,
            [FromQuery] string? apellido)
        {
            try
            {
                var query = _context.RepososVs
                    .AsNoTracking()
                    .AsQueryable();

                    // ✅ Validación de fechas
                    if (desde.HasValue && hasta.HasValue && desde > hasta)
                        return BadRequest("La fecha desde no puede ser mayor que hasta.");

                    // ✅ Conversión correcta DateTime → DateOnly
                    if (desde.HasValue)
                    {
                        var desdeDate = DateOnly.FromDateTime(desde.Value);
                        query = query.Where(x => x.FechaDesde >= desdeDate);
                    }

                    if (hasta.HasValue)
                    {
                        var hastaDate = DateOnly.FromDateTime(hasta.Value);
                        query = query.Where(x => x.FechaHasta <= hastaDate);
                    }


                // ✅ Filtros opcionales
                if (!string.IsNullOrWhiteSpace(compania))
                    query = query.Where(x => x.Compania == compania.Trim());

                if (!string.IsNullOrWhiteSpace(enfermedad))
                    query = query.Where(x => x.DescripcionCausa.Contains(enfermedad.Trim()));

                if (!string.IsNullOrWhiteSpace(departamento))
                    query = query.Where(x => x.Departamento.Contains(departamento.Trim()));

                if (!string.IsNullOrWhiteSpace(ficha))
                    query = query.Where(x => x.Ficha == ficha.Trim());

                if (!string.IsNullOrWhiteSpace(nombre))
                    query = query.Where(x => x.Nombre.Contains(nombre.Trim()));

                if (!string.IsNullOrWhiteSpace(apellido))
                    query = query.Where(x => x.Apellido.Contains(apellido.Trim()));

                var result = await query.ToListAsync();

                if (!result.Any())
                    return NotFound("No se encontraron registros.");

                var data = result.Select(x => new RepososVDTO
                {
                    Ficha = x.Ficha,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Departamento = x.Departamento,
                    FechaDesde = x.FechaDesde,
                    FechaHasta = x.FechaHasta,
                    Reintegro = x.Reintegro,
                    DescripcionCausa = x.DescripcionCausa,
                    CanDiasReposo = x.CanDiasReposo,
                    OrigenReposo = x.OrigenReposo,
                    Compania = x.Compania,
                    TipoNomina = x.TipoNomina
                }).ToList();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // ✅ COMBOS (SIN CAMBIOS)

        [HttpGet("GetCompanias")]
        public async Task<ActionResult<List<string>>> GetCompanias()
        {
            var data = await _context.RepososVs
                .Select(x => x.Compania)
                .Where(x => x != null)
                .Distinct()
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("GetEnfermedades")]
        public async Task<ActionResult<List<string>>> GetEnfermedades()
        {
            var data = await _context.RepososVs
                .Select(x => x.DescripcionCausa)
                .Where(x => x != null)
                .Distinct()
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("GetDepartamentos")]
        public async Task<ActionResult<List<string>>> GetDepartamentos()
        {
            var data = await _context.RepososVs
                .Select(x => x.Departamento)
                .Where(x => x != null)
                .Distinct()
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("GetFichas")]
        public async Task<ActionResult<List<string>>> GetFichas()
        {
            var data = await _context.RepososVs
                .Select(x => x.Ficha)
                .Where(x => x != null)
                .Distinct()
                .ToListAsync();

            return Ok(data);
        }
    }
}
