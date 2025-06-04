using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.ReunionDia;
using NeoAPI.Interface;
namespace NeoAPI.Controllers.AsistenciaReuControllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciaReuController : ControllerBase
{

    private readonly DbNeoIiContext _context;
    private readonly IMapper _mapper;
    private readonly IReunionesLogic _reunionesLogic;

    public AsistenciaReuController(DbNeoIiContext context, IMapper mapper, IReunionesLogic reunionesLogic)
    {
        _context = context;
        _mapper = mapper;
        _reunionesLogic = reunionesLogic;
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
    public async Task<ActionResult<List<CargoReuDTO>>> GetCargoReuDiaria()
    {
        var cargos = await _reunionesLogic.GetCargoReuDiaria();
        return Ok(cargos);
    }

    [HttpGet("GetAsisReuDiaria")]
    public async Task<ActionResult<List<AsistenReuDTO>>> GetAsisReuDiaria()
    {
        var asistencia = await _reunionesLogic.GetAsisReuDiaria();
        return Ok(asistencia);
    }
    
    [HttpGet("GetPorcentajeAsistencia")]
    public async Task<ActionResult<object>> GetPorcentajeAsistencia(string fechaInicio, string fechaFin)
    {
        try
        {
            string[] partsInicio = fechaInicio.Split('-');
            string[] partsFin = fechaFin.Split('-');
            DateTime inicio = new DateTime(
                int.Parse(partsInicio[2]),
                int.Parse(partsInicio[1]),
                int.Parse(partsInicio[0])
            );
            DateTime fin = new DateTime(
                int.Parse(partsFin[2]),
                int.Parse(partsFin[1]),
                int.Parse(partsFin[0])
            );
            if (inicio > fin)
            {
                return BadRequest("La fecha de inicio debe ser anterior o igual a la fecha fin.");
            }
            int totalDias = (fin.Date - inicio.Date).Days + 1;
            List<CargoReuDTO> cargos = await _reunionesLogic.GetCargoReuDiaria();
            if (cargos == null)
            {
                return StatusCode(500, "Error al obtener datos de los cargos.");
            }
            List<AsistenReuDTO> asistencias = await _reunionesLogic.GetAsisReuDiaria();
            if (asistencias == null)
            {
                return StatusCode(500, "Error al obtener datos de la asistencia.");
            }
            var asistenciasFiltradas = asistencias
                .Where(a => a.Arfecha.HasValue &&
                            a.Arfecha.Value.Date >= inicio.Date &&
                            a.Arfecha.Value.Date <= fin.Date)
                .ToList();
            var agrupadasPorDia = asistenciasFiltradas
                .GroupBy(a => new { a.IdCargoR, Dia = a.Arfecha.Value.Date })
                .Select(g => new { g.Key.IdCargoR, g.Key.Dia })
                .ToList();
            var asistenciaPorCargo = agrupadasPorDia
                .GroupBy(x => x.IdCargoR)
                .Select(g => new
                {
                    IdCargoR = g.Key,
                    DiasAsistidos = g.Count()
                })
                .ToList();
            var unionCargoIds = cargos.Select(c => c.IdCargoR)
                                        .Union(asistenciaPorCargo.Select(a => a.IdCargoR))
                                        .Distinct();
            var detallePorCargo = unionCargoIds.Select(id =>
            {
                int diasAsistidos = asistenciaPorCargo.FirstOrDefault(x => x.IdCargoR == id)?.DiasAsistidos ?? 0;
                double porcentaje = ((double)diasAsistidos / totalDias) * 100;
                return new
                {
                    IdCargoR = id,
                    ReunionesProgramadas = totalDias,
                    ReunionesAsistidas = diasAsistidos,
                    PorcentajeAsistencia = porcentaje
                };
            }).ToList();
            int totalAsistenciasGlobal = asistenciaPorCargo.Sum(x => x.DiasAsistidos);
            int totalReunionesEsperadas = unionCargoIds.Count() * totalDias;
            double porcentajeGlobal = totalReunionesEsperadas > 0 ?
                ((double)totalAsistenciasGlobal / totalReunionesEsperadas) * 100 : 0;
            return Ok(new
            {
                PorcentajeGlobal = porcentajeGlobal,
                DetallePorCargo = detallePorCargo
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}

