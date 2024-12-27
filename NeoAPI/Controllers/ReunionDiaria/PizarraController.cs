using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.Maestra;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.ReunionDia;
using NeoAPI.Interface;


namespace NeoAPI.Controllers.Pizarra;

[ApiController]
[Route("api/[controller]")]
public class PizarraController : ControllerBase
{

    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;


    public PizarraController(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpPost("AddRegistros")]
    public async Task<ActionResult<bool>> InsertarRegistros(List<ReunionDTO> reunionDia)
    {
        List<Reunion> reunionDia1 = _mapper.Map<List<Reunion>>(reunionDia);
        foreach (var item in reunionDia1)
        {
            this._context.Reunions.Add(item);
        }
        return Ok(await _context.SaveChangesAsync() > 0);
    }


    [HttpGet("GetTrabajosPorCalendario/{pais}/{centro}/{division}/{reunion:int}")]
    public async Task<ActionResult<List<CalendarioTrabajoDTO>>> GetTrabajosCalendario(string pais, string centro, string division, int reunion)
    {

        if (division == "All")
        {
            var list = await _context.Reunions
                    .Include(x => x.IdResReuNavigation)
                    .Where(d => (d.Rdstatus == "Pendiente/Responsable" || d.Rdstatus == "Pendiente") && (d.IdMasterNavigation.IdPais == int.Parse(pais)) && (d.Rdcentro == centro) && (d.IdTipReu == reunion))
                    .AsNoTracking()
                    .ToListAsync();

            var result = list.Select(r => new CalendarioTrabajoDTO
            {
                IdReuDia = r.IdReuDia,
                RdcodEq = r.RdcodEq,
                Rddisc = r.Rddisc,
                Rdodt = r.Rdodt,
                Rdtiempo = r.Rdtiempo.ToString(),
                RdfecTra = r.RdfecTra //?? DateTime.Now
            });

            return Ok(result);
            // return Ok(_mapper.Map<List<ReunionDTO>>(list));
        }
        else
        {
            var list = await _context.Reunions
                    .Include(x => x.IdResReuNavigation)
                    .Where(d => (d.Rdstatus == "Pendiente/Responsable" || d.Rdstatus == "Pendiente") && (d.IdMasterNavigation.IdPais == int.Parse(pais)) && (d.Rdcentro == centro) && (d.Rddiv == division) && (d.IdTipReu == reunion))
                    .AsNoTracking()
                    .ToListAsync();


            var result = list.Select(r => new CalendarioTrabajoDTO
            {
                IdReuDia = r.IdReuDia,
                RdcodEq = r.RdcodEq,
                Rddisc = r.Rddisc,
                Rdodt = r.Rdodt,
                Rdtiempo = r.Rdtiempo.ToString(),
                RdfecTra = r.RdfecTra //?? DateTime.Now
            });

            // return Ok(_mapper.Map<List<ReunionDTO>>(list));
            return Ok(result);
        }
    }

    [HttpGet("GetPendientes/{idcentro}/{iddiv}/{f1:DateTime}/{f2:DateTime}/{tipo}/{estado}/{reunion:int}")]
    public async Task<ActionResult<List<ReunionDTO>>> GetPendientes(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado, int reunion)
    {


        IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        List<Reunion> reudiatablas = new List<Reunion>();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);


        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;
        int reunionDiaria = 1;
        int reunionTurno = 2;

        reudiatablas = new List<Reunion>();



        if (tipo == "1")
        {

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {

                reudiatablas = await _context.Reunions
                //.Where(a =>  (a.Div == centro & a.Division==div ) | (a.Div == centro & a.Division == div & (a.Fecha>= f1 & a.Fecha <= f2)))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div && (a.Rdstatus != "Listo" && a.Rdstatus != "Cerrado") && (a.RdfecReu >= f1.AddDays(-3) && a.RdfecReu <= f2.AddDays(+1))))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .ToListAsync();
            }
            else
            {

                reudiatablas = await _context.Reunions
                //.Where(a =>  (a.Div == centro & a.Division==div ) | (a.Div == centro & a.Division == div & (a.Fecha>= f1 & a.Fecha <= f2)))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div && (a.Rdstatus != "Listo" && a.Rdstatus != "Cerrado") && (a.RdfecReu >= f1.Date && a.RdfecReu <= f2.AddDays(+1))))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .ToListAsync();
            }
        }

