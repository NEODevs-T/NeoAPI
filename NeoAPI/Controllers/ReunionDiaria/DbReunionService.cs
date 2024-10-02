using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;
using NeoAPI.DTOs;
using System.Runtime.ConstrainedExecution;
using NeoAPI.Models.Neo;
using AutoMapper;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.DTOs.Maestra;
using NeoAPI.DTOs.LibroNovedades;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using NeoAPI.Controllers.GetCentroDiv;



namespace ReunionWeb.ServicesController;
[Route("api/[controller]")]
[ApiController]
public class DbReunionServiceController: ControllerBase
{

    public List<Centro> centros { get; set; } = new List<Centro> { };
    public List<Linea> lineas { get; set; } = new List<Linea> { };
    public List<Empresa> empresas { get; set; } = new List<Empresa> { };
    public List<Pai> paiss { get; set; } = new List<Pai> { };
    public List<Division> divs { get; set; } = new List<Division> { };
    public List<Ksf> ksfss { get; set; } = new List<Ksf>();
    public List<RespoReu> resporeus { get; set; } = new List<RespoReu>();
    public List<ReuDium> reunionditablas { get; set; } = new List<ReuDium>();
    public List<ReuDium> reudiatablas { get; set; } = new List<ReuDium>();
    public List<Division> divisionss { get; set; } = new List<Division>();
    public List<AsistenReu> asistenreus { get; set; } = new List<AsistenReu>();
    public List<CargoReu> cargoreuss { get; set; } = new List<CargoReu>();
    public List<CambStat> cambiostatus { get; set; } = new List<CambStat>();
    public List<CambFec> cambiofecha { get; set; } = new List<CambFec>();
    public List<CalendarioTrabajoDTO> calentrabajo { get; set; } = new List<CalendarioTrabajoDTO>();


    private readonly DbNeoIiContext _neocontext;

    private readonly IMapper _mapper;
    private readonly DbNeoIiContext _neoVieja;

    public DbReunionServiceController(DbNeoIiContext DbNeo, IMapper maper, DbNeoIiContext neoVieja)
    {
        _neocontext = DbNeo;
        _mapper = maper;
        _neoVieja = neoVieja;
    }




    // [HttpGet("GetByODT/{ODT}/{idcentro}/{iddiv}")]
    // public async Task<ActionResult<List<ReuDiumDTO>>> GetByODT(string ODT, string idcentro, string iddiv)
    // {
    //     OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);
    //     //Consultar nombre del centro y division  para insertarlos
    //     CentroDivisionDTO centrodiv = new CentroDivisionDTO();
    //     centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);


    //     string centro = centrodiv.Cnom;
    //     string div = centrodiv.Dnombre;

    //     reudiatablas = new List<ReuDium>();

    //     reudiatablas = await _neocontext.ReuDia
    //     .Where(a => a.Rdodt.Contains(ODT) && (a.Rdcentro == centrodiv.Cnom && a.Rddiv == centrodiv.Dnombre))
    //     .Include(b => b.IdksfNavigation)
    //     .Include(b => b.IdResReuNavigation)
    //     .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
    //     .OrderByDescending(b => b.RdfecReu)
    //     .ToListAsync();

    //     return Ok(_mapper.Map<List<ReuDiumDTO>>(reudiatablas));
    // }

    [HttpGet("GetByODT/{ODT}/{idcentro}/{iddiv}")]
public async Task<ActionResult<List<ReuDiumDTO>>> GetByODT(string ODT, string idcentro, string iddiv)
{
    try
    {
        OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);

        // Consultar nombre del centro y división para insertarlos
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);

        if (centrodiv == null)
        {
            return BadRequest("Centro o división no encontrados.");
        }

        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<ReuDium>();

        reudiatablas = await _neocontext.ReuDia
            .Where(a => a.Rdodt.Contains(ODT) && (a.Rdcentro == centrodiv.Cnom && a.Rddiv == centrodiv.Dnombre))
            .Include(b => b.IdksfNavigation)
            .Include(b => b.IdResReuNavigation)
            .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
            .OrderByDescending(b => b.RdfecReu)
            .ToListAsync();

