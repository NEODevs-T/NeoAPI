using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models.Neo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NeoAPI.DTOs.ReunionDiaria;
using Microsoft.Data.SqlClient;
using NeoAPI.Logic.GetCentroDiv;

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
                if (cen == "All")
                {
                    if (int.TryParse(cent.Substring(3), out idempresa))
                    {
                        centro = await _context.CentrosVs
                            .Where(c => c.Estado == true && c.IdEmpresa == idempresa)
                            .ToListAsync();
                    }
                    else
                    {
                        return BadRequest("El formato del parámetro 'cent' es incorrecto. No se pudo extraer el ID de la empresa.");
                    }
                }
            }
            else
            {
                if (int.TryParse(cent, out int centroid))
                {
                    centro = await _context.CentrosVs
                        .Where(c => c.IdCentro == centroid)
                        .ToListAsync();
                }
                else
                {
                    return BadRequest("El formato del parámetro 'cent' es incorrecto.");
                }
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
        [HttpGet("GetEquipos/{cent}")]
        public async Task<ActionResult<List<EquipoEamDTO>>> EquiposEAM(string cent)
        {
            List<EquipoEam> listaEquipo = new List<EquipoEam>();

            string cen = "";
            int idempresa = 0;

            if (cent.Length > 3)
            {
                cen = cent.Substring(0, 3);
                if (cen == "All")
                {
                    if (int.TryParse(cent.Substring(3), out idempresa))
                    {
                        listaEquipo = await _context.EquipoEams
                            .Where(c => c.EestaEam == true && c.IdLineaNavigation.Master.IdEmpresa == idempresa)
                            .ToListAsync();
                    }
                    else
                    {
                        return BadRequest("El formato del parámetro 'cent' es incorrecto. No se pudo extraer el ID de la empresa.");
                    }
                }
            }
            else
            {
                if (int.TryParse(cent, out int equipoid))
                {
                    listaEquipo = await _context.EquipoEams
                        .Where(c => c.IdEquipo == equipoid)
                        .ToListAsync();
                }
                else
                {
                    return BadRequest("El formato del parámetro 'cent' es incorrecto.");
                }
            }

            return Ok(_mapper.Map<List<EquipoEamDTO>>(listaEquipo));
        }

        //TODO: Sujeta a revision
        [HttpGet("GetEquiposPorLinea/{linea}")]
        public async Task<ActionResult<List<EquipoEamDTO>>> EquiposEAMxLinea(string linea)
        {

            int idlinea = int.Parse(linea);



            var result = await _context.EquipoEams
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
                        e.Efecha = equipo.Efecha;


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

                    return Ok(await UpdateEquipo(_eq));
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

        // Metodos para la reunion  

        [HttpGet("GetBdDiv/{cent}")]
        public async Task<ActionResult<List<DivisionesVDTO>>> GetDivisionPorCentro(string cent)
        {
            List<DivisionesV> divisiones = new List<DivisionesV> { };
            string cen = "";
            int idcentro = 0;

            if (cent.Length > 3)
            {
                cen = cent.Substring(0, 3);
                if (cen == "All")
                {
                    if (int.TryParse(cent.Substring(3), out idcentro))
                    {
                        divisiones = await _context.DivisionesVs
                            .Where(c => c.IdCentro == idcentro)
                            .ToListAsync();
                    }
                    else
                    {
                        return BadRequest("El formato del parámetro 'cent' es incorrecto. No se pudo extraer el ID de la empresa.");
                    }
                }
            }
            else
            {
                if (int.TryParse(cent, out int divisionid))
                {
                    divisiones = await _context.DivisionesVs
                        .Where(c => c.IdDivision == divisionid)
                        .ToListAsync();
                }
                else
                {
                    return BadRequest("El formato del parámetro 'cent' es incorrecto.");
                }
            }

            return Ok(_mapper.Map<List<DivisionesVDTO>>(divisiones));
        }

        [HttpGet("GetEquiposPorCentro/{cent}")]
        public async Task<ActionResult<List<EquipoEamDTO>>> GetEquiposEAM(string cent)
        {
            List<EquipoEam> result = new List<EquipoEam>();
            string cen = "";
            int idempresa = 0;

            if (cent.Length > 3)
            {
                cen = cent.Substring(0, 3);
                if (cen == "All")
                {
                    if (int.TryParse(cent.Substring(3), out idempresa))
                    {
                        result = await _context.EquipoEams
                            .Where(c => c.EestaEam == true && c.IdLineaNavigation.Master.IdEmpresa == idempresa)
                            .ToListAsync();
                    }
                    else
                    {
                        return BadRequest("El formato del parámetro 'cent' es incorrecto. No se pudo extraer el ID de la empresa.");
                    }
                }
            }
            else
            {
                if (int.TryParse(cent, out int centroid))
                {
                    result = await _context.EquipoEams
                        .Where(c => c.IdLineaNavigation.Master.IdCentro == centroid)
                        .ToListAsync();
                }
                else
                {
                    return BadRequest("El formato del parámetro 'cent' es incorrecto.");
                }

            }

            return _mapper.Map<List<EquipoEamDTO>>(result);
        }


        [HttpGet("GetEquiposEAMPorLinea/{Centro}")]
        public async Task<ActionResult<List<MaestraVDTO>>> EquiposLineaEAM(string Centro)
        {
            List<MaestraV> data = new List<MaestraV> { };


            data = await _context.MaestraVs
            .Where(x => x.Centro == Centro)
            .Where(x => x.IdEmpresa == x.IdEmpresa)
            .ToListAsync();

            return Ok(_mapper.Map<List<MaestraVDTO>>(data));
        }



        // linea


        [HttpGet("GetHistoricos/{centro}/{division}/{tipo:int}")]
        public async Task<ActionResult<CentroDivisionDTO>> GetCentroDiv(string centro, string division, int tipo)
        {
            CentroDivisionDTO CD = new CentroDivisionDTO();
            ReuClass ReuBuild = new ReuClass(_context);

            if (tipo == 0)
            {
                // Validar si 'division' puede convertirse a un entero
                if (!int.TryParse(division, out int divisionId))
                {
                    return BadRequest("El valor de la división no es un número válido.");
                }

                // Ejecutar la consulta para el tipo 0
                var centrodiscrepancia = await _context.Masters
                    .Include(c => c.IdCentroNavigation)
                    .Include(d => d.IdDivisionNavigation)
                    .Where(d => d.IdDivision == divisionId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                // Verificar si la consulta devolvió resultados
                if (centrodiscrepancia == null)
                {
                    return NotFound("No se encontró el centro o división especificados.");
                }

                // Verificar si las propiedades de navegación también son válidas
                if (centrodiscrepancia.IdCentroNavigation == null || centrodiscrepancia.IdDivisionNavigation == null)
                {
                    return NotFound("Datos incompletos en las propiedades de navegación (Centro o División no encontrados).");
                }

                // Construir el DTO
                CD = ReuBuild.BuildCentroDivisionDTO(centrodiscrepancia);
            }
            else if (tipo == 1)
            {
                // Ejecutar la consulta para el tipo 1
                var centrodiscrepancia = await _context.Masters
                    .Include(c => c.IdCentroNavigation)
                    .Include(d => d.IdDivisionNavigation)
                    .Where(d => d.IdDivisionNavigation.Dnombre == division && d.IdCentroNavigation.Cnom == centro)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                // Verificar si la consulta devolvió resultados
                if (centrodiscrepancia == null)
                {
                    return NotFound("No se encontró el centro o división especificados.");
                }

                // Verificar si las propiedades de navegación también son válidas
                if (centrodiscrepancia.IdCentroNavigation == null || centrodiscrepancia.IdDivisionNavigation == null)
                {
                    return NotFound("Datos incompletos en las propiedades de navegación (Centro o División no encontrados).");
                }

                // Construir el DTO
                CD = ReuBuild.BuildCentroDivisionDTO(centrodiscrepancia);
            }

            return Ok(CD);
        }

        [HttpGet("GetCentroDivi/{centro}/{division}/{tipo:int}")]
        public async Task<ActionResult<CentroDivisionDTO>> GetCentroDivi(string centro, string division, int tipo)
        {
            CentroDivisionDTO CD = new CentroDivisionDTO();
            Master? centrodiscrepancia = new Master();

            if (tipo == 0)
            {
                int divisionInt;
                if (!int.TryParse(division, out divisionInt))
                {
                    throw new FormatException("El valor de 'division' no es un número válido.");
                }

                centrodiscrepancia = await _context.Masters
                    .Include(c => c.IdCentroNavigation)
                    .Include(d => d.IdDivisionNavigation)  // Asegúrate de incluir también IdDivisionNavigation
                    .Where(d => d.IdDivision == divisionInt)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (centrodiscrepancia == null)
                {
                    throw new NullReferenceException("No se encontró ninguna coincidencia para el centro o división proporcionados.");
                }

                // Verificar que las propiedades de navegación no sean nulas
                if (centrodiscrepancia.IdCentroNavigation == null)
                {
                    throw new NullReferenceException("La propiedad 'IdCentroNavigation' es nula.");
                }
                if (centrodiscrepancia.IdDivisionNavigation == null)
                {
                    throw new NullReferenceException("La propiedad 'IdDivisionNavigation' es nula.");
                }

                CD.IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro;
                CD.IdDivision = centrodiscrepancia.IdDivision;
                CD.Cnom = centrodiscrepancia.IdCentroNavigation.Cnom;
                CD.Dnombre = centrodiscrepancia.IdDivisionNavigation.Dnombre;
            }
            else if (tipo == 1)
            {
                centrodiscrepancia = await _context.Masters
                    .Include(c => c.IdCentroNavigation)
                    .Include(d => d.IdDivisionNavigation)  // Asegúrate de incluir también IdDivisionNavigation
                    .Where(d => d.IdDivisionNavigation.Dnombre == division && d.IdCentroNavigation.Cnom == centro)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (centrodiscrepancia == null)
                {
                    throw new NullReferenceException("No se encontró ninguna coincidencia para el centro o división proporcionados.");
                }

                // Verificar que las propiedades de navegación no sean nulas
                if (centrodiscrepancia.IdCentroNavigation == null)
                {
                    throw new NullReferenceException("La propiedad 'IdCentroNavigation' es nula.");
                }
                if (centrodiscrepancia.IdDivisionNavigation == null)
                {
                    throw new NullReferenceException("La propiedad 'IdDivisionNavigation' es nula.");
                }

                CD.IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro;
                CD.IdDivision = centrodiscrepancia.IdDivision;
                CD.Cnom = centrodiscrepancia.IdCentroNavigation.Cnom;
                CD.Dnombre = centrodiscrepancia.IdDivisionNavigation.Dnombre;
            }


            return Ok(CD);
        }
    }

}
