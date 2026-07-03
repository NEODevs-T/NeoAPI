using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class VHistoricoCargController : ControllerBase
{
    private readonly DbRRHHContext _context;

    public VHistoricoCargController(DbRRHHContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetHistoricoCargo(
        string? compania,
        string? tipoNomina,
        string? ficha,
        string? departamento,
        decimal? fechaCambio,
        string? codigoCargo,
        string? nombre,
        string? apellido)
    {
        var query = _context.VHistoricoCargs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(compania))
        {
            query = query.Where(x =>
                (x.Ciahcg ?? "").Trim() == compania.Trim());
        }

        if (!string.IsNullOrWhiteSpace(tipoNomina))
        {
            query = query.Where(x =>
                (x.Tpnfic ?? "").Trim() == tipoNomina.Trim());
        }

        if (!string.IsNullOrWhiteSpace(ficha))
        {
            query = query.Where(x =>
                (x.Fichcg ?? "").Trim() == ficha.Trim());
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                (x.Dptfic ?? "").Trim() == departamento.Trim());
        }

        if (!string.IsNullOrWhiteSpace(codigoCargo))
        {
            query = query.Where(x =>
                (x.Codcgo ?? "").Trim() == codigoCargo.Trim());
        }

        if (!string.IsNullOrWhiteSpace(nombre))
        {
            query = query.Where(x =>
                (x.Nomfi1 ?? "")
                    .Trim()
                    .ToUpper()
                    .Contains(nombre.Trim().ToUpper()));
        }

        if (!string.IsNullOrWhiteSpace(apellido))
        {
            query = query.Where(x =>
                (x.Apefi1 ?? "")
                    .Trim()
                    .ToUpper()
                    .Contains(apellido.Trim().ToUpper()));
        }

        if (fechaCambio.HasValue)
        {
            query = query.Where(x =>
                x.Fecchc == fechaCambio.Value);
        }

        var result = await query
            .Select(x => new VHistoricoCargDTO
            {
                Ciahcg = (x.Ciahcg ?? "").Trim(),
                Fichcg = (x.Fichcg ?? "").Trim(),
                Tpnfic = (x.Tpnfic ?? "").Trim(),
                Cgohcg = (x.Cgohcg ?? "").Trim(),
                Fecchc = x.Fecchc,
                Nomfi1 = (x.Nomfi1 ?? "").Trim(),
                Apefi1 = (x.Apefi1 ?? "").Trim(),
                Dptfic = (x.Dptfic ?? "").Trim(),
                Desdpt = (x.Desdpt ?? "").Trim(),
                Codcgo = (x.Codcgo ?? "").Trim(),
                Descgo = (x.Descgo ?? "").Trim()
            })
            .ToListAsync();

        if (!result.Any())
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(result);
    }
}