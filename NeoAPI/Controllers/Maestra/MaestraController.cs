﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models.Neo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NeoAPI.DTOs.Maestra;

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