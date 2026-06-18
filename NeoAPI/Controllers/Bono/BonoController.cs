using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models.Bono;

namespace NeoAPI.Controllers.Bono;

[ApiController]
[Route("api/[controller]")]
public class BonoController : ControllerBase
{
    private readonly DbNeoBonoContext _context;

    public BonoController(DbNeoBonoContext context)
    {
        _context = context;
    }
}