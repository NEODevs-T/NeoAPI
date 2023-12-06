using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models;
using NeoAPI.ModelsViews;
using NeoAPI.DTOs.Asentamientos;

namespace NeoAPI.Controllers.RangoControl
{
    [ApiController]
    [Route("api/[controller]")]

    public class RangoControlController : ControllerBase
    {   
        private readonly DbNeoIiContext _context;
        private readonly ViewsContext _views;

        public RangoControlController(DbNeoIiContext context,ViewsContext views)
        {
            _context = context;
            _views = views;
        }
    }
}