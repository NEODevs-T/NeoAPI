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

        //Obtener Asentamientos fuera de rango 
        [HttpGet("AsentamientoFueraRango/{turno}/{fecha:datetime}/{idfiltroarea:int}")]
        public async Task<ActionResult<List<Asentum>>> GetAsentamientosFueraRango(string turno, DateTime fecha, int idfiltroarea)
        {
            //retorna fuera de rango de un centro
            var result = await _context.Asenta
            .AsNoTracking()
            .Include(r => r.IdRangoNavigation)
            .Include(r => r.IdRangoNavigation.IdVariableNavigation)
            .Include(r => r.IdRangoNavigation.IdVariableNavigation.IdUnidadNavigation)
            .Include(r => r.IdRangoNavigation.IdProductoNavigation)
            .Where(f => (f.Avalor > f.IdRangoNavigation.Rmax || f.Avalor < f.IdRangoNavigation.Rmin)
            && f.IdInfoAseNavigation.IafechCrea.Date == fecha.Date
            && f.AisActivo == true
            && f.IdInfoAseNavigation.Iaturno == turno
            && f.IdRangoNavigation.IdMasterNavigation.IdCentro == idfiltroarea)
            .ToListAsync();

            return Ok(result);
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


        //Obtener Categorias para corte de discrepancias
        [HttpGet("CategoriasCorte")]
        public async Task<ActionResult<List<CategoriaDTO>>> GetCategoriasCorte()
        {
            var result = await _context.Categoris
            .AsNoTracking()
            .ToListAsync();

            return Ok(result);
        }

        //Obtener Categorias para corte de discrepancias
        [HttpGet("CortesTotales")]
        public async Task<ActionResult<List<CorteDi>>> GetCortesTotales()
        {
            var result = await _context.CorteDis
            .AsNoTracking()
            .Select(c => new CorteDi
            {
                IdCorteDis = c.IdCorteDis,
                CdaccCorr = c.CdaccCorr,
                IdCategoriNavigation = c.IdCategoriNavigation
            })
            .ToListAsync();

            return Ok(result);
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
                        return BadRequest("C�digo ya registrado");
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


        //insertarcorte validando si existe uno
        [HttpPost("AddCorte")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
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
        [HttpPut("UpdateCorteLista")]
        public async Task<ActionResult<string>> UpdateCorteLista(List<CorteDiscDTO> corte)
        {
            var entity = _mapper.Map<CorteDi>(corte);
            //_context.Entry(entity).State = EntityState.Modified;
            _context.UpdateRange(entity);// Update actualiza entidad y colecciones o inserta si no tiene id
            await _context.SaveChangesAsync();
            return Ok("Registro exitoso");
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


