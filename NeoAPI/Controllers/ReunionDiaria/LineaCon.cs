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

namespace ReunionDiaApi.Controllers;

[Route("/[controller]")]
[ApiController]
public class LineasController : ControllerBase
{
    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;
    private readonly DbNeoIiContext _neoVieja;

    public LineasController(DbNeoIiContext DbNeo, IMapper maper, DbNeoIiContext neoVieja)
    {
        _context = DbNeo;
        _mapper = maper;
        _neoVieja = neoVieja;
    }
    public static List<Linea> linea = new List<Linea> { };
    public static List<Empresa> empresa = new List<Empresa> { };
    public static List<Pai> pais = new List<Pai> { };
    public static List<Division> div = new List<Division> { };
    public static List<Ksf> ksfs = new List<Ksf>();
    public static List<RespoReu> resporeu = new List<RespoReu>();
    public static List<ReuDium> reunionditabla = new List<ReuDium>();
    public static List<AsistenReu> asistenreus = new List<AsistenReu>();
    public static List<CargoReu> cargoreus = new List<CargoReu>();
    public static List<StatsAsisDto> StatsAsis = new List<StatsAsisDto>();
    public static List<EquipoEam> equipos = new List<EquipoEam>();


    //TODO: Areglos proporcionados por chat gpt

    [HttpGet("GetBdDiv{cent}")]
    public async Task<ActionResult<List<LineaDTO>>> GetBdDiv(string cent)
    {
        List<Master> data = new List<Master>();

        if (cent == "All")
        {
            data = await _context.Masters
               .Include(x => x.IdLineaNavigation) // Incluye la tabla Linea
                .ToListAsync();
        }
        else
        {
            if (!Int32.TryParse(cent, out int _cent)) // Asegura que el parámetro es un número válido
            {
                return BadRequest("El parámetro cent debe ser un número entero.");
            }

            data = await _context.Masters
               .Where(x => x.IdCentro == _cent) // Filtra por centro
               .Include(x => x.IdLineaNavigation) // Incluye la tabla Linea
               .ToListAsync();
        }

        var lineasDTO = data.Select(d => new LineaDTO
        {
            IdLinea = d.IdLineaNavigation.IdLinea,
            Lnom = d.IdLineaNavigation.Lnom,
            Ldetalle = d.IdLineaNavigation.Ldetalle,
            Lestado = d.IdLineaNavigation.Lestado,
            LcenCos = d.IdLineaNavigation.LcenCos,
            Lfecha = d.IdLineaNavigation.Lfecha,
            Lofic = d.IdLineaNavigation.Lofic
        }).ToList();

        return Ok(lineasDTO);
    }

    //TODO: nueva implementacion
    [HttpGet("GetEquipos/{cent}")]
    public async Task<ActionResult<List<EquipoEam>>> EquiposEAM(string cent)
    {
        string cen = "";
        int idempresa = 0;
        if (cent.Length > 3)
        {
            cen = cent.Substring(0, 3);
            idempresa = int.Parse(cent.Substring(3));
        }

        if (cen == "All")
        {
            var result = await _context.EquipoEams
             .Include(x => x.IdLineaNavigation)
             .Where(x => x.EestaEam == true & x.IdLineaNavigation.Master.IdEmpresaNavigation.IdEmpresa == idempresa)
             .Select(p => new EquipoEamDTO
             {
                 EcodEquiEam = p.EcodEquiEam,
                 EnombreEam = p.EnombreEam,
             })
             .AsNoTracking()
              .ToListAsync();

            return Ok(result);
        }
        else
        {


            var result = await _context.EquipoEams
             .Include(x => x.IdLineaNavigation)
             //.Where(x => x.IdLineaNavigation.IdDivisionNavigation.IdCentroNavigation.Cnom == cent && x.EestaEam == true)
             .Where(x => x.IdLineaNavigation.Master.IdCentro == int.Parse(cent) && x.EestaEam == true)
             .Select(p => new EquipoEamDTO
             {
                 EcodEquiEam = p.EcodEquiEam,
                 EnombreEam = p.EnombreEam,
                 // linea = p.IdLineaNavigation
             })
             .AsNoTracking()
              .ToListAsync();

            return Ok(result);
        }
    }

