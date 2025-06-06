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
        public async Task<List<CargoReuDTO>> GetCargoReuDiaria()
        {
            try
            {
                var cargos = await _context.CargoReus
                    .Where(c => c.Cresta)
                    .OrderByDescending(c => c.Crnombre)
                    .Select(c => new CargoReuDTO
                    {
                        IdCargoR = c.IdCargoR,
                        Crnombre = c.Crnombre,
                        Cresta = c.Cresta,
                        Crempresa = c.Crempresa,
                        Crarea = c.Crarea,
                        IdTipReu = c.IdTipReu
                    })
                    .ToListAsync();

                return cargos;
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

        public List<DateTime> ObtenerJuevesObjetivo(DateTime inicio, DateTime final)
        {
            List<DateTime> targetThursdays = new List<DateTime>();
            DateTime currentMonth = new DateTime(inicio.Year, inicio.Month, 1);
            while (currentMonth <= final)
            {
                int year = currentMonth.Year;
                int month = currentMonth.Month;
                DateTime candidate15 = new DateTime(year, month, 15);
                DateTime closestThursday15 = GetClosetThursday(candidate15);
                if (closestThursday15 >= inicio && closestThursday15 <= final)
                {
                    targetThursdays.Add(closestThursday15);
                }
                int candidate30 = DateTime.DaysInMonth(year, month) >= 30
                                    ? 30
                                    : DateTime.DaysInMonth(year, month);
                DateTime cadidate30 = new DateTime(year, month, candidate30);
                DateTime closetThursday30 = GetClosetThursday(cadidate30);
                if (closetThursday30 >= inicio && closetThursday30 <= final &&
                closetThursday30 != closestThursday15)
                {
                    targetThursdays.Add(closetThursday30);
                }

                currentMonth = currentMonth.AddMonths(1);
            }
            return targetThursdays;
        }
    }
}