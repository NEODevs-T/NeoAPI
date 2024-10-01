using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.PNC;


namespace NeoAPI.Controllers.PNC
{
    [ApiController]
    [Route("api/[controller]")]

    public class PNCIdentificacionController : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;

        private readonly IMapper _mapper;

        public PNCIdentificacionController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }
        [HttpGet("GetTodosLosIdentifi")]
        public async Task<List<IdentifDTO>> ObtenerTodosLosIdentifi()
        {
            List<Identifi> identifsLista = await this._cotext.Identifis.Where(i => i.Iestado == true).ToListAsync();

            return _mapper.Map<List<IdentifDTO>>(identifsLista);
        }
    }
}