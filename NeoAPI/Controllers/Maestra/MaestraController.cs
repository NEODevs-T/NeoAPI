using Microsoft.AspNetCore.Mvc;
using NeoAPI.Models;

namespace NeoAPI.Controllers.Maestra
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaestraController : ControllerBase
    {
        private readonly DbNeoIiContext _context;

        public MaestraController(DbNeoIiContext context)
        {
            _context = context;
        }

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios(){
            return TimeZoneInfo.GetSystemTimeZones();         
        }
    }
}