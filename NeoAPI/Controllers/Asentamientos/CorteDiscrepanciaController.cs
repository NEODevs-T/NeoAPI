using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using NeoAPI.DTOs.Asentamientos;
using AutoMapper;
//using System.ComponentModel;

namespace NeoAPI.Controllers.Asentamientos
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]



    public class CorteDiscrepanciaController : ControllerBase
    {
        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public CorteDiscrepanciaController(DbNeoIiContext _DbNeo, IMapper maper)
        {
            _context = _DbNeo;
            _mapper = maper;
        }


        //GETS

        //Obtener Asentamientos fuera de rango TODO:DTO
        [HttpGet("AsentamientoFueraRango/{turno}/{fecha:datetime}/{idfiltrolinea:int}")]
        public async Task<ActionResult<List<Asentum>>> GetAsentamientosFueraRango(string turno, DateTime fecha, int idfiltrolinea)
        {
            //retorna fuera de rango de un centro
            var result = await _context.Asenta
                .Include(r => r.IdRangoNavigation)
                .Include(r => r.IdRangoNavigation).ThenInclude(r => r.IdVariableNavigation).ThenInclude(r => r.IdUnidadNavigation)
                .Include(r => r.IdRangoNavigation).ThenInclude(r => r.IdVariableNavigation).ThenInclude(r => r.IdSeccionNavigation)
                .Include(r => r.IdRangoNavigation).ThenInclude(r => r.IdProductoNavigation)
                .Include(r => r.CorteDis)
                .Where(f => (f.Avalor > f.IdRangoNavigation.Rmax || f.Avalor < f.IdRangoNavigation.Rmin)
                    && f.IdInfoAseNavigation.IafechCrea.Date == fecha.Date
                    && f.AisActivo == true
                    && f.IdInfoAseNavigation.Iaturno == turno
                    && f.IdRangoNavigation.IdMasterNavigation.IdLinea == idfiltrolinea
                    && f.CorteDis.Count == 0)
                .AsNoTracking()
                .ToListAsync();

            var corteDiscDTO = _mapper.Map<List<AsentumDTO>>(result);

            return Ok(corteDiscDTO);
        }

        //Obtener Asentamientos fuera de rango con filtros  TODO:DTO
        [HttpGet("AsentamientoFueraRangoFiltros/{turno}/{fecha:datetime}/{idfiltrolinea:int}/{idClasiVar:int}/{idSeccion:int}/{idProducto:int}")]
        public async Task<ActionResult<List<Asentum>>> GetAsentamientosFueraRango2(string turno, DateTime fecha, int idfiltrolinea, int idClasiVar, int idSeccion, int idProducto)
        {
            //retorna fuera de rango de un centro
            var result = await _context.Asenta
                .Include(r => r.IdRangoNavigation)
                .Include(r => r.IdRangoNavigation).ThenInclude(r => r.IdVariableNavigation).ThenInclude(r => r.IdUnidadNavigation)
                .Include(r => r.IdRangoNavigation).ThenInclude(r => r.IdVariableNavigation).ThenInclude(r => r.IdSeccionNavigation)
                .Include(r => r.IdRangoNavigation).ThenInclude(r => r.IdProductoNavigation)
                .Include(r => r.CorteDis)
                .Where(f => (f.Avalor > f.IdRangoNavigation.Rmax || f.Avalor < f.IdRangoNavigation.Rmin)
                    && f.IdInfoAseNavigation.IafechCrea.Date == fecha.Date
                    && f.AisActivo == true
                    && f.IdInfoAseNavigation.Iaturno == turno
                    && f.IdRangoNavigation.IdMasterNavigation.IdLinea == idfiltrolinea
                    && f.IdRangoNavigation.IdProducto == idProducto
                    && f.IdRangoNavigation.IdVariableNavigation.IdSeccion == idSeccion
                    && f.IdRangoNavigation.IdVariableNavigation.IdClasiVar == idClasiVar
                    && f.CorteDis.Count == 0)
                .AsNoTracking()
                .ToListAsync();

            var corteDiscDTO = _mapper.Map<List<AsentumDTO>>(result);

            return Ok(corteDiscDTO);
        }


        //Valida si existe algun aentamiento fuera de rango
        [HttpGet("ValidarAsentamientoFueraRango")]
        public async Task<ActionResult<bool>> GetAnyAsentamientoFueraRango()
        {
            var result = await _context.Asenta
            .AsNoTracking()
            .Include(r => r.IdRangoNavigation)
            .Include(c => c.CorteDis)
            .Where(f => (f.Avalor > f.IdRangoNavigation.Rmax
            || f.Avalor < f.IdRangoNavigation.Rmin)
            && (f.IdInfoAseNavigation.IafechCrea.Date == DateTime.Now.Date && f.AisActivo == true))
            .AnyAsync();

            return Ok(result);
        }

        //Obtener Cortes de discrepancias del día de una linea sin filtros 
        [HttpGet("CortesDelDiaLinea/{turno}/{fecha:datetime}/{idfiltrolinea:int}")]
        public async Task<ActionResult<List<CorteDiscDTO>>> GetCortesTotales(string turno, DateTime fecha, int idfiltrolinea)
        {


            var corteDisc = await _context.CorteDis
            .Where(c => c.IdAsentaNavigation.IdInfoAseNavigation.IafechCrea.Date == fecha.Date
             && c.IdAsentaNavigation.IdInfoAseNavigation.Iaturno == turno
             && c.IdAsentaNavigation.IdRangoNavigation.IdMasterNavigation.IdLinea == idfiltrolinea)
            .Select(c => new
            {
                c.IdCorteDis,
                c.IdCategori,
                c.IdAsenta,
                c.CdaccCorr,
                c.CdisListo,
                c.CdisLibro,
                c.CdfechAcci,
                c.CdfechList,
                c.IdCategoriNavigation.Cnombre,
                c.IdAsentaNavigation.Avalor,
                c.IdAsentaNavigation.Aobserv,
                c.IdAsentaNavigation.IdRangoNavigation.IdRango,
                c.IdAsentaNavigation.IdRangoNavigation.RlimMax,
                c.IdAsentaNavigation.IdRangoNavigation.RlimMin,
                c.IdAsentaNavigation.IdRangoNavigation.Rmax,
                c.IdAsentaNavigation.IdRangoNavigation.Rmin,
                c.IdAsentaNavigation.IdRangoNavigation.Robj,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariable,
                c.IdAsentaNavigation.IdRangoNavigation.IdProducto,
                c.IdAsentaNavigation.IdRangoNavigation.IdProductoNavigation.Pnombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdUnidadNavigation.Unombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdSeccionNavigation.IdSeccion,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdSeccionNavigation.Snombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.Vnombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.Vdescri,
                c.IdAsentaNavigation.IdInfoAseNavigation.IdInfoAse,

            })
             .AsNoTracking()
             .ToListAsync();
            return Ok(corteDisc);
        }

        //Obtener Cortes de discrepancias del día con dto CortesVista
        [HttpGet("CortesDelDiaLineaFiltros/{turno}/{fecha:datetime}/{idfiltrolinea:int}/{idClasiVar:int}/{idSeccion:int}/{idProducto:int}")]
        public async Task<ActionResult<List<CortesVistaDTO>>> GetCortesTotalesFiltros(string turno, DateTime fecha, int idfiltrolinea, int idClasiVar, int idSeccion, int idProducto)
        {

            var corteDisc = await _context.CorteDis
            .Where(c => c.IdAsentaNavigation.IdInfoAseNavigation.IafechCrea.Date == fecha.Date
                && c.IdAsentaNavigation.IdInfoAseNavigation.Iaturno == turno
                && c.IdAsentaNavigation.IdRangoNavigation.IdMasterNavigation.IdLinea == idfiltrolinea
                && c.IdAsentaNavigation.IdRangoNavigation.IdProducto == idProducto
                && c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdSeccion == idSeccion
                && c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdClasiVar == idClasiVar)
            .Select(c => new
            {
                c.IdCorteDis,
                c.IdCategori,
                c.IdAsenta,
                c.CdaccCorr,
                c.CdisListo,
                c.CdisLibro,
                c.CdfechAcci,
                c.CdfechList,
                c.IdCategoriNavigation.Cnombre,
                c.IdAsentaNavigation.Avalor,
                c.IdAsentaNavigation.Aobserv,
                c.IdAsentaNavigation.IdRangoNavigation.IdRango,
                c.IdAsentaNavigation.IdRangoNavigation.RlimMax,
                c.IdAsentaNavigation.IdRangoNavigation.RlimMin,
                c.IdAsentaNavigation.IdRangoNavigation.Rmax,
                c.IdAsentaNavigation.IdRangoNavigation.Rmin,
                c.IdAsentaNavigation.IdRangoNavigation.Robj,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariable,
                c.IdAsentaNavigation.IdRangoNavigation.IdProducto,
                c.IdAsentaNavigation.IdRangoNavigation.IdProductoNavigation.Pnombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdUnidadNavigation.Unombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdSeccionNavigation.IdSeccion,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.IdSeccionNavigation.Snombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.Vnombre,
                c.IdAsentaNavigation.IdRangoNavigation.IdVariableNavigation.Vdescri,
                c.IdAsentaNavigation.IdInfoAseNavigation.IdInfoAse,

            })
             .AsNoTracking()
             .ToListAsync();
            return Ok(corteDisc);
        }


        //Obtener Categorias para corte de discrepancias
        [HttpGet("CategoriasCorte")]
        public async Task<ActionResult<List<CategoriaDTO>>> GetCategoriasCorte()
        {
            var result = await _context.Categoris
            .AsNoTracking()
            .ToListAsync();

            var categoriaDTO = _mapper.Map<List<CategoriaDTO>>(result);

            return Ok(categoriaDTO);
        }



        //POST

        //Insertar Categorias para el corte 
        [HttpPost("AddCategoria")]
        public async Task<ActionResult<string>> AddCategoria(CategoriaDTO categori)
        {
            if (categori.IdCategori == 0)
            {
                try
                {
                    bool existe = _context.Categoris.Any(c => c.Ccodigo == categori.Ccodigo);
                    // Si no existe el codigo, insertar la nueva categor�a
                    if (!existe)
                    {
                        var entity = _mapper.Map<Categori>(categori);
                        _context.Categoris.Add(entity);
                        await _context.SaveChangesAsync();
                        return Ok("Registro exitoso");
                    }
                    else
                    {
                        // Mostrar un mensaje de error o hacer otra acci�n
                        return BadRequest("Código ya registrado");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Error, intente nuevamente" + ex.Message);
                }
            }
            else
            {
                try
                {
                    var entity = _mapper.Map<Categori>(categori);
                    _context.Entry(categori).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok("Registro exitoso");

                }
                catch
                {
                    return BadRequest("Error, intente nuevamente");

                }
            }
        }

        //add corte por asentamiento individual

        [HttpPost("AddCorte")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddCorte(CorteDiscDTO cortedis)
        {
            try
            {
                var corte = await _context.CorteDis.FirstOrDefaultAsync(c => c.IdAsenta == cortedis.IdAsenta);
                if (corte == null)
                {
                    var entity = _mapper.Map<CorteDi>(cortedis);
                    _context.CorteDis.Add(entity);

                    //// Actualizar AsentumDTO
                    //var asentum = await _context.Asenta.FirstOrDefaultAsync(a => a.IdAsenta == cortedis.IdAsenta);
                    //if (asentum != null)
                    //{
                    //    _mapper.Map(cortedis.AsentumDTONavigation, asentum);
                    //    _context.Entry(asentum).State = EntityState.Modified;
                    //}

                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest(new { message = "Carte ya realizado" });
                }
                return Ok("Registro exitoso");
            }
            catch (Exception ex)
            {
                return Problem("Error, intente nuevamente" + ex.Message);
            }
        }
        //insertarcorte como lista validando si existe uno
        [HttpPost("AddListaCortes")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AddCorte(List<CorteDiscDTO> cortedis, string turno, DateTime fecha) //TODO agrgar id del master del la linea
        {
            try
            {

                var corte = await _context.CorteDis.FirstOrDefaultAsync(c => c.IdAsentaNavigation.IdInfoAseNavigation.IafechCrea.Date == fecha.Date && c.IdAsentaNavigation.IdInfoAseNavigation.Iaturno == turno); // Si no existe el codigo, insertar la nueva categor�a
                if (corte == null)
                {
                    var lista = _mapper.Map<List<CorteDi>>(cortedis);
                    _context.CorteDis.AddRange(lista);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest(new { message = "Carte ya realizado" });
                }
                return Ok("Registro exitoso");
            }
            catch (Exception ex)
            {
                return Problem("Error, intente nuevamente" + ex.Message);
            }
        }

        //PUTS

        //modifica un  objetos o inserta nuevo
        [HttpPut("UpdateCategoria")]
        public async Task<ActionResult<string>> UpdateCategoria(CategoriaDTO categoria)
        {
            var entity = _mapper.Map<Categori>(categoria);
            _context.Update(entity);// Update actualiza entidad y colecciones o inserta si no tiene id
            await _context.SaveChangesAsync();
            return Ok("Registro exitoso");
        }

        //modifica una lista de objetos o inserta nuevos
        [HttpPut("UpdateCorteAsentamientoLista")]
        public async Task<ActionResult<bool>> UpdateCorteLista(List<AsentumDTO> cortes)
        {
            var entity = _mapper.Map<List<Asentum>>(cortes);
            //_context.Entry(entity).State = EntityState.Modified;
            _context.UpdateRange(entity);// Update actualiza entidad y colecciones o inserta si no tiene id
            await _context.SaveChangesAsync();
            return Ok(true);
        }

        //Modifica un objeto
        [HttpPut("UpdateCorte")]
        public async Task<ActionResult<CorteDiscDTO>> UpdateCorte(CorteDiscDTO corte)
        {
            var entity = await _context.CorteDis.FindAsync(corte.IdCorteDis);
            _mapper.Map(corte, entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            var corteDTO = _mapper.Map<CorteDiscDTO>(entity);
            return Ok(corteDTO);
        }
    }
}


