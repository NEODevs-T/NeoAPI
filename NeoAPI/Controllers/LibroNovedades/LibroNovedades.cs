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

        // [HttpPut("UpdatenNovedad/{IdlibrNov:int}")]
        // public async Task<ActionResult<bool>> ActualizacionNovedad(int IdlibrNov, LibroNoveDTO data)
        // {
        //     LibroNove? dataNove = await this._context.LibroNoves
        //         .Where(x => x.IdlibrNov == IdlibrNov).FirstOrDefaultAsync();

        //     if (data != null && dataNove != null)
        //     {
        //         dataNove.IdLinea = data.IdLinea;
        //         dataNove.IdAreaCar = data.IdAreaCar;
        //         dataNove.IdEquipo = data.IdEquipo;
        //         dataNove.IdlibrNov = data.IdlibrNov;
        //         dataNove.IdParada = data.IdParada;
        //         dataNove.IdTipoNove = data.IdTipoNove;
        //         dataNove.Lndiscrepa = data.Lndiscrepa;
        //         dataNove.Lnfecha = data.Lnfecha;
        //         dataNove.LnfichaRes = data.LnfichaRes;
        //         dataNove.Lngrupo = data.Lngrupo;
        //         dataNove.LnisPizUni = data.LnisPizUni;
        //         dataNove.Lnobserv = data.Lnobserv;
        //         dataNove.LntiePerMi = data.LntiePerMi;
        //         dataNove.Lnturno = data.Lnturno;
        //         dataNove.IdCtpm = data.IdCtpm;
        //         dataNove.LnisResu = data.LnisResu;
        //         dataNove.IdMaster = data.IdMaster;

        //         return Ok(0 < await _context.SaveChangesAsync());
        //     }
        //     return BadRequest(false);

        // }

        [HttpPut("UpdateNovedad/{IdlibrNov:int}")]
        public async Task<ActionResult<bool>> ActualizacionNovedad(int IdlibrNov, LibroNoveDTO data)
        {
<<<<<<< HEAD
            // Buscar la entidad en la base de datos
            LibroNove? dataNove = await _context.LibroNoves.FirstOrDefaultAsync(x => x.IdlibrNov == IdlibrNov);
=======
            LibroNove? dataNove = await this._context.LibroNoves.Where(x => x.IdlibrNov == IdlibrNov).FirstOrDefaultAsync();
            
>>>>>>> 319313c72ce329ff4b62dbafc26ee87ebfed686f

            if (dataNove == null)
            {
                return NotFound();
            }

            if (data != null)
            {
                // Asignar manualmente las propiedades del DTO a la entidad
                dataNove.IdLinea = data.IdLinea;
                dataNove.IdAreaCar = data.IdAreaCar;
                dataNove.IdEquipo = data.IdEquipo;
                dataNove.IdTipoNove = data.IdTipoNove;
                dataNove.Lndiscrepa = data.Lndiscrepa;
                dataNove.Lnfecha = data.Lnfecha;
                dataNove.LnfichaRes = data.LnfichaRes;
                dataNove.Lngrupo = data.Lngrupo;
                dataNove.Lnturno = data.Lnturno;
                dataNove.IdParada = data.IdParada;
                dataNove.LnisPizUni = data.LnisPizUni;
                dataNove.Lnobserv = data.Lnobserv;
                dataNove.LntiePerMi = data.LntiePerMi;
                dataNove.IdCtpm = data.IdCtpm;
                dataNove.LnisResu = data.LnisResu;
                dataNove.IdMaster = data.IdMaster;

                // Guardar los cambios
                await _context.SaveChangesAsync();

                return Ok(true);
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
        [HttpGet("GetLibroNovedadesPorFiltro/{idCentro:int}/{idDivision:int}/{idLinea:int}/{IdCTPM:int}/{LNIsResu:int}/{fecha:DateTime}")]
        public async Task<ActionResult<List<LibroNoveDTO>>> GetLibroNovedadesPorFiltro(int idCentro, int idDivision, int idLinea, int IdCTPM, int LNIsResu, DateTime fecha)
        {
            List<LibroNove> libroNov = new List<LibroNove>();
            if (IdCTPM == 0)
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
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
            }
            if (LNIsResu == 0)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 0).ToList();
            }
            else if (LNIsResu == 1)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 1).ToList();
            }

            return Ok(_mapper.Map<List<LibroNoveDTO>>(libroNov));
        }

        [HttpGet("GetObtenerLibroNovedadesPorFiltroEntreFechas/{idCentro:int}/{idDivision:int}/{idLinea:int}/{IdCTPM:int}/{LNIsResu:int}/{fechaInicio:DateTime}/{fechaFinal:DateTime}")]
        public async Task<ActionResult<List<LibroNoveDTO>>> ObtenerLibroNovedadesPorFiltroEntreFechas(int idCentro, DateTime fechaInicio, DateTime fechaFinal, int idDivision, int idLinea, int IdCTPM, int LNIsResu)
        {
            List<LibroNove> libroNov = new List<LibroNove>();

            if (IdCTPM != 0)
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdCtpm == IdCTPM))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).Include(l => l.IdAreaCarNavigation).AsNoTracking().ToListAsync();
                }
            }

            if (LNIsResu == 0)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 0).ToList();
            }
            else if (LNIsResu == 1)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 1).ToList();
            }
            return Ok(_mapper.Map<List<LibroNoveDTO>>(libroNov)); ;
        }
        [HttpGet("GetObtenerLibroNovedadesDelAreaQueCarga/{fecha:DateTime}/{idCentro:int}/{idDivision:int}/{idLinea:int}/{IdCTPM:int}/{IdAreaCar:int}/{LNIsResu:int}")]
        public async Task<ActionResult<List<LibroNoveDTO>>> ObtenerLibroNovedadesDelAreaQueCarga(DateTime fecha, int idCentro, int idDivision, int idLinea, int IdCTPM, int IdAreaCar, int LNIsResu)
        {
            List<LibroNove> libroNov = new List<LibroNove>();
            if (IdCTPM == 0)
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date == fecha.Date) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
            }
            return _mapper.Map<List<LibroNoveDTO>>(libroNov);
        }
        [HttpGet("GetObtenerLibroNovedadesDelAreaQueCargaEntreFechas/{fechaInicio:DateTime}/{fechaFinal:DateTime}/{idCentro:int}/{idDivision:int}/{idLinea:int}/{IdCTPM:int}/{IdAreaCar:int}/{LNIsResu:int}")]
        public async Task<ActionResult<List<LibroNoveDTO>>> ObtenerLibroNovedadesDelAreaQueCargaEntreFechas(DateTime fechaInicio, DateTime fechaFinal, int idCentro, int idDivision, int idLinea, int IdCTPM, int IdAreaCar, int LNIsResu)
        {
            List<LibroNove> libroNov = new List<LibroNove>();
            if (IdCTPM != 0)
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivision).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdCtpm == IdCTPM) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
            }
            else
            {
                if (idCentro != 0 && idDivision != 0 && idLinea != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision && t.IdLinea == idLinea) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0 && idDivision != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro && t.IdMasterNavigation.IdDivision == idDivision) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else if (idCentro != 0)
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdMasterNavigation.IdCentro == idCentro) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
                else
                {
                    libroNov = await this._context.LibroNoves.Where(t => (t.Lnfecha.Date >= fechaInicio.Date && t.Lnfecha.Date <= fechaFinal.Date) && (t.IdAreaCar == IdAreaCar))
                    .Include(t => t.IdMasterNavigation.IdLineaNavigation).Include(L => L.IdMasterNavigation.IdDivisionNavigation).Include(L => L.IdMasterNavigation.IdCentroNavigation).Include(t => t.IdTipoNoveNavigation).AsNoTracking().ToListAsync();
                }
            }

            if (LNIsResu == 0)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 0).ToList();
            }
            else if (LNIsResu == 1)
            {
                libroNov = libroNov.Where(l => l.LnisResu == 1).ToList();
            }
            return _mapper.Map<List<LibroNoveDTO>>(libroNov);
        }

        [HttpGet("GetCalcularCumplimiento/{fechaInicio:DateTime}/{fechaFinal:DateTime}/{tipo}/{idCondicional:int}")]
        public async Task<ActionResult<(IEnumerable<IGrouping<DateTime, LibroNoveDTO>>, int, int, double)>> CalcularCumplimiento(DateTime fechaInicio, DateTime fechafinal, string tipo, int idCondicional)
        {
            IEnumerable<IGrouping<DateTime, LibroNoveDTO>> data;
            List<LibroNove> libroNov = new List<LibroNove>();
            int diasReales = 0;
            double cumplimiento;
            int diasToricos = (fechafinal.Day - fechaInicio.Day) + 1;
            if (tipo == "Centro")
            {
                libroNov = await this._context.LibroNoves.Where(l => l.IdMasterNavigation.IdCentro == idCondicional && (l.Lnfecha.Date >= fechaInicio.Date && l.Lnfecha.Date <= fechafinal.Date))
                .Include(t => t.IdMasterNavigation.IdLineaNavigation).ToListAsync(); //.Select(l => new LibroNove() {Lnfecha = l.Lnfecha})
            }
            else if (tipo == "Division")
            {
                libroNov = await this._context.LibroNoves.Where(l => l.IdMasterNavigation.IdDivision == idCondicional && (l.Lnfecha.Date >= fechaInicio.Date && l.Lnfecha.Date <= fechafinal.Date))
                .Include(t => t.IdMasterNavigation.IdLineaNavigation).ToListAsync(); //.Select(l => new LibroNove() {Lnfecha = l.Lnfecha})
            }
            else if (tipo == "Linea")
            {
                libroNov = await this._context.LibroNoves.Where(l => l.IdMasterNavigation.IdLinea == idCondicional && (l.Lnfecha.Date >= fechaInicio.Date && l.Lnfecha.Date <= fechafinal.Date))
                .Include(t => t.IdMasterNavigation.IdLineaNavigation).ToListAsync(); //.Select(l => new LibroNove() {Lnfecha = l.Lnfecha})
            }
            var mappeador = _mapper.Map<List<LibroNoveDTO>>(libroNov);
            data = mappeador.GroupBy(l => l.Lnfecha.Date);
            diasReales = data.Count();
            cumplimiento = (double)diasReales / diasToricos;
            return (data, diasToricos, diasReales, cumplimiento);
        }
        [HttpGet("GetObtenerLibroPorId/{idRegistro:int}")]
        public async Task<ActionResult<LibroNoveDTO>>? ObtenerLibroPorId(int idRegistro)
        {
            LibroNove data = await this._context.LibroNoves.Where(l => l.IdlibrNov == idRegistro).FirstOrDefaultAsync() ?? new LibroNove();
            return _mapper.Map<LibroNoveDTO>(data);
        }
    }
}