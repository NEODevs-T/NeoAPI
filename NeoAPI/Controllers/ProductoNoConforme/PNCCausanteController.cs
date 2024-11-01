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

    public class PNCCausanteController : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCCausanteController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;

            _mapper = mapper;
        }

        [HttpGet("GetTodosLosCausantes")]
        public async Task<List<CausanteDTO>> ObtenerTodosLosCausantes()
        {
            List<Causante> causantelista = await this._cotext.Causantes.Where(c => c.Cestado == true).ToListAsync();

            return _mapper.Map<List<CausanteDTO>>(causantelista);
        }

    }

}