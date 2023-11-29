using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;

namespace NeoAPI.Controllers.Maestra
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MaestraController : ControllerBase
    {


        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;

        public MaestraController(DbNeoIiContext _DbNeo, IMapper maper)
        {
            _context = _DbNeo;
            _mapper = maper;
        }

        [HttpGet("GetIdHorarios")]
        public System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> GetIdHorarios()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        //Obtener Categorias para corte de discrepancias
        [HttpGet("MaestrasxEmpresas")]
        public async Task<ActionResult<List<Master>>> GetMaestrasGeneral()
        {
            var result = await _context.Masters
                .Include(c => c.IdPaisNavigation)
                .Include(c => c.IdEmpresaNavigation)
                .Include(c => c.IdCentroNavigation)
                .Include(c => c.IdDivisionNavigation)
                .Include(c => c.IdLineaNavigation)
                .AsNoTracking()
                .Select(m => new Master
                {
                    IdMaster = m.IdMaster,
                    IdPaisNavigation = m.IdPaisNavigation,
                    IdEmpresaNavigation = m.IdEmpresaNavigation,
                    IdCentroNavigation = m.IdCentroNavigation,
                    IdDivisionNavigation = m.IdDivisionNavigation,
                    IdLineaNavigation = m.IdLineaNavigation,
                })
                .ToListAsync();


            return result;
        }
        //Obtener Categorias para corte de discrepancias
        [HttpGet("MaestraId")]//TODO: Crear DTOs para maestra
        public async Task<ActionResult<List<Master>>> GetMaestrasId(int idmaster)
        {
            var result = await _context.Masters
                .Include(c => c.IdPaisNavigation)
                .Include(c => c.IdEmpresaNavigation)
                .Include(c => c.IdCentroNavigation)
                .Include(c => c.IdDivisionNavigation)
                .Include(c => c.IdLineaNavigation)
                .Where(id => id.IdMaster.Equals(idmaster))
                .ToListAsync();


            return result;
        }
    }
}