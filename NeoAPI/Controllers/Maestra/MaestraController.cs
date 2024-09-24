using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models.Neo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NeoAPI.DTOs.Maestra;
using NeoAPI.DTOs.ReunionDiaria;

namespace NeoAPI.Controllers.Maestras
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MaestraController : ControllerBase
    {


        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;
        private readonly DbNeoIiContext _neoVieja;

        public MaestraController(DbNeoIiContext DbNeo, IMapper maper, DbNeoIiContext neoVieja)
        {
            _context = DbNeo;
            _mapper = maper;
            _neoVieja = neoVieja;
        }
        // public static List<Centro> centro = new List<Centro> { };
        public static List<Linea> linea = new List<Linea> { };
        public static List<Empresa> empresa = new List<Empresa> { };
        public static List<Pai> pais = new List<Pai> { };
        public static List<Division> div = new List<Division> { };
        public static List<Ksf> ksfs = new List<Ksf>();
        public static List<RespoReu> resporeu = new List<RespoReu>();
        public static List<ReuDium> reunionditabla = new List<ReuDium>();
        public static List<Division> divisions = new List<Division>();
        public static List<AsistenReu> asistenreus = new List<AsistenReu>();
        public static List<CargoReu> cargoreus = new List<CargoReu>();
        public static List<StatsAsisDto> StatsAsis = new List<StatsAsisDto>();
        public static List<EquipoEam> equipos = new List<EquipoEam>();
        public static List<EquipoEam> equiposlinea = new List<EquipoEam>();
        public static EquipoDTO equipoinsertar = new EquipoDTO();

        [HttpGet("GetPaises")]
        public async Task<ActionResult<List<PaiDTO>>> GetPaises()
        {

            List<Pai> data = await this._context.Pais.Where(p => p.Pestado == true).ToListAsync();
            return Ok(_mapper.Map<List<PaiDTO>>(data));

        }
        [HttpGet("GetEmpresas/{idPais:int}")]
        public async Task<ActionResult<List<EmpresasVDTO>>> GetEmpresas(int idPais)
        {
            List<EmpresasV> data = await this._context.EmpresasVs.Where(e => e.IdPais == idPais && e.Estado == true).ToListAsync();
            return Ok(_mapper.Map<List<EmpresasVDTO>>(data));
        
        }

        [HttpGet("GetCentros/{idEmpresa:int}")]
        public async Task<ActionResult<List<CentrosVDTO>>> GetCentros(int idEmpresa)
        {
                List<CentrosV> data = await this._context.CentrosVs.Where(c => c.IdEmpresa == idEmpresa && c.Estado == true).ToListAsync();
                return Ok(_mapper.Map<List<CentrosVDTO>>(data));
        }

        [HttpGet("GetCentrosJT/{cent}")]
        public async Task<ActionResult<List<CentrosVDTO>>> ObtenerCentrosJT(string cent)
        {
            List<CentrosV> centro = new List<CentrosV> { };
            string cen = "";
            int idempresa = 0;

            if (cent.Length > 3)
            {
                cen = cent.Substring(0, 3);
                idempresa = int.Parse(cent.Substring(3));
            }

            if (cen == "All")
            {
                centro = await _context.CentrosVs
                .Where(c => c.IdEmpresa == idempresa)
                    .ToListAsync();
            }

            else
            {
                int centroid = int.Parse(cent);

                centro = await _context.CentrosVs
                    .Where(c => c.IdCentro == centroid)
                    .ToListAsync();
            }


            return Ok(_mapper.Map<List<CentrosVDTO>>(centro));
        }
        [HttpGet("GetAllCentros/")]
        public async Task<ActionResult<List<CentrosVDTO>>> GetAllCentros()
        {
                List<CentrosV> data = await this._context.CentrosVs.Where(c => c.Estado == true).ToListAsync();
                return Ok(_mapper.Map<List<CentrosVDTO>>(data));
        }

        [HttpGet("GetDivisiones/{idCentro:int}")]
        public async Task<ActionResult<List<DivisionesVDTO>>> GetDivisiones(int idCentro)
        {
            List<DivisionesV> data = await this._context.DivisionesVs.Where(v => v.IdCentro == idCentro && v.Estado == true).ToListAsync();
            return Ok(_mapper.Map<List<DivisionesVDTO>>(data));
        }

        [HttpGet("GetAllLineas")]
        public async Task<ActionResult<List<LineaVDTO>>> GetAllLineas()
        {
            List<LineaV> data = await this._context.LineaVs.Where(l => l.Estado == true).ToListAsync();
            return Ok(_mapper.Map<List<LineaVDTO>>(data));
        }

        //      javier metodo
        [HttpGet("GetEquipos/{idCentro}")]
        public async Task<ActionResult<List<EquipoEamDTO>>> EquiposEAM(string idCentro)
        {
            List<EquipoEam> listaEquipo = new List<EquipoEam>();

            if (idCentro == "All")
            {
                listaEquipo = await _context.EquipoEams
                    .Include(x => x.IdLineaNavigation)
                    .Where(x => x.EestaEam == true)
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(_mapper.Map<List<EquipoEamDTO>>(listaEquipo));
            }
            else
            {
                int idcentro = int.Parse(idCentro);

                var result = await _context.Masters
                    .Where(x => x.IdCentro == idcentro)
                    .Include(x => x.IdLineaNavigation)
                    .ThenInclude(x => x.EquipoEams)
                    .AsNoTracking()
                    .ToListAsync();

                // TODO: Ver la implentacion de las modificaciones de este metodo

                foreach (var item in result)
                {
                    listaEquipo.AddRange(item.IdLineaNavigation.EquipoEams);
                }
                return Ok(_mapper.Map<List<EquipoEamDTO>>(listaEquipo));
            }
        }

        //TODO: Sujeta a revision
        [HttpGet("GetEquiposPorLinea/{linea}")]
        public async Task<ActionResult<List<EquipoEam>>> EquiposEAMxLinea(string linea)
        {

            int idlinea = int.Parse(linea);



            var result = await _context.Masters
                .Where(x => x.IdLineaNavigation.IdLinea == idlinea)
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<List<EquipoEamDTO>>(result));
        }


        [HttpGet("GetLineas/{idDivision:int}")]
        public async Task<ActionResult<List<LineaVDTO>>> GetLineas(int idDivision)
        {
            List<LineaV> data = await this._context.LineaVs.Where(l => l.IdDivision == idDivision && l.Estado == true).ToListAsync();
            return Ok(_mapper.Map<List<LineaVDTO>>(data));
        }

        [HttpGet("GetLineaPorId/{idLineas:int}")]
        public async Task<ActionResult<LineaVDTO>> GetLineaPorId(int idLineas)
        {
            LineaV data = await this._context.LineaVs.Where(l => l.IdLinea == idLineas).FirstOrDefaultAsync() ?? new LineaV();
            return Ok(_mapper.Map<LineaVDTO>(data));
        }

        //TODO: Revisar implentacion

        [HttpPost("AddEquipo")]
        public async Task<ActionResult<string>> AddEquipo(EquipoDTO equipo)
        {
            if (equipo.IdEquipo == 0)
            {
                try
                {
                    var result = await _context.EquipoEams
                    .Where(x => x.EcodEquiEam == equipo.EcodEquiEam && x.IdLinea == equipo.IdLinea)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                    if (result == null)
                    {
                        EquipoEam e = new EquipoEam();

                        e.IdLinea = equipo.IdLinea;
                        e.EcodEquiEam = equipo.EcodEquiEam;
                        e.EdescriEam = equipo.EdescriEam;
                        e.EestaEam = equipo.EestaEam;
                        e.EnombreEam = equipo.EnombreEam;

                        _context.EquipoEams.Add(e);
                        await _context.SaveChangesAsync();

                        return Ok("Registro Exitoso");
                    }
                    else
                    {
                        return BadRequest("Ya se registró este código de equipo.");
                    }

                }
                catch
                {
                    return BadRequest("Error, intente nuevamente");
                }
            }
            else
            {
                try
                {
                    EquipoEamDTO _eq = _mapper.Map<EquipoEamDTO>(equipo);
                    _eq.IdEquipo = equipo.IdEquipo;
                    _eq.IdLinea = equipo.IdLinea;
                    _eq.EcodEquiEam = equipo.EcodEquiEam;
                    _eq.EdescriEam = equipo.EdescriEam;
                    _eq.EestaEam = equipo.EestaEam;
                    _eq.EnombreEam = equipo.EnombreEam;

                    return await UpdateEquipo(_eq);
                    //return Ok("");

                }
                catch
                {
                    return BadRequest("Error, intente nuevamente");

                }
            }

        }

        [HttpPost("UpdateEquipo")]
        public async Task<string> UpdateEquipo(EquipoEamDTO equipo)
        {
            try
            {
                //No se porque pero asi funciona, no mover 
                //ya lo movi XD
                EquipoEamDTO _eq = _mapper.Map<EquipoEamDTO>(equipo);
                // EquipoEam eq = new EquipoEam();
                _eq = equipo;

                _context.Entry(_eq).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return "Registro Exitoso";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [HttpGet("GetMaestraPorFiltros")]
        public async Task<ActionResult<List<MasterDTO>>> GetMaestraPorFiltros([FromQuery] MaestraV maestra)
        {
                if (maestra.IdDivision != 0)
                {
                    List<Master> data = await this._context.Masters.Where(m => m.IdDivision == maestra.IdDivision).Include(m => m.IdLineaNavigation).ToListAsync();
                    return Ok(_mapper.Map<List<MasterDTO>>(data));
                }
                else if (maestra.IdCentro != 0)
                {
                    List<Master> data = await this._context.Masters.Where(m => m.IdCentro == maestra.IdCentro).Include(m => m.IdDivisionNavigation).ToListAsync();
                    return Ok(_mapper.Map<List<MasterDTO>>(data));
                }
                else if (maestra.IdEmpresa != 0)
                {
                    List<Master> data = await this._context.Masters.Where(m => m.IdEmpresa == maestra.IdEmpresa).Include(m => m.IdCentroNavigation).ToListAsync();
                    return Ok(_mapper.Map<List<MasterDTO>>(data));
                }
                else if (maestra.IdPais != 0)
                {
                    List<Master> data = await this._context.Masters.Where(m => m.IdPais == maestra.IdPais).Include(m => m.IdEmpresaNavigation).ToListAsync();
                    return Ok(_mapper.Map<List<MasterDTO>>(data));
                }
                return BadRequest();
        }

        [HttpGet("GetMaestraPorLinea/{idLinea:int}")]
        public async Task<ActionResult<int>> GetMaestraPorLinea(int idLinea)
        {

            return await this._context.Masters.Where(m => m.IdLinea == idLinea).Select(m => m.IdMaster).FirstOrDefaultAsync();

        }

        [HttpGet("GetEquiposEAMPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<EquipoEamDTO>>> GetEquiposEAMPorLinea(int idLinea)
        {
            List<EquipoEam> data = await this._context.EquipoEams.Where(e => e.IdLinea == idLinea && e.EestaEam).AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<List<EquipoEamDTO>>(data));
        }
    }
}