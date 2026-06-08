using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.SPI;
using NeoAPI.Models.SPI;


[ApiController]
[Route("api/[controller]")]
public class CompañiaController : ControllerBase
{
    private readonly DbSPIContext _context;

    public CompañiaController(DbSPIContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanias(string? codcia)
    {
        var query = _context.Compañias.AsQueryable();

        if (!string.IsNullOrEmpty(codcia))
            query = query.Where(x => x.Codcia == codcia);

        var result = await query.Select(x => new CompañiaDTO
        {
            Codcia = x.Codcia,
            Nomci1 = x.Nomci1,
            Nomci2 = x.Nomci2,
            Rifci1 = x.Rifci1,
            Dirci1 = x.Dirci1,
            Dirci2 = x.Dirci2,
            Dirci3 = x.Dirci3,
            Ucrcia = x.Ucrcia,
            Uupcia = x.Uupcia,
            Fcrcia = x.Fcrcia,
            Fupcia = x.Fupcia
        }).ToListAsync();

        return Ok(result);
    }
}
