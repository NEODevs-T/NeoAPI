using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.SPI;
using NeoAPI.Models.SPI;


[ApiController]
[Route("api/[controller]")]
public class MaestroTrabajadorController : ControllerBase
{
    private readonly DbSPIContext _context;

    public MaestroTrabajadorController(DbSPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrabajadores(
        string? ciafic,
        string? tpnfic,
        string? codfic,
        string? departamento,
        string? cargo)
    {
        var query = _context.MaestroTrabajadors.AsQueryable();

        if (!string.IsNullOrEmpty(ciafic))
            query = query.Where(x => (x.Ciafic ?? "").Trim() == ciafic.Trim());

        if (!string.IsNullOrEmpty(tpnfic))
            query = query.Where(x => (x.Tpnfic ?? "").Trim() == tpnfic.Trim());

        if (!string.IsNullOrEmpty(codfic))
            query = query.Where(x => (x.Codfic ?? "").Trim() == codfic.Trim());

        if (!string.IsNullOrEmpty(departamento))
            query = query.Where(x => (x.Dptfic ?? "").Trim() == departamento.Trim());

        if (!string.IsNullOrEmpty(cargo))
            query = query.Where(x => (x.Cgofic ?? "").Trim() == cargo.Trim());

        var result = await query.Select(x => new MaestroTrabajadorDTO
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
        }).ToListAsync();

        if (!result.Any())
            return NotFound("No hay trabajadores");

        return Ok(result);
    }
}