        if (reudiatablas == null || !reudiatablas.Any())
        {
            return NotFound("No se encontraron datos con los parámetros proporcionados.");
        }

        return Ok(_mapper.Map<List<ReuDiumDTO>>(reudiatablas));
    }
    catch (FormatException ex)
    {
        // Error al convertir formatos de datos
        return BadRequest($"Error en el formato de datos: {ex.Message}");
    }
    catch (ArgumentNullException ex)
    {
        // Error si algún argumento es nulo
        return BadRequest($"Argumento nulo detectado: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        // Error si la operación no es válida (por ejemplo, cuando `FirstOrDefaultAsync` no encuentra nada)
        return BadRequest($"Operación inválida: {ex.Message}");
    }
    catch (DbUpdateException ex)
    {
        // Error relacionado con la base de datos
        return StatusCode(500, $"Error en la base de datos: {ex.Message}");
    }
    catch (Exception ex)
    {
        // Cualquier otro tipo de error general
        return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
    }
}


    // //obtener discrepancias para pendientes y reunion 
    // [HttpGet("GetPendientes/{idcentro:string}/{iddiv:string}/{f1:DateTime}/{f2:DateTime}/{tipo:string}/{estado:string}")]
    // public async Task<ActionResult<List<ReuDiumDTO>>> GetPendientes(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado)
    // {


    //     //Consultar nombre del centro y division  para insertarlos
    //     CentroDivisionDTO centrodiv = new CentroDivisionDTO();
    //     centrodiv = await GetCentroDiv(idcentro, iddiv, 0);


    //     string centro = centrodiv.Cnom;
    //     string div = centrodiv.Dnombre;

    //     reudiatablas = new List<ReuDium>();


    //     // Es Reunión
    //     if (tipo == "1")
    //     {

    //         if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
    //         {

    //             reudiatablas = await _neocontext.ReuDia
    //             //.Where(a =>  (a.Div == centro & a.Division==div ) | (a.Div == centro & a.Division == div & (a.Fecha>= f1 & a.Fecha <= f2)))
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div && (a.Rdstatus != "Listo" && a.Rdstatus != "Cerrado") && (a.RdfecReu >= f1.AddDays(-3) && a.RdfecReu <= f2)))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .Include(b => b.IdEmpresaNavigation)
    //             .OrderByDescending(b => b.RdfecReu)
    //             .ToListAsync();
    //         }
    //         else
    //         {

    //             reudiatablas = await _neocontext.ReuDia
    //             //.Where(a =>  (a.Div == centro & a.Division==div ) | (a.Div == centro & a.Division == div & (a.Fecha>= f1 & a.Fecha <= f2)))
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div && (a.Rdstatus != "Listo" && a.Rdstatus != "Cerrado") && (a.RdfecReu >= f1 && a.RdfecReu <= f2)))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .Include(b => b.IdEmpresaNavigation)
    //             .OrderByDescending(b => b.RdfecReu)
    //             .ToListAsync();
    //         }
    //     }

    //     //tipo 0 Pendientes para fecha reunion
    //     else if (tipo == "0")
    //     {


    //         if (estado == "Total Pendiente")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //            //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //            .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
    //            .Include(b => b.IdksfNavigation)
    //            .Include(b => b.IdResReuNavigation)
    //            .OrderByDescending(b => b.RdfecReu)
    //            .ToListAsync();
    //         }

    //         else if (estado == "Todo")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //             //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.RdplanAcc != null))
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .OrderByDescending(b => b.RdfecReu)
    //             .Take(500)
    //             .ToListAsync();
    //         }

    //         else if (estado == "Pendiente-Responsable")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente/Responsable"))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .OrderByDescending(b => b.RdfecReu)
    //             .Take(350)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else if (estado == "Vencidos")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra.Date < DateTime.Now.Date))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .OrderByDescending(b => b.RdfecReu)
    //             .Take(350)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //           //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //           //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) & (a.RdplanAcc != null))
    //           .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == estado))
    //           .Include(b => b.IdksfNavigation)
    //           .Include(b => b.IdResReuNavigation)
    //           .OrderByDescending(b => b.RdfecReu)
    //           .Take(350)
    //           .ToListAsync();
    //         }
    //     }
    //     //Fecha de trabajo
    //     else if (tipo == "2")
    //     {
    //         if (estado == "Total Pendiente")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //            //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //            .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
    //            .Include(b => b.IdksfNavigation)
    //            .Include(b => b.IdResReuNavigation)
    //            .OrderByDescending(b => b.RdfecTra)
    //            .ToListAsync();
    //         }

    //         else if (estado == "Todo")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //             //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.RdplanAcc != null))
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .OrderByDescending(b => b.RdfecTra)
    //             .Take(500)
    //             .ToListAsync();
    //         }
    //         else if (estado == "Pendiente-Responsable")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente/Responsable"))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .OrderByDescending(b => b.RdfecTra)
    //             .Take(350)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else if (estado == "Vencidos")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra.Date < DateTime.Now.Date))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .OrderByDescending(b => b.RdfecTra)
    //             .Take(350)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //           //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //           //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) & (a.RdplanAcc != null))
    //           .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == estado))
    //           .Include(b => b.IdksfNavigation)
    //           .Include(b => b.IdResReuNavigation)
    //           .OrderByDescending(b => b.RdfecTra)
    //           .Take(50)
    //           .ToListAsync();
    //         }

    //     return Ok(_mapper.Map<List<ReuDiumDTO>>(reudiatablas));


    //     }

    // }

    // //historicos
    // public async Task<List<ReuDium>> GetHistoricos(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado)
    // {


    //     //Consultar nombre del centro y division  para insertarlos
    //     CentroDivision centrodiv = new CentroDivision();
    //     centrodiv = await GetCentroDiv(idcentro, iddiv, 0);


    //     string centro = centrodiv.Cnom;
    //     string div = centrodiv.Dnombre;

    //     reudiatablas = new List<ReuDium>();

    //     if (tipo == "2")
    //     {


    //         if (estado == "Total Pendiente")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecTra >= f1 & a.RdfecTra <= f2)))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();

    //         }

    //         else if (estado == "Todo")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecTra >= f1 & a.RdfecTra <= f2))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else if (estado == "Pendiente-Responsable")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.Rdstatus == "Pendiente/Responsable"))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else if (estado == "Vencidos")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.RdfecTra.Date < DateTime.Now.Date))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }

    //         else
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //           //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //           .Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) && (a.RdfecTra >= f1 & a.RdfecTra <= f2))
    //           .Include(b => b.IdksfNavigation)
    //           .Include(b => b.IdResReuNavigation)
    //           .AsNoTracking()
    //           .ToListAsync();
    //         }
    //     }
    //     else
    //     {
    //         if (estado == "Total Pendiente")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }

    //         else if (estado == "Todo")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else if (estado == "Pendiente-Responsable")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2) && (a.Rdstatus == "Pendiente/Responsable"))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else if (estado == "Vencidos")
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //             .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.RdfecTra.Date < DateTime.Now.Date))
    //             .Include(b => b.IdksfNavigation)
    //             .Include(b => b.IdResReuNavigation)
    //             .AsNoTracking()
    //             .ToListAsync();
    //         }
    //         else
    //         {
    //             reudiatablas = await _neocontext.ReuDia
    //           //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
    //           .Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) && (a.RdfecReu >= f1 & a.RdfecReu <= f2))
    //           .Include(b => b.IdksfNavigation)
    //           .Include(b => b.IdResReuNavigation)
    //           .AsNoTracking()
    //           .ToListAsync();
    //         }
    //     }

    //     return reudiatablas;
    // }

    // //Update Discrepancia
    // public async Task<bool> UpdateDiscrepancia(ReuDium d, int id, int tipo, string f1, string f2, string estado)
    // {
    //     string div = "", centro = "";
    //     // DateTime f1= DateTime.Now;

    //     if (d.Rdcentro is not null)
    //     {
    //         //Consultar nombre del centro y division pra retornar el id en pendientes
    //         CentroDivision centrodiv = new CentroDivision();
    //         centrodiv = await GetCentroDiv(d.Rdcentro, d.Rddiv, 1);
    //         centro = centrodiv.IdCentro.ToString();
    //         div = centrodiv.IdDivision.ToString();

    //     }

    //     ReuDium bdDiscrep = new ReuDium();

    //     bdDiscrep = await _neocontext.ReuDia
    //         .Include(b => b.IdksfNavigation)
    //         .Include(b => b.IdResReuNavigation)
    //        .FirstOrDefaultAsync(sh => sh.IdReuDia == id);
    //     if (bdDiscrep == null)
    //         throw new Exception("Sorry, not found");


    //     bdDiscrep.Rdarea = d.Rdarea;
    //     bdDiscrep.Rddiv = d.Rddiv;
    //     bdDiscrep.RdcodDis = d.RdcodDis;
    //     bdDiscrep.RdcodEq = d.RdcodEq;
    //     bdDiscrep.Rddisc = d.Rddisc;
    //     bdDiscrep.Rdcentro = d.Rdcentro;
    //     bdDiscrep.RdfecReu = d.RdfecReu;
    //     bdDiscrep.RdfecTra = d.RdfecTra;
    //     bdDiscrep.Idksf = d.Idksf;
    //     bdDiscrep.RdplanAcc = d.RdplanAcc;
    //     bdDiscrep.Rdodt = d.Rdodt;
    //     bdDiscrep.IdResReu = d.IdResReu;
    //     bdDiscrep.Rdstatus = d.Rdstatus;
    //     bdDiscrep.Rdtiempo = d.Rdtiempo;
    //     bdDiscrep.IdPais = d.IdPais;
    //     bdDiscrep.IdEmpresa = d.IdEmpresa;
    //     //bdDiscrep.RdnumDis = d.RdnumDis;
    //     //bdDiscrep.Rdobs = d.Rdobs;

    //     try
    //     {
    //         _neocontext.Entry(bdDiscrep).State = EntityState.Modified;
    //         await _neocontext.SaveChangesAsync();
    //         reudiatablas = new List<ReuDium>();
    //         //bdDiscrep = null;

    //         if (tipo == 0)
    //         {
    //             _navigationManager.NavigateTo($"pendientes/{centro}/{div}/{f1}/{f2}/{tipo}/{estado}");
    //         }
    //         else if (tipo == 1)
    //         {
    //             _navigationManager.NavigateTo($"reunion/{centro}/{div}/{f1}/{f2}/{tipo}/Reunion");
    //         }
    //         else if (tipo == 2)
    //         {
    //             _navigationManager.NavigateTo($"pendientes/{centro}/{div}/{f1}/{f2}/{tipo}/{estado}");
    //         }

    //         return true;
    //     }
    //     catch (Exception ex)
    //     {
    //         return false;
    //     }


    // }
    // public async Task<bool> UpdateDiscrepancia2(ReuDium d, int id, int tipo, string f1, string f2, string estado, string linea)
    // {
    //     string div = "", centro = "";
    //     bool check = false;
    //     // DateTime f1= DateTime.Now;

    //     if (d.Rdcentro is not null)
    //     {
    //         //Consultar nombre del centro y division pra retornar el id en pendientes
    //         CentroDivision centrodiv = new CentroDivision();
    //         centrodiv = await GetCentroDiv(d.Rdcentro, d.Rddiv, 1);
    //         centro = centrodiv.IdCentro.ToString();
    //         div = centrodiv.IdDivision.ToString();

    //     }


    //     try
    //     {

    //         _neocontext.ReuDia.Update(d);
    //         check = await _neocontext.SaveChangesAsync() > 0;
    //         //reudiatablas = new List<ReuDium>();
    //         ////bdDiscrep = null;

    //         if (check)
    //         {
    //             if (tipo == 0)
    //             {
    //                 _navigationManager.NavigateTo($"pendientes/{centro}/{div}/{linea}/{f1}/{f2}/{tipo}/{estado}", forceLoad: true);
    //             }
    //             else if (tipo == 1)
    //             {
    //                 _navigationManager.NavigateTo($"reunion/{centro}/{div}/Re/{f1}/{f2}/{tipo}/Reunion", forceLoad: true);
    //             }
    //             else if (tipo == 2)
    //             {
    //                 _navigationManager.NavigateTo($"pendientes/{centro}/{div}/{linea}/{f1}/{f2}/{tipo}/{estado}", forceLoad: true);
    //             }

    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }

    //     }
    //     catch (Exception ex)
    //     {
    //         return false;
    //     }
    // }

    // //Obtener id de centro y dic=vision y viceversa

    // public async Task<CentroDivision> GetCentroDiv(string centro, string division, int tipo)
    // {
    //     CentroDivision CD = new CentroDivision();
    //     if (tipo == 0)
    //     {

    //         centrodiscrepancia = await _neocontext.Divisions
    //             .Include(c => c.IdCentroNavigation)
    //             .Where(d => d.IdDivision == int.Parse(division))
    //             .Select(di => new Division
    //             {
    //                 IdDivision = di.IdDivision,
    //                 Dnombre = di.Dnombre,
    //                 IdCentroNavigation = di.IdCentroNavigation
    //             })
    //             .AsNoTracking()
    //             .FirstOrDefaultAsync();



    //         CD.IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro;
    //         CD.IdDivision = centrodiscrepancia.IdDivision;
    //         CD.Cnom = centrodiscrepancia.IdCentroNavigation.Cnom;
    //         CD.Dnombre = centrodiscrepancia.Dnombre;
    //     }

    //     else if (tipo == 1)
    //     {



    //         centrodiscrepancia = await _neocontext.Divisions
    //             .Include(c => c.IdCentroNavigation)
    //             .Where(d => d.Dnombre == division && d.IdCentroNavigation.Cnom == centro)
    //             .Select(di => new Division
    //             {
    //                 IdDivision = di.IdDivision,
    //                 Dnombre = di.Dnombre,
    //                 IdCentroNavigation = di.IdCentroNavigation
    //             })
    //             .AsNoTracking()
    //             .FirstOrDefaultAsync();



    //         CD.IdCentro = centrodiscrepancia.IdCentroNavigation.IdCentro;
    //         CD.IdDivision = centrodiscrepancia.IdDivision;
    //         CD.Cnom = centrodiscrepancia.IdCentroNavigation.Cnom;
    //         CD.Dnombre = centrodiscrepancia.Dnombre;
    //     }


    //     return CD;
    // }

    // //Consultas para los cambios de estatus en una discrepancia
    // public async Task GetCambioStatus(int idreu)
    // {
    //     cambiostatus = await _neocontext.CambStats
    //         .Where(a => a.IdReuDia == idreu)
    //         // .OrderByDescending(b => b.IdCambStat)
    //         .ToListAsync();
    // }

    // public async Task GetCambioFecha(int idreu)
    // {
    //     cambiofecha = await _neocontext.CambFecs
    //         .Where(a => a.IdReuDia == idreu)
    //         //.OrderByDescending(b => b.IdCambFec)
    //         .ToListAsync();
    // }


    // //obtener discrepancia a editar
    // public async Task<ReuDium> GetDiscrepantacia(int id)
    // {
    //     var disc = await _neocontext.ReuDia
    //         .Include(b => b.IdksfNavigation)
    //         .Include(b => b.IdResReuNavigation)
    //         .FirstOrDefaultAsync(h => h.IdReuDia == id);
    //     if (disc == null)
    //         throw new Exception("not found!");
    //     return disc;

    // }


    // public async Task<int> InsertDiscrepancia(ReuDium discre)
    // {
    //     _neocontext.ReuDia.Add(discre);
    //     await _neocontext.SaveChangesAsync();
    //     return discre.IdReuDia;
    // }


    // public async Task InsertCambioStatus(CambStat status)
    // {
    //     _neocontext.CambStats.Add(status);
    //     await _neocontext.SaveChangesAsync();
    // }

    // public async Task InsertCambioFec(CambFec cambiofec)
    // {
    //     _neocontext.CambFecs.Add(cambiofec);
    //     await _neocontext.SaveChangesAsync();
    // }
    // //Insertar discrepancia con chismoso
    // public async Task<bool> InsertarRegistros(CambFec data, CambStat data2)
    // {
    //     data.IdReuDiaNavigation.CambStats.Add(data2);
    //     _neocontext.CambFecs.Add(data);
    //     //_neocontext.CambStats.Add(data2);
    //     await _neocontext.SaveChangesAsync();

    //     return true;
    // }

}

