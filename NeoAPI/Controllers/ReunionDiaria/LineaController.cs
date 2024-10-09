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

[Route("api/[controller]")]
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
    public static List<Pai> pais = new List<Pai> { };
    public static List<Division> div = new List<Division> { };
    public static List<Ksf> ksfs = new List<Ksf>();
    public static List<RespoReu> resporeu = new List<RespoReu>();
    public static List<ReuDium> reunionditabla = new List<ReuDium>();
    public static List<AsistenReu> asistenreus = new List<AsistenReu>();
    public static List<CargoReu> cargoreus = new List<CargoReu>();
    public static List<StatsAsisDto> StatsAsis = new List<StatsAsisDto>();
    public static List<EquipoEam> equipos = new List<EquipoEam>();



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
    public async Task<ActionResult<List<CargoReuDTO>>> GetAsistencia(string centro, string empresa)
    {

        cargoreus = await _context.CargoReus
            .Where(a => a.Crarea == centro & a.Crempresa == empresa & a.Cresta == true)
            .OrderByDescending(a => a.Crnombre)
            .ToListAsync();

        return Ok(_mapper.Map<List<CargoReuDTO>>(cargoreus));
    }


    [HttpGet("GetKsf")]
    public async Task<ActionResult<List<KsfDTO>>> GetKsf()
    {

        ksfs = await _context.Ksfs
            .Where(a => a.KsfEsta == true)
            .ToListAsync();

        return Ok(_mapper.Map<List<KsfDTO>>(ksfs));
    }

    [HttpGet("GetResponsables")]
    public async Task<ActionResult<List<RespoReuDTO>>> GetRespon()
    {


        resporeu = await _context.RespoReus
            .Where(a => a.Rresta == true)
            .ToListAsync();


        return Ok(_mapper.Map<List<RespoReuDTO>>(resporeu));
    }

    //Obtener asistencia en porcentaje
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

    //Obtener asistencia por dia
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

    [HttpGet("GetEquiposPorCentro/{Centro}")]
    public async Task<ActionResult<List<MaestraVDTO>>> EquiposLineaEAM(string Centro)
    {
        List<MaestraV> data = new List<MaestraV> { };


        data = await _context.MaestraVs
        .Where(x => x.Centro == Centro)
        .Where(x => x.IdEmpresa == x.IdEmpresa)
        .ToListAsync();

        return Ok(_mapper.Map<List<MaestraVDTO>>(data));
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


    //TODO:areglar error de base datos
    // Obtener trabajos pendientes para el calendario
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

}

