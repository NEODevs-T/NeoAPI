using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.SPI;
using NeoAPI.Models.SPI;


[ApiController]
[Route("api/[controller]")]
public class CargoController : ControllerBase
{
    private readonly DbSPIContext _context;

    public CargoController(DbSPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCargos(string? cia, string? codigo)
    {
        var query = _context.Cargos.AsQueryable();

        if (!string.IsNullOrEmpty(cia))
            query = query.Where(x => x.Ciacgo == cia);

        if (!string.IsNullOrEmpty(codigo))
            query = query.Where(x => x.Codcgo == codigo);

        var result = await query.Select(x => new CargoDTO
        {
            Ciacgo = x.Ciacgo,
            Codcgo = x.Codcgo,
            Descgo = x.Descgo,
            Ucrcgo = x.Ucrcgo,
            Fcrcgo = x.Fcrcgo,
            Uupcgo = x.Uupcgo,
            Fupcgo = x.Fupcgo
        }).ToListAsync();

        return Ok(result);
    }
}
