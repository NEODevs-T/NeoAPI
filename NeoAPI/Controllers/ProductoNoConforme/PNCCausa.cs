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


public class PNCCausa : ControllerBase
    {

        private readonly DbNeoIiContext _cotext;
        private readonly IMapper _mapper;

        public PNCCausa (DbNeoIiContext context, IMapper mapper)
        {
            _cotext = context;
            _mapper = mapper;
        }

        [HttpGet("GetTodosLasCausas")]
        public async Task<List<causaDTO>> ObtenerTodasLasCausas()
        {
            List<Causa> causaLista = await this._cotext.Causas.Where(p => p.Cestado == true).ToListAsync();
            
            return _mapper.Map<List<causaDTO>>(causaLista);
        }

        [HttpGet("GetTodosLasCausasPorCausante")]
        public async Task<List<causaDTO>> ObtenerLasCausasPorCausante(int idCausante)
        {
            List<Causa> CausasPorCausanteLista = await this._cotext.Causas.Where(p => p.Cestado == true && p.IdCausante == idCausante).ToListAsync();
            return _mapper.Map<List<causaDTO>>(CausasPorCausanteLista);
        }


        [HttpPost("AddPropuestaDisposicion")]
                public async Task<bool> AddPropuestaDisposicion(causaDTO registro)
        {
            var entidades = _mapper.Map<Causa>(registro);
            
            this._cotext.Causas.Add(entidades);
            
            return await _cotext.SaveChangesAsync() > 0;
        }
    
    }


    }
