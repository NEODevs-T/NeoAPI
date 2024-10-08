using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.GetCentroDiv;


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
    public Master? centrodiscrepancia { get; set; } = new Master();


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
        ReuClass getDiv = new ReuClass(_neocontext);
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


        ReuClass getDiv = new ReuClass(_neocontext);
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
        ReuClass getDiv = new ReuClass(_neocontext);
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
    [HttpPut("UpdateDiscrepancia/{id:int}")]
    public async Task<ActionResult<bool>> UpdateDiscrepancia(ReuDiumDTO d, int id)
    {
        try
        {
            string div = "", centro = "";

            if (d.Rdcentro is not null)
            {
                // Consultar nombre del centro y división para retornar el id en pendientes
                ReuClass getDiv = new ReuClass(_neocontext);
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
                bdDiscrep.IdMasterNavigation.IdPais = d.IdPais ?? 0;
            }

            bdDiscrep.IdEmpresa = d.IdEmpresa;

            _neocontext.Entry(bdDiscrep).State = EntityState.Modified;
            await _neocontext.SaveChangesAsync();

            return Ok(true);
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
                ReuClass getDiv = new ReuClass(_neocontext);
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

    //TODO: viejo

    [HttpGet("GetHistoricos/{centro}/{division}/{tipo:int}")]
    public async Task<ActionResult<CentroDivisionDTO>> GetCentroDiv(string centro, string division, int tipo)
    {
        CentroDivisionDTO CD = new CentroDivisionDTO();
        ReuClass ReuBuild = new ReuClass(_neocontext);

        if (tipo == 0)
        {
            // Validar si 'division' puede convertirse a un entero
            if (!int.TryParse(division, out int divisionId))
            {
                return BadRequest("El valor de la división no es un número válido.");
            }

            // Ejecutar la consulta para el tipo 0
            var centrodiscrepancia = await _neocontext.Masters
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
            var centrodiscrepancia = await _neocontext.Masters
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

    //Consultas para los cambios de estatus en una discrepancia
    [HttpGet("GetCambioStatus/{idreu:int}")]
    public async Task<ActionResult<List<CambStatDTO>>> GetCambioStatus(int idreu)
    {
        cambiostatus = await _neocontext.CambStats
            .Where(a => a.IdReuDia == idreu)
            // .OrderByDescending(b => b.IdCambStat)
            .ToListAsync();

        return Ok(_mapper.Map<List<CambStatDTO>>(cambiostatus));
    }

    [HttpGet("GetCambioFecha/{idReu:int}")]
    public async Task<ActionResult<List<CambFecDTO>>> GetCambioFecha(int idReu)
    {
        cambiofecha = await _neocontext.CambFecs
            .Where(a => a.IdReuDia == idReu)
            //.OrderByDescending(b => b.IdCambFec)
            .ToListAsync();
        return Ok(_mapper.Map<List<CambFecDTO>>(cambiofecha));

    }


    //obtener discrepancia a editar
    [HttpGet("GetDiscrepantaciaJT/{id:int}")]
    public async Task<ActionResult<ReuDiumDTO>> GetDiscrepantacia(int id)
    {
        var disc = await _neocontext.ReuDia
            .Include(b => b.IdksfNavigation)
            .Include(b => b.IdResReuNavigation)
            .FirstOrDefaultAsync(h => h.IdReuDia == id);
        if (disc == null)
            throw new Exception("not found!");
        return Ok(_mapper.Map<ReuDiumDTO>(disc));
    }

    [HttpPost("AddDiscrepancia")]
    public async Task<ActionResult<int>> InsertDiscrepancia(ReuDiumDTO discre)
    {
        ReuDium data = _mapper.Map<ReuDium>(discre);
        _neocontext.ReuDia.Add(data);
        await _neocontext.SaveChangesAsync();
        return Ok(data.IdReuDia);
    }

    [HttpPost("AddtCambioStatus")]
    public async Task<ActionResult<bool>> InsertCambioStatus(CambStatDTO status)
    {
        CambStat data = _mapper.Map<CambStat>(status);
        _neocontext.CambStats.Add(data);

        return Ok(await _neocontext.SaveChangesAsync() > 0);
    }

    [HttpPost("AddCambioFec")]
    public async Task<ActionResult<bool>> InsertCambioFec(CambFecDTO cambiofec)
    {
        CambFec data = _mapper.Map<CambFec>(cambiofec);
        _neocontext.CambFecs.Add(data);
        return Ok(await _neocontext.SaveChangesAsync() > 0);
    }

    //Insertar discrepancia con chismoso
    [HttpPost("AddRegistros")]
    public async Task<ActionResult<bool>> InsertarRegistros(RegistroCambiosDTO registroCambios)
    {
        CambFec cambiofec = _mapper.Map<CambFec>(registroCambios.Data);
        CambStat cambioEstado = _mapper.Map<CambStat>(registroCambios.Data2);
        _neocontext.CambStats.Add(cambioEstado);
        _neocontext.CambFecs.Add(cambiofec);

        //     //_neocontext.CambStats.Add(data2);

        return Ok(await _neocontext.SaveChangesAsync() > 0);
    }

//TODO: ARREGLOS DE IMPLEMENTACION

    [HttpGet("GetCentroDivi/{centro}/{division}/{tipo:int}")]
        public async Task<ActionResult<CentroDivisionDTO>> GetCentroDivi(string centro, string division, int tipo)
    {
        CentroDivisionDTO CD = new CentroDivisionDTO();

        if (tipo == 0)
        {
            int divisionInt;
            if (!int.TryParse(division, out divisionInt))
            {
                throw new FormatException("El valor de 'division' no es un número válido.");
            }

            centrodiscrepancia = await _neocontext.Masters
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
            centrodiscrepancia = await _neocontext.Masters
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