    [HttpGet("GetAsistencia/{centro}/{empresa}")]
    public async Task<ActionResult<List<AsistenReuDTO>>> GetAsistencia(string centro, string empresa)
    {

        cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro & a.Crempresa == empresa & a.Cresta == true)
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        return Ok(_mapper.Map<List<AsistenReuDTO>>(cargoreus));
    }


    [HttpGet("Ksf")]
    public async Task<ActionResult<List<KsfDTO>>> GetKsf()
    {

        ksfs = await _context.Ksfs
            .Where(a => a.KsfEsta == true)
            .ToListAsync();

        return Ok(_mapper.Map<List<KsfDTO>>(ksfs));
    }

    [HttpGet("Responsables")]
    public async Task<ActionResult<List<RespoReuDTO>>> GetRespon()
    {


        resporeu = await _context.RespoReus
            .Where(a => a.Rresta == true)
            .ToListAsync();


        return Ok(_mapper.Map<List<RespoReuDTO>>(resporeu));
    }

    // //Obtener asistencia en porcentaje
    // [HttpGet("StatsAsis/{cent}/{empresa}/{f1}/{f2}")]
    // public async Task<ActionResult<List<StatsAsisDto>>> GetStatsAsis(string cent, string empresa, string f1, string f2)
    // {

    //     string[] fecha1 = f1.Split('-');
    //     string[] fecha2 = f2.Split('-');

    //     //año, mes dia
    //     DateTime date1 = new DateTime(int.Parse(fecha1[2]), int.Parse(fecha1[1]), int.Parse(fecha1[0]));
    //     DateTime date2 = new DateTime(int.Parse(fecha2[2]), int.Parse(fecha2[1]), int.Parse(fecha2[0]));


    //     if (cent == "All")
    //     {
    //         var result = await _context.AsistenReus
    //         .Include(x => x.AridCargoRNavigation)
    //         .Where(x => x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2)
    //         .GroupBy(x => x.AridCargoRNavigation.Crnombre)
    //         .Select(a => new
    //         {
    //             a.Key,
    //             Asistencias = a.Sum(b => b.ArAsistente)
    //         })
    //         .ToListAsync();

    //         return Ok(result);
    //     }

    //     else
    //     {
    //         var result = await _context.AsistenReus
    //         .Include(x => x.AridCargoRNavigation)
    //         .Where(x => (x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2) && x.Ararea == cent && x.AridCargoRNavigation.Crempresa == empresa)
    //         .GroupBy(x => x.AridCargoRNavigation.Crnombre)
    //         .Select(a => new
    //         {
    //             Cargo = a.Key,
    //             Asistencias = a.Sum(b => b.ArAsistente)
    //         })
    //         .ToListAsync();

    //         return Ok(result);
    //     }


    //     return Ok();
    // }

    // //Obtener asistencia por dia
    // [HttpGet("ListaAsis/{cent}/{empresa}/{f1}/{f2}")]
    // public async Task<ActionResult<List<AsistenReu>>> GetListaAsis(string cent, string empresa, string f1, string f2)
    // {

    //     string[] fecha1 = f1.Split('-');
    //     string[] fecha2 = f2.Split('-');

    //     //año, mes dia
    //     DateTime date1 = new DateTime(int.Parse(fecha1[2]), int.Parse(fecha1[1]), int.Parse(fecha1[0]));
    //     DateTime date2 = new DateTime(int.Parse(fecha2[2]), int.Parse(fecha2[1]), int.Parse(fecha2[0]));



    //     if (cent == "All")
    //     {
    //         var result = await _context.AsistenReus
    //        .Include(x => x.AridCargoRNavigation)
    //        .Where(x => x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2)
    //        .GroupBy(x => x.AridCargoRNavigation.Crnombre)
    //        .ToListAsync();

    //         return Ok(result);
    //     }

    //     else
    //     {
    //         var result = await _context.AsistenReus
    //        .Include(x => x.AridCargoRNavigation)
    //        .Where(x => (x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2) && x.Ararea == cent && x.AridCargoRNavigation.Crempresa == empresa)
    //        .ToListAsync();

    //         return Ok(result);
    //     }


    //     return Ok();
    // }

    // [HttpGet("EquiposLinea/{Centro}")]
    // public async Task<ActionResult<List<Empresa>>> EquiposLineaEAM(string Centro)
    // {

    //     empresa = await _context.Empresas
    //     .Include(x => x.IdPaisNavigation)
    //     .Include(y => y.Centros)
    //     .Where(x => x.Centros.First(i => i.Cnom == Centro).IdEmpresa == x.IdEmpresa)
    //     .ToListAsync();


    //     return Ok(empresa);
    // }

    // [HttpPost("Asistencia")]
    // public async Task<ActionResult<string>> SaveAsistencia(List<AsistenReu> list)
    // {
    //     DateTime d = DateTime.Today;

    //     try
    //     {
    //         var result = await _context.AsistenReus
    //         .Include(x => x.AridCargoRNavigation)
    //         .Where(x => (x.Arfecha >= d) && (x.Ararea == list[0].Ararea) && (x.AridCargoRNavigation.Crempresa == list[0].AridCargoRNavigation.Crempresa) && (x.AridCargoRNavigation.CRBloque == list[0].AridCargoRNavigation.CRBloque))
    //         .FirstOrDefaultAsync();
    //         if (result == null)
    //         {

    //             for (var i = 0; i < list.Count; i++)
    //             {
    //                 AsistenReu insertar = new AsistenReu();
    //                 insertar.Ararea = list[i].Ararea;
    //                 insertar.Arfecha = list[i].Arfecha;
    //                 insertar.AridCargoR = list[i].AridCargoR;
    //                 insertar.ArAsistente = list[i].ArAsistente;
    //                 insertar.ArSuplente = list[i].ArSuplente;
    //                 insertar.Ararea = list[i].Ararea;

    //                 _context.AsistenReus.Add(insertar);
    //                 await _context.SaveChangesAsync();

    //             }
    //             return Ok("Registro Exitoso");
    //         }
    //         else
    //         {
    //             return BadRequest("Ya se registró asistencia");
    //         }

    //     }
    //     catch
    //     {
    //         return BadRequest("Error, intente nuevamente");
    //     }

    // }

    // //Obtener trabajos pendientes para el calendario
    // [HttpGet("TrabajosCalendario/{pais}/{centro}/{division}")]
    // public async Task<ActionResult<List<CalendarioTrabajoDTO>>> GetTrabajosCalendario(string pais, string centro, string division)
    // {

    //     if (division == "All")
    //     {
    //         var list = await _context.ReuDia
    //               .Include(x => x.IdResReuNavigation)
    //                 .Where(d => (d.Rdstatus == "Pendiente/Responsable" | d.Rdstatus == "Pendiente") & (d.IdPais == int.Parse(pais)) & (d.Rdcentro == centro))
    //                 .Select(rd => new
    //                 {
    //                     IdReuDia = rd.IdReuDia,
    //                     RdcodEq = rd.RdcodEq,
    //                     Rddisc = rd.Rddisc,
    //                     Rdodt = rd.Rdodt,
    //                     Rdtiempo = rd.Rdtiempo,
    //                     RdfecTra = rd.RdfecTra,
    //                     Responsable = rd.IdResReuNavigation.Rrnombre,
    //                     Linea = rd.Rdarea

    //                 })
    //                 .AsNoTracking()
    //                 .ToListAsync();
    //         return Ok(list);
    //     }
    //     else
    //     {
    //         var list = await _context.ReuDia
    //               .Include(x => x.IdResReuNavigation)
    //                 .Where(d => (d.Rdstatus == "Pendiente/Responsable" | d.Rdstatus == "Pendiente") & (d.IdPais == int.Parse(pais)) & (d.Rdcentro == centro) & (d.Rddiv == division))
    //                 .Select(rd => new
    //                 {
    //                     IdReuDia = rd.IdReuDia,
    //                     RdcodEq = rd.RdcodEq,
    //                     Rddisc = rd.Rddisc,
    //                     Rdodt = rd.Rdodt,
    //                     Rdtiempo = rd.Rdtiempo,
    //                     RdfecTra = rd.RdfecTra,
    //                     Responsable = rd.IdResReuNavigation.Rrnombre,
    //                     Linea = rd.Rdarea

    //                 })
    //                 .AsNoTracking()
    //                 .ToListAsync();
    //         return Ok(list);
    //     }


    // }


}

