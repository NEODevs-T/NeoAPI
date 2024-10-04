﻿using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Controllers.GetCentroDiv;


namespace ReunionWeb.ServicesController;
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
[ApiController]
public class DbReunionServiceController : ControllerBase
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




    [HttpGet("GetByODT/{ODT}/{idcentro}/{iddiv}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetByODT(string ODT, string idcentro, string iddiv)
    {
        OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);
        //Consultar nombre del centro y division  para insertarlos
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);


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

        return Ok(_mapper.Map<List<ReuDiumDTO>>(reudiatablas));
    }

    //TODO: cuidado al escanear
    [HttpGet("GetPendientes/{idcentro}/{iddiv}/{f1:DateTime}/{f2:DateTime}/{tipo}/{estado}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetPendientes(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado)
    {


        OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);
        //Consultar nombre del centro y division  para insertarlos
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);


        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<ReuDium>();


        // Es Reunión
        if (tipo == "1")
        {

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {

                reudiatablas = await _neocontext.ReuDia
                //.Where(a =>  (a.Div == centro & a.Division==div ) | (a.Div == centro & a.Division == div & (a.Fecha>= f1 & a.Fecha <= f2)))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div && (a.Rdstatus != "Listo" && a.Rdstatus != "Cerrado") && (a.RdfecReu >= f1.AddDays(-3) && a.RdfecReu <= f2)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .ToListAsync();
            }
            else
            {

                reudiatablas = await _neocontext.ReuDia
                //.Where(a =>  (a.Div == centro & a.Division==div ) | (a.Div == centro & a.Division == div & (a.Fecha>= f1 & a.Fecha <= f2)))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div && (a.Rdstatus != "Listo" && a.Rdstatus != "Cerrado") && (a.RdfecReu >= f1 && a.RdfecReu <= f2)))
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
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(500)
                .ToListAsync();
            }

            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {

                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == estado))
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
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(500)
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(350)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) & (a.RdplanAcc != null))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == estado))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .Take(50)
                .ToListAsync();
            }

        }

        return Ok(_mapper.Map<List<ReuDiumDTO>>(reudiatablas));

    }


    //historicos
    [HttpGet("GetHistoricos/{idcentro}/{iddiv}/{f1:DateTime}/{f2:DateTime}/{tipo}/{estado}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetHistoricos(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado)
    {


        //Consultar nombre del centro y division  para insertarlos
        OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);


        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<ReuDium>();

        if (tipo == "2")
        {


            if (estado == "Total Pendiente")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecTra >= f1 & a.RdfecTra <= f2)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();

            }

            else if (estado == "Todo")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecTra >= f1 & a.RdfecTra <= f2))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }

            else
            {
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) && (a.RdfecTra >= f1 & a.RdfecTra <= f2))
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
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _neocontext.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _neocontext.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) & (a.Rdstatus == estado) && (a.RdfecReu >= f1 & a.RdfecReu <= f2))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
        }
        return (_mapper.Map<List<ReuDiumDTO>>(reudiatablas));
    }

    // Update Discrepancia
    [HttpPut("UpdateDiscrepancia/{id:int}/{tipo}")]
    public async Task<ActionResult<bool>> UpdateDiscrepancia(ReuDiumDTO d, int id, int? tipo)
    {
        try
        {
            string div = "", centro = "";

            if (d.Rdcentro is not null)
            {
                // Consultar nombre del centro y división para retornar el id en pendientes
                OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);
                CentroDivisionDTO centrodiv = await getDiv.GetCentroDivi(d.Rdcentro, d.Rddiv, 1);

                if (centrodiv == null)
                {
                    return NotFound("Centro o División no encontrados.");
                }

                centro = centrodiv.IdCentro.ToString();
                div = centrodiv.IdDivision.ToString();
            }

            ReuDium bdDiscrep = await _neocontext.ReuDia
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

            if (bdDiscrep.IdMasterNavigation != null)
            {
                bdDiscrep.IdMasterNavigation.IdPais = d.IdPais;
            }

            bdDiscrep.IdEmpresa = d.IdEmpresa;

            // Verifica que el tipo sea válido
            if (tipo != null && tipo == 0)
            {
                _neocontext.Entry(bdDiscrep).State = EntityState.Modified;
                await _neocontext.SaveChangesAsync();

                return Ok(true);
            }

            return BadRequest("El tipo no es válido o no se proporcionó.");
        }
        catch (Exception ex)
        {
            // Manejo de la excepción
            return StatusCode(500, $"Ocurrió un error en el servidor: {ex.Message}");
        }
    }


    [HttpPut("UpdateDiscrepancia2")]
    public async Task<ActionResult<bool>> UpdateDiscrepancia2(ReuDiumDTO d)
    {
        string div = "", centro = "";
        bool check = false;
        // DateTime f1= DateTime.Now;
        try
        {
            if (d.Rdcentro is not null)
            {
                //Consultar nombre del centro y division pra retornar el id en pendientes
                OptencionDeDiv getDiv = new OptencionDeDiv(_neocontext);
                CentroDivisionDTO centrodiv = await getDiv.GetCentroDivi(d.Rdcentro, d.Rddiv, 1);

                var _d = _mapper.Map<ReuDium>(d);
                _neocontext.ReuDia.Update(_d);
                check = await _neocontext.SaveChangesAsync() > 0;
                //reudiatablas = new List<ReuDium>();
                ////bdDiscrep = null;
                return Ok(true);
            }

            return BadRequest("El tipo no es válido o no se proporcionó.");
        }
        catch (Exception ex)
        {
            // Manejo de la excepción
            return StatusCode(500, $"Ocurrió un error en el servidor: {ex.Message}");
        }

    }

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

