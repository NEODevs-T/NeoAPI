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



public class PNCPropuestaDisposicionController : ControllerBase
    
    {


        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCPropuestaDisposicionController(DbNeoIiContext context, IMapper maper)
        {
            _cotext = context;
            _mapper = maper;
        }

        [HttpGet("GetTodasLasPropuestaDisposicion")]

        public async Task<List<ProDispDTO>> ObtenerTodasLasPropuestaDisposicion()
        {
            List<PropDisp> proDispsLista = await this._cotext.PropDisps.Where(p => p.Pdestado == true).ToListAsync();

            return _mapper.Map<List<ProDispDTO>>(proDispsLista);
        }

        [HttpPost("AddPropuestaDisposicion/{registro}")]
        public async Task<bool> AddPropuestaDisposicion(ProDispDTO registro)
        {
            var entidad = _mapper.Map<PropDisp>(registro);
            
            this._cotext.PropDisps.Add(entidad);
            
            return await _cotext.SaveChangesAsync() > 0;
        }
    
    }

}
