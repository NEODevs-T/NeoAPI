using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.SPI;
using NeoAPI.Models.SPI;


[ApiController]
[Route("api/[controller]")]
public class DepartamentoController : ControllerBase
{
    private readonly DbSPIContext _context;

    public DepartamentoController(DbSPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartamentos(string? cia, string? codigo)
    {
        var query = _context.Departamentos.AsQueryable();

        if (!string.IsNullOrEmpty(cia))
            query = query.Where(x => x.Ciadpt == cia);

        if (!string.IsNullOrEmpty(codigo))
            query = query.Where(x => x.Coddpt == codigo);

        var result = await query.Select(x => new DepartamentoDTO
        {
            Ciadpt = x.Ciadpt,
            Coddpt = x.Coddpt,
            Desdpt = x.Desdpt,
            Ucrdpt = x.Ucrdpt,
            Fcrdpt = x.Fcrdpt,
            Uupdpt = x.Uupdpt,
            Fupdpt = x.Fupdpt
        }).ToListAsync();

        return Ok(result);
    }
}
