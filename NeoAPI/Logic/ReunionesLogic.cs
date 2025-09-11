using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Interface;
using System.Text.Json;
using AutoMapper;
using NeoAPI.Models.Neo;
using NeoAPI.DTOs.LibroNovedades;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Logic.ReunionDia;

namespace NeoAPI.Logic.Reuniones
{
    public class ReunionesLogic : IReunionesLogic
    {
        private readonly DbNeoIiContext _context;
        public ReunionesLogic(DbNeoIiContext context)
        {
            _context = context;
        }
        public async Task<List<CarReuDTO>> GetCargoReuDiaria()
        {
            try
            {
                var cargosRaw = await (
                    from e in _context.Empresas
                    join cr in _context.CargoReus on e.Enombre equals cr.Crempresa into crGroup
                    from cr in crGroup.DefaultIfEmpty()
                    join c in _context.Centros on cr.Crarea equals c.Cnom into cGroup
                    from c in cGroup.DefaultIfEmpty()
                    select new CarReuDTO
                    {
                        IdEmpresa = e.IdEmpresa,
                        Empresa = e.Enombre,
                        IdCargoR = cr != null ? cr.IdCargoR : 0,
                        Centro = c != null ? c.Cnom : null,
                        Crnombre = cr != null ? cr.Crnombre : null,
                        Cresta = cr != null ? cr.Cresta : false,
                        IdTipReu = cr != null ? cr.IdTipReu : 0
                    }
                ).ToListAsync();
                var cargosFinal = cargosRaw
                    .OrderBy(x => x.IdEmpresa)
                    .ToList();
                return cargosFinal;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error interno del servidor en GetCargoReuDiariaAsync: {ex.Message}", ex);
            }
        }



        public async Task<List<AsistenReuDTO>> GetAsisReuDiaria()
        {
            try
            {
                var asistentes = await _context.AsistenReus
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
                    })
                    .ToListAsync();
                return asistentes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error interno del servidor en GetAsisReuDiariaAsync: {ex.Message}", ex);
            }
        }

        public DateTime GetClosetThursday(DateTime candidate)
        {
            if (candidate.DayOfWeek == DayOfWeek.Thursday)
                return candidate;
            int bestOffset = 0;
            int bestDiff = int.MaxValue;
            for (int offset = -6; offset <= 6; offset++)
            {
                DateTime testDay = candidate.AddDays(offset);
                if (testDay.DayOfWeek == DayOfWeek.Thursday)
                {
                    int diff = Math.Abs(offset);
                    if (diff < bestDiff)
                    {
                        bestDiff = diff;
                        bestOffset = offset;
                    }
                    else if (diff == bestDiff && offset < bestOffset)
                    {
                        bestOffset = offset;
                    }
                }
            }
            return candidate.AddDays(bestOffset);
        }
    public List<DateTime> ObtenerJuevesObjetivo(DateTime inicio, DateTime fin)
    {
        List<DateTime> targetDates = new List<DateTime>();
        DateTime currentMonth = new DateTime(inicio.Year, inicio.Month, 1);
        while (currentMonth <= fin)
        {
            int year = currentMonth.Year;
            int month = currentMonth.Month;
            // -------------------------------
            // PRIMERA REUNIÓN (quincenal)
            // -------------------------------
            DateTime candidate15 = new DateTime(year, month, 15);
            DateTime computedThursday15;
            if (candidate15.DayOfWeek == DayOfWeek.Monday || candidate15.DayOfWeek == DayOfWeek.Tuesday)
            {
                int daysToThursday = ((int)DayOfWeek.Thursday - (int)candidate15.DayOfWeek + 7) % 7;
                computedThursday15 = candidate15.AddDays(daysToThursday);
            }
            else
            {
                computedThursday15 = GetClosetThursday(candidate15);
                if (computedThursday15 < candidate15)
                {
                    computedThursday15 = candidate15;
                }
            }
            if (computedThursday15 >= inicio && computedThursday15 <= fin && RegistroExiste(computedThursday15))
            {
                targetDates.Add(computedThursday15);
            }
            // -------------------------------
            // SEGUNDA REUNIÓN (fin de mes)
            // -------------------------------
            int lastDayOfMonth = DateTime.DaysInMonth(year, month);
            List<int> posiblesDias = new List<int> { 31, 30, 29, 28 };
            DateTime computedThursdayEnd = DateTime.MinValue;
            foreach (int dia in posiblesDias)
            {
                if (dia <= lastDayOfMonth)
                {
                    DateTime candidateDate = new DateTime(year, month, dia);
                    DateTime thursday = GetClosetThursday(candidateDate);
                    if (thursday.Month == month && thursday <= new DateTime(year, month, lastDayOfMonth))
                    {
                        computedThursdayEnd = thursday;
                        break;
                    }
                }
            }
            // Si no se encontró un jueves válido, usar el último día del mes si hay registro
            if (computedThursdayEnd == DateTime.MinValue)
            {
                DateTime fallbackDate = new DateTime(year, month, lastDayOfMonth);
                if (RegistroExiste(fallbackDate))
                {
                    computedThursdayEnd = fallbackDate;
                }
            }
            if (computedThursdayEnd != DateTime.MinValue &&
                computedThursdayEnd >= inicio && computedThursdayEnd <= fin &&
                computedThursdayEnd != computedThursday15 &&
                RegistroExiste(computedThursdayEnd))
            {
                targetDates.Add(computedThursdayEnd);
            }
            currentMonth = currentMonth.AddMonths(1);
        }
        return targetDates;
    }

    public bool RegistroExiste(DateTime fecha)
        {
            return true;
        }
    }
}