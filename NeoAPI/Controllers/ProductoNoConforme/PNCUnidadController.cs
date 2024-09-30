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


public class PNCUnidadController : ControllerBase 
    {

        private readonly DbNeoIiContext _cotext;

        private readonly IMapper _mapper;

        public PNCUnidadController (DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;       
        }

        [HttpGet("GetTodosLasUnidades")]
        public async Task<List<UnidadeDTO>> ObtenerTodosLasUnidades()
        {
            List<Unidad> UnidadLista =  await this._cotext.Unidads.Where(u => u.Uestado == true).ToListAsync();

            return _mapper.Map<List<UnidadeDTO>>(UnidadLista);
        }
    }

}