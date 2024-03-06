using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models.Neo;
using NeoAPI.ModelsViews;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NeoAPI.Models.NeoVieja;


namespace NeoAPI.Controllers.Maestras
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MaestraController : ControllerBase
    {


        private readonly DbNeoIiContext _context;
        private readonly IMapper _mapper;
        private readonly ViewsContext _views;
        private readonly NeoViejaContext _neoVieja;

        public MaestraController(DbNeoIiContext DbNeo, IMapper maper,ViewsContext views, NeoViejaContext neoVieja)
        {
            _context = DbNeo;
            _mapper = maper;
            _views = views;
            _neoVieja = neoVieja;
        }

        [HttpGet("GetPaises")]
        public async Task<ActionResult<List<Pai>>> GetPaises()
        {
            try{
                return await this._context.Pais.Where(p => p.Pestado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }
        [HttpGet("GetEmpresas/{idPais:int}")]
        public async Task<ActionResult<List<EmpresasV>>> GetEmpresas(int idPais)
        {
            try{
                return await this._views.EmpresasVs.Where(e => e.IdPais == idPais && e.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetCentros/{idEmpresa:int}")]
        public async Task<ActionResult<List<CentrosV>>> GetCentros(int idEmpresa)
        {
            try{
                return await this._views.CentrosVs.Where(c => c.IdEmpresa == idEmpresa && c.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetDivisiones/{idCentro:int}")]
        public async Task<ActionResult<List<DivisionesV>>> GetDivisiones(int idCentro)
        {
            try{
                return await this._views.DivisionesVs.Where(v => v.IdCentro == idCentro && v.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }


        [HttpGet("GetLineas/{idDivision:int}")]
        public async Task<ActionResult<List<LineaV>>> GetLineas(int idDivision)
        {
            try{
                return await this._views.LineaVs.Where(l => l.IdDivision == idDivision && l.Estado == true).ToListAsync();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetLineaPorId/{idLineas:int}")]
        public async Task<ActionResult<LineaV>> GetLineaPorId(int idLineas)
        {
            try{
                return await this._views.LineaVs.Where(l => l.IdLinea == idLineas).FirstOrDefaultAsync() ?? new LineaV();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetMaestraPorFiltros")]
        public async Task<ActionResult<List<Master>>> GetMaestraPorFiltros( [FromQuery] MaestraDTO maestra)
        {
            try{
                if(maestra.idDivision != 0){
                    return await this._context.Masters.Where(m => m.IdDivision == maestra.idDivision).Include(m => m.IdLineaNavigation).ToListAsync();
                }else if(maestra.idCentro != 0){
                    return await this._context.Masters.Where(m => m.IdCentro == maestra.idCentro).Include(m => m.IdDivisionNavigation).ToListAsync();
                }else if(maestra.idEmpresa != 0){
                    return await this._context.Masters.Where(m => m.IdEmpresa == maestra.idEmpresa).Include(m => m.IdCentroNavigation).ToListAsync();
                }else if(maestra.idPais != 0){
                    return await this._context.Masters.Where(m => m.IdPais == maestra.idPais).Include(m => m.IdEmpresaNavigation).ToListAsync();
                }
                return BadRequest();
            }catch{
                return NotFound();
            }
        }

        [HttpGet("GetMaestraPorLinea/{idLinea:int}")]
        public async Task<ActionResult<int>> GetMaestraPorLinea(int idLinea)
        {
            try{
                return await this._context.Masters.Where(m => m.IdLinea == idLinea).Select(m => m.IdMaster).FirstOrDefaultAsync();
            }catch{
                return NotFound();
            }
        }
        
        [HttpGet("GetEquiposEAMPorLinea/{idLinea:int}")]
        public async Task<ActionResult<List<EquipoEamViejo>>> GetEquiposEAMPorLinea(int idLinea)
        {
            try{
                return await this._neoVieja.EquipoEams.Where(e=>e.IdLinea==idLinea && e.EestaEam).AsNoTracking().ToListAsync();
            }catch{
                return NotFound();
            }
        }
    }
}