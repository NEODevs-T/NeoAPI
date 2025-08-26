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
                DateTime candidate15 = new DateTime(year, month, 15);
                DateTime computedThursday15 = GetClosetThursday(candidate15); // Lógica ya definida previamente
                if (computedThursday15 >= inicio && computedThursday15 <= fin && RegistroExiste(computedThursday15))
                {
                    targetDates.Add(computedThursday15);
                }
                int lastDayOfMonth = DateTime.DaysInMonth(year, month);
                int candidateDay = lastDayOfMonth >= 30 ? 30 : lastDayOfMonth;
                DateTime candidate30Date = new DateTime(year, month, candidateDay);
                DateTime computedThursday30;
                if (RegistroExiste(candidate30Date))
                {
                    computedThursday30 = candidate30Date;
                }
                else
                {
                    if (candidate30Date.DayOfWeek < DayOfWeek.Thursday)
                    {
                        int daysBack = ((int)candidate30Date.DayOfWeek - (int)DayOfWeek.Thursday + 7) % 7;
                        computedThursday30 = candidate30Date.AddDays(-daysBack);
                    }
                    else
                    {
                        computedThursday30 = GetClosetThursday(candidate30Date);
                        if (computedThursday30.Month != month)
                        {
                            int daysBack = ((int)candidate30Date.DayOfWeek - (int)DayOfWeek.Thursday + 7) % 7;
                            computedThursday30 = candidate30Date.AddDays(-daysBack);
                        }
                    }
                    if (!RegistroExiste(computedThursday30))
                    {
                        DateTime tempDate = computedThursday30;
                        while (tempDate <= candidate30Date)
                        {
                            if (RegistroExiste(tempDate))
                            {
                                computedThursday30 = tempDate;
                                break;
                            }
                            tempDate = tempDate.AddDays(1);
                        }
                    }
                }
                if (computedThursday30 >= inicio && computedThursday30 <= fin &&
                    computedThursday30 != computedThursday15 && RegistroExiste(computedThursday30))
                {
                    targetDates.Add(computedThursday30);
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