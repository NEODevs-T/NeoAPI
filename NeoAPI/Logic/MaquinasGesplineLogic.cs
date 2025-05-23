using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models.Neo;
using NeoAPI.Models.Gespline;
using NeoAPI.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoAPI.Logic.MaquinasGespline
{
    public class MaquinasGesplineLogic : IMaquinasGesplineLogic
    {
        private readonly GesplineContext _context;

        public MaquinasGesplineLogic(GesplineContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetMaquinasGesplineActivos1turno()
        {
            DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
            DateTime final = DateTime.Today.AddHours(18);
            try
            {
                List<Entradaejecucion> listaEjecucion = await _context.Entradaejecucions
                    .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
                    .Include(e => e.CodigotuplaNavigation)
                    .ToListAsync();

                if (!listaEjecucion.Any())
                {
                    return new List<string>();
                }

                List<string> listaCodigoProceso = listaEjecucion
                    .Select(e => e.CodigotuplaNavigation.Codigoproceso)
                    .Distinct()
                    .OrderBy(l => l)
                    .ToList();

                return listaCodigoProceso;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<string>> GetMaquinasGesplineActivos2turnoDespues0am()
        {
            DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);
            DateTime final = DateTime.Today.AddHours(6);
            try
            {
                List<Entradaejecucion> listaEjecucion = await _context.Entradaejecucions
                    .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
                    .Include(e => e.CodigotuplaNavigation)
                    .ToListAsync();

                if (!listaEjecucion.Any())
                {
                    return new List<string>();
                }

                List<string> listaCodigoProceso = listaEjecucion
                    .Select(e => e.CodigotuplaNavigation.Codigoproceso)
                    .Distinct()
                    .OrderBy(l => l)
                    .ToList();

                return listaCodigoProceso;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<List<string>> GetMaquinasGesplineActivos2turnoAntes0am()
        {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        try
        {
                List<Entradaejecucion> listaEjecucion = await _context.Entradaejecucions
                    .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
                    .Include(e => e.CodigotuplaNavigation)
                    .ToListAsync();

                if (!listaEjecucion.Any())
                {
                    return new List<string>();
                }

                List<string> listaCodigoProceso = listaEjecucion
                    .Select(e => e.CodigotuplaNavigation.Codigoproceso)
                    .Distinct()
                    .OrderBy(l => l)
                    .ToList();

                return listaCodigoProceso;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}