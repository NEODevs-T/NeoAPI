using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;


namespace NeoAPI.Controllers.Avisador
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvisadorController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public AvisadorController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AddRegistros")]
        public async Task<ActionResult<bool>> AddRegistros((List<CambFecDTO> lista1, List<CambStatDTO> lista2) data)
        {
            List<CambFec> lista1_1 = _mapper.Map<List<CambFec>>(data.lista1);
            List<CambStat> lista2_2 = _mapper.Map<List<CambStat>>(data.lista2);

            foreach (var item in lista1_1)
            {
                this._context.CambFecs.Add(item);

            }

            foreach (var item in lista2_2)
            {
                this._context.CambStats.Add(item);
            }
            return Ok(await _context.SaveChangesAsync() > 0);
        }
    }


}