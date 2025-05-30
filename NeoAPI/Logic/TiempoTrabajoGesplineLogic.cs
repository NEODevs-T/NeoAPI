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
using System.Globalization;

namespace NeoAPI.Logic.TiempoTrabajoGespline
{
    public class TiempoTrabajoGesplineLogic : ITiempoTrabajoGesplineLogic
    {
        private readonly GesplineContext _context;

        public TiempoTrabajoGesplineLogic(GesplineContext context)
        {
            _context = context;
        }

        
    [HttpGet("GetTiempoPerdidoActual1Turno")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual1Turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
        List<Entradaejecucion> listaEjecucion = await _context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .ToListAsync();
            var tiempoPerdido = await _context.Paradasejecutadas
                .Where(e => e.Timespan != null && e.Fechayhoraparada != null
                        && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio
                        && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
                .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
                .Select(g => new
                {
                    Codigoproceso = g.Key,
                    TiempoPerdido = g.Sum(p => EF.Functions.DateDiffSecond(p.Fechayhoraparada.Value, p.Timespan.Value)) / 3600.0f 
                })
                .ToListAsync();
            List<string> listaTiempoPerdido = tiempoPerdido
            .OrderBy(tp => tp.Codigoproceso)
            .Select(tp => $"{tp.Codigoproceso}: {tp.TiempoPerdido}")
            .ToList();
            return listaTiempoPerdido;
    }

    [HttpGet("GetTiempoPerdidoActual2TurnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2TurnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddDays(1).AddHours(23).AddMinutes(59).AddSeconds(59);
            List<Entradaejecucion> listaEjecucion = await _context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .ToListAsync();
            var tiempoPerdido = await _context.Paradasejecutadas
            .Where(e => e.Timespan != null && e.Fechayhoraparada != null
                    && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio
                    && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
            .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
            .Select(g => new
            {
                Codigoproceso = g.Key,
                TiempoPerdido = g.Sum(p => EF.Functions.DateDiffSecond(p.Fechayhoraparada.Value, p.Timespan.Value)) / 3600.0f
            })
            .ToListAsync();
            List<string> listaTiempoPerdido = tiempoPerdido
            .OrderBy(tp => tp.Codigoproceso)
            .Select(tp => $"{tp.Codigoproceso}: {tp.TiempoPerdido}")
            .ToList();
            return listaTiempoPerdido;
    }

    [HttpGet("GetTiempoPerdidoActual2TurnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2TurnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(6);
            List<Entradaejecucion> listaEjecucion = await _context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .ToListAsync();
            var tiempoPerdido = await _context.Paradasejecutadas
            .Where(e => e.Timespan != null && e.Fechayhoraparada != null
                    && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio
                    && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
            .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
            .Select(g => new
            {
                Codigoproceso = g.Key,
                TiempoPerdido = g.Sum(p => EF.Functions.DateDiffSecond(p.Fechayhoraparada.Value, p.Timespan.Value)) / 3600.0f
            })
            .ToListAsync();
            List<string> listaTiempoPerdido = tiempoPerdido
            .OrderBy(tp => tp.Codigoproceso)
            .Select(tp => $"{tp.Codigoproceso}: {tp.TiempoPerdido}")
            .ToList();
            return listaTiempoPerdido;
    }


    [HttpGet("GetTiempoEjecutadoActual1Turno")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual1Turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
            List<string> listaTiempoActual = new List<string>();
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .OrderBy(e => e.CodigotuplaNavigation.Codigoproceso)
            .ToListAsync();
            foreach (var item in listaEjecucion)
            {
                var codigo = item.CodigotuplaNavigation.Codigoproceso;
                string tiempo = item.Horasejecutadas.ToString();
                listaTiempoActual.Add($"{codigo}: {tiempo}");
            }
            return listaTiempoActual;
    }

    [HttpGet("GetTiempoEjecutadoActual2TurnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2TurnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<string> listaTiempoActual = new List<string>();
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .OrderBy(e => e.CodigotuplaNavigation.Codigoproceso)
            .ToListAsync();
            foreach (var item in listaEjecucion)
            {
                var codigo = item.CodigotuplaNavigation.Codigoproceso;
                string tiempo = item.Horasejecutadas.ToString();
                listaTiempoActual.Add($"{codigo}: {tiempo}");
            }
            return listaTiempoActual;
    }

    [HttpGet("GetTiempoEjecutadoActual2TurnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2TurnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(5).AddMinutes(50);
            List<string> listaTiempoActual = new List<string>();
            List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
            .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
            .Include(e => e.CodigotuplaNavigation)
            .OrderBy(e => e.CodigotuplaNavigation.Codigoproceso)
            .ToListAsync();
            foreach (var item in listaEjecucion)
            {
                var codigo = item.CodigotuplaNavigation.Codigoproceso;
                string tiempo = item.Horasejecutadas.ToString();
                listaTiempoActual.Add($"{codigo}: {tiempo}");
            }
            return listaTiempoActual;

    }
        [HttpGet("GetTiempoTrabajadoActual1Turno")]
        public async Task<ActionResult<List<string>>> GetTiempoTrabajadoActual1Turno()
        {
            var listaEjecutadoR = await GetTiempoEjecutadoActual1Turno();
            List<string> listaEjecutado = listaEjecutadoR.Value;
            var listaPerdidoR = await GetTiempoPerdidoActual1Turno();
            List<string> listaPerdido = listaPerdidoR.Value;
            var ejecutado = listaEjecutado.Select(e =>
            {
                var parts = e.Split(':');
                return new
                {
                    Key = parts[0].Trim(),
                    Value = float.Parse(parts[1].Trim(), new CultureInfo("es-VE"))
                };
            }).ToList();
            var perdido = listaPerdido.Select(p =>
            {
                var parts = p.Split(':');
                return new
                {
                    Key = parts[0].Trim(),
                    Value = float.Parse(parts[1].Trim(), new CultureInfo("es-VE"))
                };
            }).ToList();
            var resultado = (from ej in ejecutado
                            join p in perdido on ej.Key equals p.Key into perdGroup
                            from perd in perdGroup.DefaultIfEmpty()
                            let tiempoEjecutado = ej.Value
                            let tiempoPerdido = perd != null ? perd.Value : 0f
                            let tiempoNeto = tiempoEjecutado - tiempoPerdido
                            select tiempoNeto.ToString("F16", CultureInfo.InvariantCulture))
                            .ToList();
            return resultado;
        }


        [HttpGet("GetTiempoTrabajadoActual2Turno")]
        public async Task<ActionResult<List<string>>> GetTiempoTrabajadoActual2Turno(bool band)
        {
            List<string> listaEjecutado;
            List<string> listaPerdido;

            if (band)
            {
                listaEjecutado = (await GetTiempoEjecutadoActual2TurnoAntes0am()).Value;
                listaPerdido = (await GetTiempoPerdidoActual2TurnoAntes0am()).Value;
            }
            else
            {
                listaEjecutado = (await GetTiempoEjecutadoActual2TurnoDespues0am()).Value;
                listaPerdido = (await GetTiempoPerdidoActual2TurnoDespues0am()).Value;
            }

            var ejecutado = listaEjecutado.Select(e =>
            {
                var parts = e.Split(':');
                return new
                {
                    Key = parts[0].Trim(),
                    Value = float.Parse(parts[1].Trim(), new CultureInfo("es-VE"))
                };
            }).ToList();

            var perdido = listaPerdido.Select(p =>
            {
                var parts = p.Split(':');
                return new
                {
                    Key = parts[0].Trim(),
                    Value = float.Parse(parts[1].Trim(), new CultureInfo("es-VE"))
                };
            }).ToList();

            var resultado = (from ej in ejecutado
                            join p in perdido on ej.Key equals p.Key into perdGroup
                            from perd in perdGroup.DefaultIfEmpty()
                            let tiempoEjecutado = ej.Value
                            let tiempoPerdido = perd != null ? perd.Value : 0f
                            let tiempoNeto = tiempoEjecutado - tiempoPerdido
                            select tiempoNeto.ToString("F16", CultureInfo.InvariantCulture))
                            .ToList();

            return resultado;
        }

    }
}