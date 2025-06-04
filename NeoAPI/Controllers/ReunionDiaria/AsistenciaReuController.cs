using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.ReunionDia;
namespace NeoAPI.Controllers.AsistenciaReuControllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciaReuController : ControllerBase
{

    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;

    public AsistenciaReuController(DbNeoIiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
                    insertar.IdCargoR = list[i].Cargo.IdCargoR;
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

    [HttpGet("GetStatsAsis/{cent}/{Fecha_inicio}/{Fecha_Final}")]
    public async Task<ActionResult<List<StatsAsisDto>>> GetStatsAsis(string cent, string empresa, string Fecha_inicio, string Fecha_Final)
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

            var statsAsisDto = result.Select(a => new StatsAsisDto
            {
                Cargo = a.Key,
                Asistencias = a.Sum(b => b.ArAsistente)
            });


            // return Ok(carStDTO);
            return Ok(statsAsisDto);
        }

        else
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => (x.Arfecha.Value.Date >= date1 & x.Arfecha.Value.Date <= date2) && x.Ararea == cent && x.IdCargoRNavigation.Crempresa == empresa)
            .GroupBy(x => x.IdCargoRNavigation.Crnombre)
            .ToListAsync();

            var statsAsisDto = result.Select(a => new StatsAsisDto
            {
                Cargo = a.Key,
                Asistencias = a.Sum(b => b.ArAsistente)
            });


            // return Ok(carStDTO);
            return Ok(statsAsisDto);

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
    [HttpGet("GetCargoReuDiaria")]
    public async Task<ActionResult<List<CargReuDTO>>> GetCargoReuDiaria()
    {
        try
        {
            List<CargReuDTO> cargoreus = await _context.CargoReus
                .Where(c => c.Cresta == true
                        && c.IdTipReu == 1)
                .OrderByDescending(c => c.Crnombre)
                .Select(c => new CargReuDTO
                {
                    Crnombre = c.Crnombre,
                    Cresta = c.Cresta,
                    Crempresa = c.Crempresa,
                    Crarea = c.Crarea,
                    IdTipReu = c.IdTipReu
                })
                .ToListAsync();
            return Ok(cargoreus);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("GetAsisReuDiaria")]
    public async Task<ActionResult<List<AsistenReuDTO>>> GetAsisReuDiaria()
    {
        try
        {
            List<AsistenReuDTO> asisreu = await _context.AsistenReus
            .OrderByDescending(a => a.Ararea)
            .Select(a => new AsistenReuDTO
            {
                Ararea = a.Ararea,
                IdAsistencia = a.IdAsistencia,
                Arfecha = a.Arfecha,
                IdCargoR = a.IdCargoR,
                ArAsistente = a.ArAsistente,
                ArSuplente = a.ArSuplente,
                ArObser = a.ArObser,
                ArIdEmpresa = a.ArIdEmpresa
            }).ToListAsync();
        return Ok(asisreu);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        } 
    }
}