        //tipo 0 Pendientes para fecha reunion
        else if (tipo == "0")
        {


            if (estado == "Total Pendiente")
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(500)
                .ToListAsync();
            }

            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {

                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus == estado))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(350)
                .ToListAsync();
            }
        }
        //Fecha de trabajo
        else if (tipo == "2")
        {
            if (estado == "Total Pendiente")
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(500)
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus == estado))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(50)
                .ToListAsync();
            }

        }

        return Ok(_mapper.Map<List<ReunionDTO>>(reudiatablas));

    }

    [HttpGet("GetByODT/{ODT}/{idcentro}/{iddiv}/{reunion:int}")]
    public async Task<ActionResult<List<ReunionDTO>>> GetByODT(string ODT, string idcentro, string iddiv)
    {
        IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
        //Consultar nombre del centro y division  para insertarlos
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);
        List<Reunion> reudiatablas = new List<Reunion>();



        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<Reunion>();

        reudiatablas = await _context.Reunions
        .Where(a => a.Rdodt.Contains(ODT) && (a.Rdcentro == centrodiv.Cnom && a.Rddiv == centrodiv.Dnombre && a.IdTipReu == 1))
        .Include(b => b.IdksfNavigation)
        .Include(b => b.IdResReuNavigation)
        .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
        .OrderByDescending(b => b.RdfecReu)
        .ToListAsync();

        return Ok(_mapper.Map<List<ReunionDTO>>(reudiatablas));
    }

    [HttpGet("GetPendientesTurno/{idcentro}/{iddiv}")]
    public async Task<ActionResult<List<ReunionDTO>>> GetPendientesTurno(string idcentro, string iddiv)
    {
        IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);

        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;
        int reunionTurno = 2;

        List<Reunion> disc = await _context.Reunions
            .Include(b => b.IdksfNavigation)
            .Include(b => b.IdResReuNavigation)
            .Where(h => h.Rdcentro == centro && h.Rddiv == div && h.IdTipReu == reunionTurno && h.Rdstatus == "Pendiente" && h.RdfecReu.Date < DateTime.Now.Date)
            .ToListAsync();
        if (disc == null)
            throw new Exception("not found!");
        return Ok(_mapper.Map<List<ReunionDTO>>(disc));

    }
    [HttpGet("GetPendientesQuincenal/{idcentro}/{iddiv}")]
    public async Task<ActionResult<List<ReunionDTO>>> GetPendientesQuincenal(string idcentro, string iddiv)
    {
        IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);
        List<CambFec> filt = await getDiv.GetPendientesQuincenal(centrodiv);
        List<Reunion> disc = new List<Reunion>();

        foreach(var iten in filt)
        {
            List<Reunion> discs = await _context.Reunions
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .Where(h => h.IdReuDia == iten.IdReuDia)
                .ToListAsync();
            disc.AddRange(discs);
        }

        if (disc == null)
            throw new Exception("not found!");
        return Ok(_mapper.Map<List<ReunionDTO>>(disc));

    }
    [HttpGet("GetPendientesQuincenal2/{idcentro}/{iddiv}")]
    public async Task<ActionResult<List<CambiReuVDTO>>> GetPendientesQuincenal2(string idcentro, string iddiv)
    {
        IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);
        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;
        int reunionDiaria = 1;

