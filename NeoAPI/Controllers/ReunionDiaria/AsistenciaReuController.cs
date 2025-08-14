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
using System.Data;


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
            .Where(x => x.Arfecha.Date >= date1 & x.Arfecha.Date <= date2)
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
            .Where(x => (x.Arfecha.Date >= date1 & x.Arfecha.Date <= date2) && x.Ararea == cent && x.IdCargoRNavigation.Crempresa == empresa)
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
            .Where(x => x.Arfecha.Date >= date1 & x.Arfecha.Date <= date2)
            .GroupBy(x => x.IdCargoRNavigation.Crnombre)
            .ToListAsync();

            return Ok(_mapper.Map<List<AsistenReuDTO>>(result));

        }

        else
        {
            var result = await _context.AsistenReus
            .Include(x => x.IdCargoRNavigation)
            .Where(x => (x.Arfecha.Date >= date1 & x.Arfecha.Date <= date2) && x.Ararea == cent && x.IdCargoRNavigation.Crempresa == empresa)
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

    private static DateTime GetEasterSunday(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;
            return new DateTime(year, month, day);
        }

    [HttpGet("GetPorcentajeAsistenciaDiaria")]
    public async Task<ActionResult<PorcentajeAsistenciaDiariaResponseDTO>> GetPorcentajeAsistenciaDiaria(
        string fechaInicio, string fechaFin, string empresa, string area, [FromQuery] List<string> eventosExternos = null)
    {
        try
        {
            string[] partsInicio = fechaInicio.Split('-');
            string[] partsFin = fechaFin.Split('-');
            DateTime inicio = new DateTime(int.Parse(partsInicio[2]), int.Parse(partsInicio[1]), int.Parse(partsInicio[0]));
            DateTime fin = new DateTime(int.Parse(partsFin[2]), int.Parse(partsFin[1]), int.Parse(partsFin[0]));
            if (inicio > fin)
                return BadRequest("La fecha de inicio debe ser anterior o igual a la fecha final.");
            TimeSpan horaReunion = new TimeSpan(9, 0, 0);
            DateTime hoy = DateTime.Now.Date;
            if (fin.Date == hoy && DateTime.Now.TimeOfDay < horaReunion)
            {
                fin = fin.AddDays(-1);
            }
            int totalDias = (fin.Date - inicio.Date).Days + 1;
            var allHolidays = new List<PublicHoliday>();
            for (int year = inicio.Year; year <= fin.Year; year++)
                allHolidays.AddRange(DateSystem.GetPublicHolidays(year, CountryCode.VE));
            var requireHolidayNames = new List<string>
            {
                "Día de Año Nuevo", "Jueves Santo", "Viernes Santo",
                "Nochebuena", "Navidad", "Nochevieja"
            };
            var selectHolidays = allHolidays
                .Where(h => requireHolidayNames.Contains(h.LocalName, StringComparer.OrdinalIgnoreCase)
                            && h.Date >= inicio.Date && h.Date <= fin.Date)
                .Select(h => h.Date)
                .ToList();
            if (eventosExternos != null)
            {
                foreach (var fechaEvento in eventosExternos)
                {
                    string[] partesEvento = fechaEvento.Split('-');
                    DateTime evento = new DateTime(int.Parse(partesEvento[2]), int.Parse(partesEvento[1]), int.Parse(partesEvento[0]));
                    if (evento >= inicio.Date && evento <= fin.Date && !selectHolidays.Contains(evento))
                        selectHolidays.Add(evento);
                }
            }
            for (int year = inicio.Year; year <= fin.Year; year++)
            {
                DateTime easterSunday = GetEasterSunday(year);
                DateTime juevesSanto = easterSunday.AddDays(-3);
                DateTime viernesSanto = easterSunday.AddDays(-2);
                if (juevesSanto >= inicio.Date && juevesSanto <= fin.Date && !selectHolidays.Contains(juevesSanto))
                    selectHolidays.Add(juevesSanto);
                if (viernesSanto >= inicio.Date && viernesSanto <= fin.Date && !selectHolidays.Contains(viernesSanto))
                    selectHolidays.Add(viernesSanto);
            }
            int reunionesProgramadas = Enumerable.Range(0, totalDias)
                .Select(i => inicio.AddDays(i))
                .Count(fecha =>
                    fecha.DayOfWeek != DayOfWeek.Saturday &&
                    fecha.DayOfWeek != DayOfWeek.Sunday &&
                    !selectHolidays.Contains(fecha)
                );
            var cargos = (await _reunionesLogic.GetCargoReuDiaria())
                .Where(c => c.IdTipReu == 1 &&
                            (string.IsNullOrWhiteSpace(area) || c.Centro.Equals(area, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrWhiteSpace(empresa) || c.Empresa.Equals(empresa, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            if (!cargos.Any())
                return StatusCode(500, "Error al obtener datos de los cargos.");
            var cargoIds = cargos.Select(c => c.IdCargoR).ToList();
            var asistencias = (await _reunionesLogic.GetAsisReuDiaria())
                .Where(a => a.Arfecha.Date >= inicio.Date && a.Arfecha.Date <= fin.Date && cargoIds.Contains(a.IdCargoR))
                .ToList();
            var asistenciaPorCargo = asistencias
                .GroupBy(a => new { a.IdCargoR, Fecha = a.Arfecha.Date })
                .GroupBy(g => g.Key.IdCargoR)
                .Select(g => new
                {
                    IdCargoR = g.Key,
                    DiasAsistidos = g.Select(x => x.Key.Fecha).Distinct().Count()
                })
                .ToList();
            var detallePorCargo = cargoIds.Select(id =>
            {
                var asistencia = asistenciaPorCargo.FirstOrDefault(x => x.IdCargoR == id);
                int diasAsistidos = asistencia?.DiasAsistidos ?? 0;
                double porcentaje = reunionesProgramadas > 0 ? ((double)diasAsistidos / reunionesProgramadas) * 100 : 0;
                string nombre = cargos.FirstOrDefault(c => c.IdCargoR == id)?.Crnombre ?? string.Empty;
                return new AsistenReuPorcetanjeDTO
                {
                    IdCargoR = id,
                    Nombre = nombre,
                    ReunionesProgramadas = reunionesProgramadas,
                    ReunionesAsistidas = diasAsistidos,
                    PorcentajeAsistencia = porcentaje
                };
            }).OrderByDescending(x => x.IdCargoR).ToList();
            int totalAsistenciasGlobal = asistenciaPorCargo.Sum(x => x.DiasAsistidos);
            int totalReunionesEsperadas = cargoIds.Count * reunionesProgramadas;
            double porcentajeGlobal = totalReunionesEsperadas > 0 ? ((double)totalAsistenciasGlobal / totalReunionesEsperadas) * 100 : 0;
            return Ok(new PorcentajeAsistenciaDiariaResponseDTO
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


    [HttpGet("GetPorcentajeAsistenciaTurno")]
    public async Task<ActionResult<PorcentajeAsistenciaDiariaResponseDTO>> GetPorcentajeAsistenciaTurno(
    string fechaInicio,
    string fechaFin,
    string empresa,
    string area,
    bool diasExcepcionalesLaborables = false,
    [FromQuery] List<string> eventosExternos = null)
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
                return BadRequest("La fecha de inicio debe ser anterior o igual a la fecha final.");
            }
            int totalDias = (fin.Date - inicio.Date).Days + 1;
            var allHolidays = new List<PublicHoliday>();
            for (int year = inicio.Year; year <= fin.Year; year++)
            {
                allHolidays.AddRange(DateSystem.GetPublicHolidays(year, CountryCode.VE));
            }
            var requireHolidayNames = new List<string>
        {
            "Día de Año Nuevo", "Navidad"
        };
            List<DateTime> fechasEventosExternos = new List<DateTime>();
            if (eventosExternos != null)
            {
                foreach (var fechaEvento in eventosExternos)
                {
                    string[] partesEvento = fechaEvento.Split('-');
                    DateTime evento = new DateTime(
                        int.Parse(partesEvento[2]),
                        int.Parse(partesEvento[1]),
                        int.Parse(partesEvento[0])
                    );
                    if (evento >= inicio.Date && evento <= fin.Date)
                    {
                        fechasEventosExternos.Add(evento);
                    }
                }
            }
            List<DateTime> selectHolidays;
            if (diasExcepcionalesLaborables)
            {
                selectHolidays = new List<DateTime>();
            }
            else
            {
                selectHolidays = allHolidays
                    .Where(h => requireHolidayNames.Contains(h.LocalName, StringComparer.OrdinalIgnoreCase)
                                && h.Date >= inicio.Date && h.Date <= fin.Date)
                    .Select(h => h.Date)
                    .ToList();
                if (fechasEventosExternos.Any())
                {
                    selectHolidays = selectHolidays.Union(fechasEventosExternos).ToList();
                }
            }
            int horaActual = DateTime.Now.Hour;
            DateTime hoy = DateTime.Today;
            int reunionesProgramadas = 0;
            for (int i = 0; i < totalDias; i++)
            {
                DateTime fecha = inicio.AddDays(i);
            if (!selectHolidays.Any(feriado => feriado.Date == fecha.Date))
            {
                if (empresa == "PANASA" || empresa == "PAINSA" || empresa == "CHEMPRO")
                {
                    if (fecha.Date == hoy)
                    {
                        if (horaActual >= 6 && horaActual < 14)
                        {
                            reunionesProgramadas += 1; 
                        }
                        else if (horaActual >= 14 && horaActual < 22)
                        {
                            reunionesProgramadas += 2; 
                        }
                        else
                        {
                            reunionesProgramadas += 3; 
                        }
                    }
                    else
                    {
                        reunionesProgramadas += 3;
                    }
                }
                else
                {
                    if (fecha.Date == hoy)
                    {
                        if (horaActual >= 6 && horaActual < 18)
                        {
                            reunionesProgramadas += 1;
                        }
                        else
                        {
                            reunionesProgramadas += 2;
                        }
                    }
                    else
                    {
                        reunionesProgramadas += 2;
                    }
                }
            }
            }    
            List<CarReuDTO> cargos = await _reunionesLogic.GetCargoReuDiaria();
            if (cargos == null)
            {
                return StatusCode(500, "Error al obtener datos de los cargos.");
            }
            cargos = cargos
                .Where(c => c.IdTipReu == 2 &&
                            (string.IsNullOrWhiteSpace(area) || c.Centro.Equals(area, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrWhiteSpace(empresa) || c.Empresa.Equals(empresa, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            var cargoIds = cargos.Select(c => c.IdCargoR).ToList();
            List<AsistenReuDTO> asistencias = await _reunionesLogic.GetAsisReuDiaria();
            if (asistencias == null)
            {
                return StatusCode(500, "Error al obtener datos de la asistencia.");
            }
            var asistenciasFiltradas = asistencias
                .Where(a =>
                            a.Arfecha.Date >= inicio.Date &&
                            a.Arfecha.Date <= fin.Date &&
                            (diasExcepcionalesLaborables || !selectHolidays.Contains(a.Arfecha.Date)) &&
                            cargoIds.Contains(a.IdCargoR))
                .ToList();
            var asistenciaPorCargo = asistenciasFiltradas
                .GroupBy(a => new { a.IdCargoR, Dia = a.Arfecha.Date })
                .Select(g => new { g.Key.IdCargoR, Count = g.Count() })
                .GroupBy(x => x.IdCargoR)
                .Select(g => new
                {
                    IdCargoR = g.Key,
                    ReunionesAsistidas = g.Sum(x => Math.Min(x.Count, 2))
                })
                .ToList();
            var detallePorCargo = cargoIds.Select(id =>
            {
                int reunionesAsistidas = asistenciaPorCargo.FirstOrDefault(x => x.IdCargoR == id)?.ReunionesAsistidas ?? 0;
                double porcentaje = reunionesProgramadas > 0 ?
                    ((double)reunionesAsistidas / reunionesProgramadas) * 100 : 0;
                string nombre = cargos.FirstOrDefault(c => c.IdCargoR == id)?.Crnombre ?? string.Empty;
                return new AsistenReuPorcetanjeDTO
                {
                    IdCargoR = id,
                    Nombre = nombre,
                    ReunionesProgramadas = reunionesProgramadas,
                    ReunionesAsistidas = reunionesAsistidas,
                    PorcentajeAsistencia = porcentaje
                };
            })
            .OrderByDescending(x => x.IdCargoR)
            .ToList();
            int totalAsistenciasGlobal = asistenciaPorCargo.Sum(x => x.ReunionesAsistidas);
            int totalReunionesEsperadas = cargoIds.Count() * reunionesProgramadas;
            double porcentajeGlobal = totalReunionesEsperadas > 0 ?
                ((double)totalAsistenciasGlobal / totalReunionesEsperadas) * 100 : 0;
            return Ok(new PorcentajeAsistenciaDiariaResponseDTO
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

    [HttpGet("GetPorcentajeAsistenciaQuincenal")]
    public async Task<ActionResult<PorcentajeAsistenciaDiariaResponseDTO>> GetPorcentajeAsistenciaMensual(
    string fechaMesAño, string empresa, string area)
    {
        try
        {
            string[] parts = fechaMesAño.Split('-'); // formato esperado: "MM-YYYY"
            int mes = int.Parse(parts[0]);
            int año = int.Parse(parts[1]);

            DateTime inicio = new DateTime(año, mes, 1);
            DateTime fin = new DateTime(año, mes, DateTime.DaysInMonth(año, mes));

            if (inicio > fin)
            {
                return BadRequest("La fecha de inicio debe ser anterior o igual a la fecha final.");
            }
            int totalDias = (fin.Date - inicio.Date).Days + 1;
            var meetingDays = _reunionesLogic.ObtenerJuevesObjetivo(inicio, fin);
            int reunionesProgramadas = meetingDays.Count;
            List<CarReuDTO> cargos = await _reunionesLogic.GetCargoReuDiaria();
            if (cargos == null)
            {
                return StatusCode(500, "Error al obtener datos de los cargos.");
            }
            cargos = cargos
                .Where(c => c.IdTipReu == 3 &&
                            (string.IsNullOrWhiteSpace(area) || c.Centro.Equals(area, StringComparison.OrdinalIgnoreCase)) &&
                            (string.IsNullOrWhiteSpace(empresa) || c.Empresa.Equals(empresa, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            List<AsistenReuDTO> asistencias = await _reunionesLogic.GetAsisReuDiaria();
            if (asistencias == null)
            {
                return StatusCode(500, "Error al obtener datos de la asistencia.");
            }
            var meetingDaysSet = meetingDays.Select(md => md.Date).ToHashSet();
            var cargoIds = cargos.Select(c => c.IdCargoR).ToList();
            var asistenciasFiltradas = asistencias
                .Where(a => meetingDaysSet.Contains(a.Arfecha.Date) && cargoIds.Contains(a.IdCargoR))
                .ToList();
            var agrupadasPorDia = asistenciasFiltradas
                .GroupBy(a => new { a.IdCargoR, Dia = a.Arfecha.Date })
                .Select(g => new { g.Key.IdCargoR })
                .ToList();
            var asistenciaPorCargo = agrupadasPorDia
                .GroupBy(x => x.IdCargoR)
                .Select(g => new
                {
                    IdCargoR = g.Key,
                    DiasAsistidos = g.Count()
                })
                .ToList();
            var detallePorCargo = cargoIds.Select(id =>
            {
                int diasAsistidos = asistenciaPorCargo.FirstOrDefault(x => x.IdCargoR == id)?.DiasAsistidos ?? 0;
                double porcentaje = reunionesProgramadas > 0 ?
                    ((double)diasAsistidos / reunionesProgramadas) * 100 : 0;
                string nombre = cargos.FirstOrDefault(c => c.IdCargoR == id)?.Crnombre ?? string.Empty;
                return new AsistenReuPorcetanjeDTO
                {
                    IdCargoR = id,
                    Nombre = nombre,
                    ReunionesProgramadas = reunionesProgramadas,
                    ReunionesAsistidas = diasAsistidos,
                    PorcentajeAsistencia = porcentaje
                };
            })
            .OrderByDescending(x => x.IdCargoR)
            .ToList();
            int totalAsistenciasGlobal = asistenciaPorCargo.Sum(x => x.DiasAsistidos);
            int totalReunionesEsperadas = cargoIds.Count() * reunionesProgramadas;
            double porcentajeGlobal = totalReunionesEsperadas > 0 ?
                ((double)totalAsistenciasGlobal / totalReunionesEsperadas) * 100 : 0;
            return Ok(new PorcentajeAsistenciaDiariaResponseDTO
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

