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
using Nager.Date;
using Nager.Date.Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


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
            var allHolidays = DateSystem.GetPublicHolidays(inicio.Year, CountryCode.VE);
            var requireHolidayNames = new List<string>
            {"Día de Año Nuevo","Carnaval","Jueves Santo","Viernes Santo"
            ,"Diez y nueve de abril","Día del Trabajador","Día de San Juan Bautista y aniversario de la Batalla de Carabobo","Cinco de julio"
            ,"Natalicio del Libertador, Dia de la Armada Nacional","Día de la Resistencia Indígena","Nochebuena","Navidad","Nochevieja"
            };
            var selectHolidays = allHolidays
            .Where(h => requireHolidayNames.Contains(h.LocalName, StringComparer.OrdinalIgnoreCase)
            && h.Date >= inicio.Date && h.Date <= fin.Date)
            .Select(h => h.Date)
            .ToList();
            DateTime juevesSanto = new DateTime(inicio.Year, 4, 17);
            DateTime viernesSanto = new DateTime(inicio.Year, 4, 18);
            if (!selectHolidays.Any(d => d.Date == juevesSanto.Date))
            {
                selectHolidays.Add(juevesSanto.Date);
            }
            if (!selectHolidays.Any(d => d.Date == viernesSanto.Date))
            {
                selectHolidays.Add(viernesSanto.Date);
            }
            int reunionesProgramadas = Enumerable.Range(0, totalDias)
            .Select(i => inicio.AddDays(i))
            .Count(fecha =>
                fecha.DayOfWeek != DayOfWeek.Saturday &&
                fecha.DayOfWeek != DayOfWeek.Sunday &&
                !selectHolidays.Any(feriado => feriado == fecha)
            );
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
                double porcentaje = reunionesProgramadas > 0 ?
                    ((double)diasAsistidos / reunionesProgramadas) * 100 : 0;
                return new
                {
                    IdCargoR = id,
                    ReunionesProgramadas = reunionesProgramadas,
                    ReunionesAsistidas = diasAsistidos,
                    PorcentajeAsistencia = porcentaje
                };
            }).ToList();
            int totalAsistenciasGlobal = asistenciaPorCargo.Sum(x => x.DiasAsistidos);
            int totalReunionesEsperadas = unionCargoIds.Count() * reunionesProgramadas;
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

     [HttpGet("FeriadosDeVenezuela2025ConSemanaSanta")]
    public ActionResult<IEnumerable<string>> FeriadosDeVenezuela2025ConSemanaSanta()
    {
        int year = 2025;
        // Obtiene la lista de feriados para Venezuela usando Nager.Date versión 1.30.0
        var feriados = DateSystem.GetPublicHolidays(year, CountryCode.VE).ToList();

        // Para 2025, se asume que el Domingo de Pascua cae el 20 de abril,
        // por lo que se derivan:
        var juevesSanto = new DateTime(year, 4, 17); // Jueves Santo
        var viernesSanto = new DateTime(year, 4, 18); // Viernes Santo

        // Agrega Jueves Santo si no está ya en la lista
        if (!feriados.Any(h => h.Date.Date == juevesSanto))
        {
            // El constructor puede variar; en esta versión se usa:
            feriados.Add(new PublicHoliday(juevesSanto, "Jueves Santo", "Holy Thursday", CountryCode.VE, null));
        }

        // Agrega Viernes Santo si no está ya en la lista
        if (!feriados.Any(h => h.Date.Date == viernesSanto))
        {
            feriados.Add(new PublicHoliday(viernesSanto, "Viernes Santo", "Good Friday", CountryCode.VE, null));
        }

        // Ordena la lista por fecha (opcional)
        var resultado = feriados
            .OrderBy(h => h.Date)
            .Select(h => $"{h.Date.ToShortDateString()} - {h.LocalName}");

        return Ok(resultado);
    }
}

