using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.GetCentroDiv;


namespace NeoAPI.Controllers.Avisador;

[ApiController]
[Route("api/[controller]")]
public class AvisadorController : ControllerBase
{
    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public AvisadorController(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("AddRegistros")]
    public async Task<ActionResult<bool>> AddRegistros((List<CambFecDTO> lista1, List<CambStatDTO> lista2) data)
    {
        List<CambFec> _lista1 = _mapper.Map<List<CambFec>>(data.lista1);
        List<CambStat> _lista2 = _mapper.Map<List<CambStat>>(data.lista2);

        foreach (var item in _lista1)
        {
            this._context.CambFecs.Add(item);
        }

        foreach (var item in _lista2)
        {
            this._context.CambStats.Add(item);
        }
        return Ok(await _context.SaveChangesAsync() > 0);
    }


    [HttpPost("AddAsistencia")]
    public async Task<ActionResult<string>> SaveAsistencia(List<AsistenReuDTO> list)
    {
        DateTime d = DateTime.Today;

        try
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => (x.Arfecha >= d) && (x.Ararea == list[0].Ararea) && (x.IdCargoRNavigation.Crempresa == list[0].Cargo.Crempresa) && (x.IdCargoRNavigation.Crbloque == list[0].Cargo.Crbloque))
            .FirstOrDefaultAsync();
            if (result == null)
            {

                for (var i = 0; i < list.Count; i++)
                {
                    AsistenReu insertar = new AsistenReu();
                    insertar.Ararea = list[i].Ararea;
                    insertar.Arfecha = list[i].Arfecha;
                    insertar.IdCargoR = list[i].IdCargoR;
                    insertar.ArAsistente = list[i].ArAsistente;
                    insertar.ArSuplente = list[i].ArSuplente;
                    insertar.Ararea = list[i].Ararea;

                    _context.AsistenReus.Add(insertar);
                    await _context.SaveChangesAsync();

                }
                return Ok("Registro Exitoso");
            }
            else
            {
                return BadRequest("Ya se registró asistencia");
            }

        }
        catch
        {
            return BadRequest("Error, intente nuevamente");
        }

    }


    [HttpGet("GetTrabajosPorCalendario/{pais}/{centro}/{division}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetTrabajosCalendario(string pais, string centro, string division)
    {

        if (division == "All")
        {
            var list = await _context.ReuDia
                    .Include(x => x.IdResReuNavigation)
                    .Where(d => (d.Rdstatus == "Pendiente/Responsable" || d.Rdstatus == "Pendiente") && (d.IdMasterNavigation.IdPais == int.Parse(pais)) && (d.Rdcentro == centro))
                    .AsNoTracking()
                    .ToListAsync();

            return Ok(_mapper.Map<List<ReuDiumDTO>>(list));
        }
        else
        {
            var list = await _context.ReuDia
                    .Include(x => x.IdResReuNavigation)
                    .Where(d => (d.Rdstatus == "Pendiente/Responsable" || d.Rdstatus == "Pendiente") && (d.IdMasterNavigation.IdPais == int.Parse(pais)) && (d.Rdcentro == centro) && (d.Rddiv == division))
                    .AsNoTracking()
                    .ToListAsync();


            return Ok(_mapper.Map<List<ReuDiumDTO>>(list));
            // return Ok(result);
        }


    }


    [HttpGet("GetAsistencia/{centro}/{empresa}")]
    public async Task<ActionResult<List<CargoReuDTO>>> GetAsistencia(string centro, string empresa)
    {

        List<CargoReu> cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro & a.Crempresa == empresa & a.Cresta == true)
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        return Ok(_mapper.Map<List<CargoReuDTO>>(cargoreus));
    }

    [HttpGet("GetKsf")]
    public async Task<ActionResult<List<KsfDTO>>> GetKsf()
    {

        List<Ksf> ksfs = await _context.Ksfs
            .Where(a => a.KsfEsta == true)
            .ToListAsync();

        return Ok(_mapper.Map<List<KsfDTO>>(ksfs));
    }

    [HttpGet("GetResponsables")]
    public async Task<ActionResult<List<RespoReuDTO>>> GetRespon()
    {


        List<RespoReu> resporeu = await _context.RespoReus
            .Where(a => a.Rresta == true)
            .ToListAsync();


        return Ok(_mapper.Map<List<RespoReuDTO>>(resporeu));
    }

    [HttpGet("GetStatsAsis/{cent}/{empresa}/{Fecha_inicio}/{Fecha_Final}")]
    public async Task<ActionResult<List<AsistenReuDTO>>> GetStatsAsis(string cent, string empresa, string Fecha_inicio, string Fecha_Final)
    {

        string[] fecha1 = Fecha_inicio.Split('-');
        string[] fecha2 = Fecha_Final.Split('-');

        //año, mes dia
        DateTime date1 = new DateTime(int.Parse(fecha1[2]), int.Parse(fecha1[1]), int.Parse(fecha1[0]));
        DateTime date2 = new DateTime(int.Parse(fecha2[2]), int.Parse(fecha2[1]), int.Parse(fecha2[0]));


        if (cent == "All")
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2)
            .GroupBy(x => x.IdCargoRNavigation.Crnombre)
            .ToListAsync();

            var carStDTO = result.Select(a => new CarStDTO
            {
                Cargo = a.Key,
                Asistencias = a.Sum(b => b.ArAsistente)
            });

            return Ok(carStDTO);
        }

        else
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => (x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2) && x.Ararea == cent && x.IdCargoRNavigation.Crempresa == empresa)
            .GroupBy(x => x.IdCargoRNavigation.Crnombre)
            .ToListAsync();

            var carStDTO = result.Select(a => new CarStDTO
            {
                Cargo = a.Key,
                Asistencias = a.Sum(b => b.ArAsistente)
            });


            return Ok(carStDTO);
        }
    }

    [HttpGet("GetListaAsis/{cent}/{empresa}/{f1}/{f2}")]
    public async Task<ActionResult<List<AsistenReuDTO>>> GetListaAsis(string cent, string empresa, string f1, string f2)
    {

        string[] fecha1 = f1.Split('-');
        string[] fecha2 = f2.Split('-');

        //año, mes dia
        DateTime date1 = new DateTime(int.Parse(fecha1[2]), int.Parse(fecha1[1]), int.Parse(fecha1[0]));
        DateTime date2 = new DateTime(int.Parse(fecha2[2]), int.Parse(fecha2[1]), int.Parse(fecha2[0]));



        if (cent == "All")
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2)
            .GroupBy(x => x.IdCargoRNavigation.Crnombre)
            .ToListAsync();

            return Ok(_mapper.Map<List<AsistenReuDTO>>(result));

        }

        else
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => (x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2) && x.Ararea == cent && x.IdCargoRNavigation.Crempresa == empresa)
            .ToListAsync();

            return Ok(_mapper.Map<List<AsistenReuDTO>>(result));
        }

    }

    [HttpGet("GetPendientes/{idcentro}/{iddiv}/{f1:DateTime}/{f2:DateTime}/{tipo}/{estado}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetPendientes(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado)
    {


        ReuClass getDiv = new ReuClass(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        List<ReuDium> reudiatablas = new List<ReuDium>();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);


        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<ReuDium>();


        // Es Reunión
        if (tipo == "1")
        {

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {

                reudiatablas = await _context.ReuDia
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

                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecReu)
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
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

                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
                //.Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus == "Pendiente" || a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .OrderByDescending(b => b.RdfecTra)
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
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

    [HttpGet("GetByODT/{ODT}/{idcentro}/{iddiv}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetByODT(string ODT, string idcentro, string iddiv)
    {
        ReuClass getDiv = new ReuClass(_context);
        //Consultar nombre del centro y division  para insertarlos
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);
        List<ReuDium> reudiatablas = new List<ReuDium>();



        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<ReuDium>();

        reudiatablas = await _context.ReuDia
        .Where(a => a.Rdodt.Contains(ODT) && (a.Rdcentro == centrodiv.Cnom && a.Rddiv == centrodiv.Dnombre))
        .Include(b => b.IdksfNavigation)
        .Include(b => b.IdResReuNavigation)
        .Include(b => b.IdMasterNavigation.IdEmpresaNavigation)
        .OrderByDescending(b => b.RdfecReu)
        .ToListAsync();

        return Ok(_mapper.Map<List<ReuDiumDTO>>(reudiatablas));
    }


    //historicos
    [HttpGet("GetHistoricos/{idcentro}/{iddiv}/{f1:DateTime}/{f2:DateTime}/{tipo}/{estado}")]
    public async Task<ActionResult<List<ReuDiumDTO>>> GetHistoricos(string idcentro, string iddiv, DateTime f1, DateTime f2, string tipo, string estado)
    {


        //Consultar nombre del centro y division  para insertarlos
        ReuClass getDiv = new ReuClass(_context);
        CentroDivisionDTO centrodiv = new CentroDivisionDTO();
        centrodiv = await getDiv.GetCentroDivi(idcentro, iddiv, 0);
        List<ReuDium> reudiatablas;



        string centro = centrodiv.Cnom;
        string div = centrodiv.Dnombre;

        reudiatablas = new List<ReuDium>();

        if (tipo == "2")
        {


            if (estado == "Total Pendiente")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecTra >= f1 & a.RdfecTra <= f2)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();

            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecTra >= f1 & a.RdfecTra <= f2))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }

            else
            {
                reudiatablas = await _context.ReuDia
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
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div & (a.Rdstatus == "Pendiente" | a.Rdstatus == "Pendiente/Responsable") && (a.RdfecReu >= f1 & a.RdfecReu <= f2)))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }

            else if (estado == "Todo")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Pendiente-Responsable")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro & a.Rddiv == div) && (a.RdfecReu >= f1 & a.RdfecReu <= f2) && (a.Rdstatus == "Pendiente/Responsable"))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else if (estado == "Vencidos")
            {
                reudiatablas = await _context.ReuDia
                .Where(a => (a.Rdcentro == centro && a.Rddiv == div) && (a.Rdstatus.StartsWith("Pendiente")) && (a.RdfecTra >= f1 & a.RdfecTra <= f2) && (a.RdfecTra < DateTime.Now.Date))
                .Include(b => b.IdksfNavigation)
                .Include(b => b.IdResReuNavigation)
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                reudiatablas = await _context.ReuDia
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
                ReuClass getDiv = new ReuClass(_context);
                CentroDivisionDTO centrodiv = await getDiv.GetCentroDivi(d.Rdcentro, d.Rddiv, 1);

                if (centrodiv == null)
                {
                    return NotFound("Centro o División no encontrados.");
                }

                centro = centrodiv.IdCentro.ToString();
                div = centrodiv.IdDivision.ToString();
            }

            ReuDium bdDiscrep = await _context.ReuDia
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

            bdDiscrep.IdEmpresa = d.IdEmpresa;

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

    [HttpPut("UpdateDiscrepancia2")]
    public async Task<ActionResult<bool>> UpdateDiscrepancia2(ReuDiumDTO d)
    {
        if (d?.Rdcentro == null)
        {
            return BadRequest("El centro no es válido o no se proporcionó.");
        }

        try
        {
            ReuClass getDiv = new ReuClass(_context);
            CentroDivisionDTO centrodiv = await getDiv.GetCentroDivi(d.Rdcentro, d.Rddiv, 1);

            // Mapeo con AutoMapper
            var entity = _mapper.Map<ReuDium>(d);

            _context.ReuDia.Update(entity);
            bool isUpdated = await _context.SaveChangesAsync() > 0;

            return isUpdated ? Ok(true) : StatusCode(500, "No se pudo actualizar la discrepancia.");
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Ocurrió un error en el servidor: {ex.Message}");
        }
    }


    //Consultas para los cambios de estatus en una discrepancia
    [HttpGet("GetCambioStatus/{idreu:int}")]
    public async Task<ActionResult<List<CambStatDTO>>> GetCambioStatus(int idreu)
    {
        List<CambStat> cambiostatus = await _context.CambStats
            .Where(a => a.IdReuDia == idreu)
            // .OrderByDescending(b => b.IdCambStat)
            .ToListAsync();

        return Ok(_mapper.Map<List<CambStatDTO>>(cambiostatus));
    }

    [HttpGet("GetCambioFecha/{idReu:int}")]
    public async Task<ActionResult<List<CambFecDTO>>> GetCambioFecha(int idReu)
    {
        List<CambFec> cambiofecha = await _context.CambFecs
            .Where(a => a.IdReuDia == idReu)
            //.OrderByDescending(b => b.IdCambFec)
            .ToListAsync();
        return Ok(_mapper.Map<List<CambFecDTO>>(cambiofecha));

    }

    //obtener discrepancia a editar
    [HttpGet("GetDiscrepantaciaJT/{id:int}")]
    public async Task<ActionResult<ReuDiumDTO>> GetDiscrepantacia(int id)
    {
        var disc = await _context.ReuDia
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
        _context.ReuDia.Add(data);
        await _context.SaveChangesAsync();
        return Ok(data.IdReuDia);
    }

    [HttpPost("AddtCambioStatus")]
    public async Task<ActionResult<bool>> InsertCambioStatus(CambStatDTO status)
    {
        CambStat data = _mapper.Map<CambStat>(status);
        _context.CambStats.Add(data);

        return Ok(await _context.SaveChangesAsync() > 0);
    }

    [HttpPost("AddCambioFec")]
    public async Task<ActionResult<bool>> InsertCambioFec(CambFecDTO cambiofec)
    {
        CambFec data = _mapper.Map<CambFec>(cambiofec);
        _context.CambFecs.Add(data);
        return Ok(await _context.SaveChangesAsync() > 0);
    }

    //Insertar discrepancia con chismoso
    [HttpPost("AddRegistrosCambios")]
    public async Task<ActionResult<bool>> InsertarRegistros(RegistroCambiosDTO registroCambios)
    {
        if (registroCambios == null)
        {
            return BadRequest("El objeto RegistroCambiosDTO no puede ser nulo.");
        }

        CambFec cambiofec;
        CambStat cambioEstado;

        try
        {
            cambiofec = _mapper.Map<CambFec>(registroCambios.cambFecDTO);
            cambioEstado = _mapper.Map<CambStat>(registroCambios.cambStatDTO);
        }
        catch (AutoMapperMappingException ex)
        {
            return StatusCode(500, $"Error al mapear los datos: {ex.Message}");
        }

        if (cambiofec.IdReuDiaNavigation == null || cambioEstado.IdReuDiaNavigation == null)
        {
            return BadRequest("Las propiedades de navegación IdReuDiaNavigation no pueden ser nulas.");
        }

        cambiofec.IdReuDiaNavigation.IdksfNavigation = null;
        cambiofec.IdReuDiaNavigation.IdMasterNavigation = null;
        cambiofec.IdReuDiaNavigation.IdResReuNavigation = null;

        cambioEstado.IdReuDiaNavigation.IdksfNavigation = null;
        cambioEstado.IdReuDiaNavigation.IdMasterNavigation = null;
        cambioEstado.IdReuDiaNavigation.IdResReuNavigation = null;

        try
        {
            _context.CambStats.Add(cambioEstado);
            _context.CambFecs.Add(cambiofec);
            bool result = await _context.SaveChangesAsync() > 0;

            return Ok(result);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, $"Error al guardar en la base de datos: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error desconocido: {ex.Message}");
        }
    }

}
