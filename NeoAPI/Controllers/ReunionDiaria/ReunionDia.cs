using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models.Neo;

namespace ReunionDia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LibroNoveController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;
        enum DatosNovedad
        {
            SinPerdidaDeTiempo = 19,
            Operaciones = 1,
            NoResuelto = 0
        }
        public LibroNoveController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
