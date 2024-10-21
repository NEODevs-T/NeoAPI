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



}

