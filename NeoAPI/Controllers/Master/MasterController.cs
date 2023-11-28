using Microsoft.AspNetCore.Mvc;
using NeoAPI.Models;

namespace NeoAPI.Controllers.Master
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsentamientosController : ControllerBase
    {
        private readonly DbNeoIiContext _context;

        public AsentamientosController(DbNeoIiContext context)
        {
            _context = context;
        }

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios(){
            return TimeZoneInfo.GetSystemTimeZones();         
        }
    }
}