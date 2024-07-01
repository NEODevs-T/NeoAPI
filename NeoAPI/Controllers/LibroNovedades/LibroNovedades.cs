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
        public LibroNoveController(DbNeoIiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AddLibroNovedades")]
        public async Task<ActionResult<bool>> AddLibroNovedadesPorCorte(List<LibroNoveDTO> novedades)
        {
            List<LibroNove> data;
            foreach (var item in novedades)
            {
                item.LntiePerMi = -1;
                item.IdTipoNove = (int)DatosNovedad.SinPerdidaDeTiempo;
                item.IdAreaCar = (int)DatosNovedad.Operaciones;
                item.LnisPizUni = false;
                item.LnisResu = (int)DatosNovedad.NoResuelto;
            }

            data = _mapper.Map<List<LibroNove>>(novedades);
            _context.LibroNoves.AddRange(data);

            return Ok(await _context.SaveChangesAsync() > 0);
        }

        [HttpPost("AddLibroNovedadesNormal")]
        public async Task<ActionResult<bool>> AddLibroNovedadesNormal(List<LibroNoveDTO> novedades)
        {
            List<LibroNove> data;
            data = _mapper.Map<List<LibroNove>>(novedades);
            _context.LibroNoves.AddRange(data);
            return Ok(await _context.SaveChangesAsync() > 0);
        }

        [HttpGet("GetNoveadadPorIdParada/{idParada}")]
        public async Task<ActionResult<LibroNoveDTO>> GetNoveadadPorIdParada(string idParada)
        {
            LibroNove novedad;
            novedad = await this._context.LibroNoves.Where(x => x.IdParada == idParada).FirstOrDefaultAsync() ?? new LibroNove();
            return Ok(_mapper.Map<LibroNoveDTO>(novedad));
        }

        [HttpPut("UpdatenNovedad/{IdlibrNov:int}")]
        public async Task<ActionResult<bool>> ActualizacionNovedad(int IdlibrNov, LibroNoveDTO data)
        {
            LibroNove? dataNove = await this._context.LibroNoves.Where(x => x.IdlibrNov == IdlibrNov).FirstOrDefaultAsync();

            if (data != null && dataNove != null)
            {
                dataNove.IdLinea = data.IdLinea;
                dataNove.IdAreaCar = data.IdAreaCar;
                dataNove.IdEquipo = data.IdEquipo;
                dataNove.IdlibrNov = data.IdlibrNov;
                dataNove.IdParada = data.IdParada;
                dataNove.IdTipoNove = data.IdTipoNove;
                dataNove.Lndiscrepa = data.Lndiscrepa;
                dataNove.Lnfecha = data.Lnfecha;
                dataNove.LnfichaRes = data.LnfichaRes;
                dataNove.Lngrupo = data.Lngrupo;
                dataNove.LnisPizUni = data.LnisPizUni;
                dataNove.Lnobserv = data.Lnobserv;
                dataNove.LntiePerMi = data.LntiePerMi;
                dataNove.Lnturno = data.Lnturno;
                dataNove.IdCtpm = data.IdCtpm;
                dataNove.LnisResu = data.LnisResu;
                return Ok(0 < await _context.SaveChangesAsync());
            }
            return BadRequest(false);

        }

        [HttpGet("GetRegistroDeHoyPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<LibroNoveDTO>>> RegistroDeHoyPorLinea(int idLinea)
        {
            List<LibroNove> data = await this._context.LibroNoves.Where(t => t.IdLinea == idLinea && t.Lnfecha >= DateTime.Today && t.Lnfecha < DateTime.Today.AddDays(1)).ToListAsync();
            return Ok(_mapper.Map<List<LibroNoveDTO>>(data));
        }

        [HttpGet("GetObtenerNovedadePorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<LibroNoveDTO>>> ObtenerNovedadePorLinea(int idLinea)
        {
            List<LibroNove> data = await this._context.LibroNoves.Where(l => l.IdLinea == idLinea).ToListAsync();
            return Ok(_mapper.Map<List<LibroNoveDTO>>(data));
        }

        [HttpPut("UpdateGrupoRegistros")]
        public async Task<ActionResult<bool>> UpdateRegistros(List<LibroNoveDTO> novedades)
        {
            LibroNove? registro;
            foreach (var item in novedades)
            {
                registro = await this._context.LibroNoves.FirstOrDefaultAsync(x => x.IdlibrNov == item.IdlibrNov);
                if (registro != null)
                {
                    registro.LnisPizUni = item.LnisPizUni;
                }
                await _context.SaveChangesAsync();
            }
            return Ok(true);
        }

        //TODO: Revisar funcionamiento de esta consulta
        [HttpGet("GetLibroNovedadesPorFiltro/{idCentro:int}/{idDivision:int}/{idLinea:int}/{tipoClasi:int}/{filtroIsResuelto:int}/{fecha:DateTime}")]
        public async Task<ActionResult<List<LibroNove>>> GetLibroNovedadesPorFiltro(int idCentro, int idDivision, int idLinea, int tipoClasi, int filtroIsResuelto, DateTime fecha)
        {
            List<LibroNove> libroNov = new List<LibroNove>();
            if (tipoClasi == 0)
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdCtpm == tipoClasi))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdCtpm == tipoClasi))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdCtpm == tipoClasi))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdCtpm == tipoClasi))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
            }
            if (filtroIsResuelto == 0)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 0).ToList();
            }
            else if (filtroIsResuelto == 1)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 1).ToList();
            }
            return libroNov;
        }
    }
}