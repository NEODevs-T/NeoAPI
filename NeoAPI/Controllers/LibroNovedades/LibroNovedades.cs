using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;


namespace NeoAPI.Controllers.LibroNovedades
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
        public LibroNoveController(DbNeoIiContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AddLibroNovedades")]
        public async Task<ActionResult<bool>> AddLibroNovedadesPorCorte(List<LibroNoveDTO> novedades){
            List<LibroNove> data;
            foreach (var item in novedades)
            {
                item.LntiePerMi = -1;
                item.IdTipoNove = (int) DatosNovedad.SinPerdidaDeTiempo;
                item.IdAreaCar = (int) DatosNovedad.Operaciones;
                item.LnisPizUni = false;
                item.LnisResu = (int) DatosNovedad.NoResuelto;
            }

            data = _mapper.Map<List<LibroNove>>(novedades);
            _context.LibroNoves.AddRange(data);
            
            try{
                return Ok(await _context.SaveChangesAsync() > 0);
            }catch(Exception e){
                return BadRequest(e);
            }
        }

        [HttpPost("AddLibroNovedadesNormal")]
        public async Task<ActionResult<bool>> AddLibroNovedadesNormal(List<LibroNoveDTO> novedades){
            List<LibroNove> data;
            
            data = _mapper.Map<List<LibroNove>>(novedades);
            _context.LibroNoves.AddRange(data);
            
            try{
                return Ok(await _context.SaveChangesAsync() > 0);
            }catch(Exception e){
                return BadRequest(e);
            }
        }

        [HttpGet("ObtenerNoveadadPorIdParada/{idParada}")]
        public async Task<ActionResult<LibroNoveDTO>> ObtenerNoveadadPorIdParada(string idParada){
            LibroNove novedad;
            try{
                novedad =  await this._context.LibroNoves.Where(x => x.IdParada == idParada).FirstOrDefaultAsync() ?? new LibroNove();
                return _mapper.Map<LibroNoveDTO>(novedad);
            }catch(Exception e){
                return BadRequest(e);
            }
        }
    }
}