List<CambiReuV> disc = await _context.CambiReuVs
    .Where(h => h.Centro == centro && h.Division == div && h.TipoReunion == reunionDiaria && h.FechaTrabajo.Date >= DateTime.Now.AddMonths(-3) && (h.Estado == "Pendiente" || h.Estado == "Pendiente/Responsable" || h.Estado == "Listo"))
    .ToListAsync();


        if (disc == null)
            throw new Exception("not found!");
        return Ok(_mapper.Map<List<CambiReuVDTO>>(disc));

    }

    //historicos
    [HttpGet("GetHistoricos/{idcentro}/{iddiv}/{f1:DateTime}/{f2:DateTime}/{tipo}/{estado}/{reunion:int}")]
    public async Task<ActionResult<List<ReunionDTO>>> GetHistoricos(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado, int reunion)
    {


        //Consultar nombre del centro y division  para insertarlos
        IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);
        List<Reunion> reudiatablas;



        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<Reunion>();

        if (tipo == "2")
        {


            if (estado == "Total Pendiente")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div && (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecTra >= f1 & a.RdfecTra <= f2.AddDays(+1))))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync(); 

            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.RdfecTra >= f1 && a.RdfecTra <= f2.AddDays(+1)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.RdfecTra >= f1 && a.RdfecTra <= f2.AddDays(+1)) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2.AddDays(+1)) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }

            else
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) & (a.Rdstatus == estado) && (a.RdfecTra >= f1 & a.RdfecTra <= f2.AddDays(+1)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
        }
        else
        {
            if (estado == "Total Pendiente")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecReu >= f1 & a.RdfecReu <= f2.AddDays(+1))))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.RdfecReu >= f1 && a.RdfecReu <= f2.AddDays(+1)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2.AddDays(+1)) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2.AddDays(+1)) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "VencidosDiaria")
            {
                reudiatablas = await _context.Reunions
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == 1 && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2.AddDays(+1)) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _context.Reunions
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.IdTipReu == reunion && a.Rddiv == div) & (a.Rdstatus == estado) && (a.RdfecReu >= f1 & a.RdfecReu <= f2.AddDays(+1)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
        }
        return (_mapper.Map<List<ReunionDTO>>(reudiatablas));
    }

    // Update Discrepancia
    [HttpPut("UpdateDiscrepancia/{id:int}")]
    public async Task<ActionResult<bool>> UpdateDiscrepancia(ReunionDTO d, int id)
    {
        try
        {
            string div = "", centro = "";

            if (d.Rdcentro is not null)
            {
                // Consultar nombre del centro y división para retornar el id en pendientes
                IReunionDiaLogic getDiv = new ReunionDiaLogic(_context);
                CentroDivisionDTO centrodiv = await getDiv.GetCentroDivi(d.Rdcentro, d.Rddiv, 1);

                if (centrodiv == null)
                {
                    return NotFound("Centro o División no encontrados.");
                }

                centro = centrodiv.IdCentro.ToString();
                div = centrodiv.IdDivision.ToString();
            }

            Reunion bdDiscrep = await _context.Reunions
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .FirstOrDefaultAsync(sh => sh.IdReuDia == id);

            if (bdDiscrep == null)
            {
                return NotFound("Discrepancia no encontrada.");
            }

            bdDiscrep.Rdarea = d.Rdarea;
            bdDiscrep.Rddiv = d.Rddiv;
            bdDiscrep.RdcodDis = d.RdcodDis;
            bdDiscrep.RdcodEq = d.RdcodEq;
            bdDiscrep.Rddisc = d.Rddisc;
            bdDiscrep.Rdcentro = d.Rdcentro;
            bdDiscrep.RdfecReu = d.RdfecReu;
            bdDiscrep.RdfecTra = d.RdfecTra;
            bdDiscrep.Idksf = d.Idksf;
            bdDiscrep.RdplanAcc = d.RdplanAcc;
            bdDiscrep.Rdodt = d.Rdodt;
            bdDiscrep.IdResReu = d.IdResReu;
            bdDiscrep.Rdstatus = d.Rdstatus;
            bdDiscrep.Rdtiempo = d.Rdtiempo;
            bdDiscrep.IdMaster = d.IdMaster;
            bdDiscrep.IdEmpresa = d.IdEmpresa;
            bdDiscrep.IdOrigen = 0;

            /* 
            Campos faltantes
            IdCausaCal 
            IdTipReu 
            */

            _context.Entry(bdDiscrep).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(true);
        }
        catch (Exception ex)
        {
            // Manejo de la excepción
            return StatusCode(500, $"Ocurrió un error en el servidor: {ex.Message}");
        }
    }

    [HttpPut("UpdateDiscrepancia2/{id:int}")]
    public async Task<ActionResult<bool>> UpdateDiscrepancia2(ReunionDTO d, int id)
    {
        if (d?.Rdcentro == null)
        {
            return BadRequest("El centro no es válido o no se proporcionó.");
        }
        // Cargar la entidad desde la base de datos
        var entity = await _context.Reunions.FirstOrDefaultAsync(sh => sh.IdReuDia == id);
        if (entity == null)
        {
            return NotFound("La entidad no fue encontrada.");
        }

        // Mapeo de los cambios de d a la entidad cargada
        _mapper.Map(d, entity);

        // Guardar los cambios sin usar Update
        bool isUpdated = await _context.SaveChangesAsync() > 0;

        return isUpdated ? Ok(true) : StatusCode(500, "No se pudo actualizar la discrepancia.");

    }
    //obtener discrepancia a editar
    [HttpGet("GetDiscrepantacia/{id:int}")]
    public async Task<ActionResult<ReunionDTO>> GetDiscrepantacia(int id)
    {
        var disc = await _context.Reunions
            .Include(b => b.IdksfNavigation)
            .Include(b => b.IdResReuNavigation)
            .FirstOrDefaultAsync(h => h.IdReuDia == id);
        if (disc == null)
            throw new Exception("not found!");
        return Ok(_mapper.Map<ReunionDTO>(disc));
    }

    [HttpPost("AddDiscrepancia")]
    public async Task<ActionResult<int>> InsertDiscrepancia(ReunionDTO discre)
    {
        Reunion data = _mapper.Map<Reunion>(discre);
        _context.Reunions.Add(data);
        await _context.SaveChangesAsync();
        return Ok(data.IdReuDia);
    }
}
