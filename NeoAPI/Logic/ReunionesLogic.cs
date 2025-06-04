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
                    .Where(c => c.Cresta && c.IdTipReu == 1)
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
    }
}