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
    public class PNCDisposicionDefinitivaController : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;


        public PNCDisposicionDefinitivaController(DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }

        [HttpGet("GetTodosLasDisposicionDefinitiva")]
        public async Task<List<DisDefiDTO>> ObtenerTodosLasDisposicionDefinitiva()
        {
            List<DispDefi> disdefisLista = await this._cotext.DispDefis.Where(d => d.Ddestado == true).ToListAsync();

            return _mapper.Map<List<DisDefiDTO>>(disdefisLista);
        }
    }
}