using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.SPI;
using NeoAPI.Models.SPI;

namespace NeoAPI.Controllers.SPI;

[ApiController]
[Route("api/[controller]")]
public class MaestroTrabajadorReController : ControllerBase
{
    private readonly DbSPIContext _context;

    public MaestroTrabajadorReController(DbSPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrabajadoresRetirados(
        string? ciafic,
        string? tpnfic,
        string? codfic,
        string? departamento,
        string? cargo)
    {
        var query = _context.MaestroTrabajadorRes
            .AsNoTracking()
            .Where(x => x.Fecret >= 20240103);

        if (!string.IsNullOrWhiteSpace(ciafic))
        {
            query = query.Where(x =>
                (x.Ciafic ?? "").Trim() == ciafic.Trim());
        }

        if (!string.IsNullOrWhiteSpace(tpnfic))
        {
            query = query.Where(x =>
                (x.Tpnfic ?? "").Trim() == tpnfic.Trim());
        }

        if (!string.IsNullOrWhiteSpace(codfic))
        {
            query = query.Where(x =>
                (x.Codfic ?? "").Trim() == codfic.Trim());
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                (x.Dptfic ?? "").Trim() == departamento.Trim());
        }

        if (!string.IsNullOrWhiteSpace(cargo))
        {
            query = query.Where(x =>
                (x.Cgofic ?? "").Trim() == cargo.Trim());
        }

        var result = await query
            .OrderByDescending(x => x.Fecret)
            .Select(x => new MaestroTrabajadorReDTO
            {
                Ciafic = x.Ciafic,
                Tpnfic = x.Tpnfic,
                Codfic = x.Codfic,
                Nomfi1 = x.Nomfi1,
                Nomfi2 = x.Nomfi2,
                Apefi1 = x.Apefi1,
                Apefi2 = x.Apefi2,
                Cedfic = x.Cedfic,
                Cgofic = x.Cgofic,
                Dptfic = x.Dptfic,
                Fecing = x.Fecing,
                Fecret = x.Fecret,
                Fecnac = x.Fecnac,
                Ucrfic = x.Ucrfic,
                Fcrfic = x.Fcrfic,
                Uupfic = x.Uupfic,
                Fupfic = x.Fupfic,
                Lcdfic = x.Lcdfic,
                Sexfic = x.Sexfic,
                Cdanfi = x.Cdanfi,
                Tlffic = x.Tlffic,
                Emlfic = x.Emlfic,
                Ficjef = x.Ficjef,
                Nacfic = x.Nacfic
            })
            .Take(10000)
            .ToListAsync();

        if (!result.Any())
        {
            return NotFound("No se encontraron trabajadores retirados.");
        }

        return Ok(result);
    }
}