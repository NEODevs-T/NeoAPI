using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NeoAPI.Models.Gespline;
using NeoAPI.ModelsDOCIng;
using NeoAPI.DTOs.Asentamientos;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Logic.Global;
using NeoAPI.Controllers.Maestras;
using NeoAPI.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NeoAPI.Controllers.Gespline;


[ApiController]
[Route("api/[controller]")]

public class EntradaejecucionController : ControllerBase
{
    private readonly GesplineContext _context;

    public EntradaejecucionController(GesplineContext context)
    {
        _context = context;
    }

    [HttpGet("MaquinasGesplineActivos1turno")]
    public async Task<List<string>> MaquinasGesplineActivos1turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);//new DateTime(2025,04,22,5,50,0);
        DateTime final = DateTime.Today.AddHours(18);//new DateTime(2025,04,22,18,0,0);
        List<string> listaCodigoProceso = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio &&  e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            listaCodigoProceso.Add(item.CodigotuplaNavigation.Codigoproceso);
        }
        return listaCodigoProceso;
    }

    [HttpGet("MaquinasGesplineActivos2turnoDespues0am")]
    public async Task<List<string>> MaquinasGesplineActivos2turnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);//DateTime.Today.AddHours(5).AddMinutes(50);//new DateTime(2025,04,22,5,50,0);
        DateTime final = DateTime.Today.AddHours(6);//new DateTime(2025,04,22,18,0,0);
        List<string> listaCodigoProceso = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio &&  e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            listaCodigoProceso.Add(item.CodigotuplaNavigation.Codigoproceso);
        }
        return listaCodigoProceso;
    }

    [HttpGet("MaquinasGesplineActivos2turnoAntes0am")]

    public async Task<List<string>> MaquinasGesplineActivos2turnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        List<string> listaCodigoProceso = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();
        
        foreach (var item in listaEjecucion)
        {
            listaCodigoProceso.Add(item.CodigotuplaNavigation.Codigoproceso);
        }

        return listaCodigoProceso;

    }

    [HttpGet("tiempoPerdidoActual1turno")]
    public async Task<List<string>> TiempoPerdidoActual1turno()
    {
    
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        
        DateTime final = DateTime.Today.AddHours(18);
        
        List<string> listaTiempoPerdido = new List<string>();
        
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();
        
        foreach (var item in listaEjecucion)
        {
        
            var codigo = item.CodigotuplaNavigation.Codigoproceso;
        
            string tiempo = item.Timespan.ToString();
        
            listaTiempoPerdido.Add($"{codigo}: {tiempo}");
        
        }
        
        
        return listaTiempoPerdido;
    }

    [HttpGet("tiempoPerdidoActual2turnoAntes0am")]

    public async Task<List<string>> tiempoPerdidoActual2turnoAntes0am()
    {
        
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        
        List<string> listaTiempoPerdido = new List<string>();
        
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
        
            var codigo = item.CodigotuplaNavigation.Codigoproceso;
        
            string tiempo = item.Timespan.ToString();
        
            listaTiempoPerdido.Add($"{codigo}: {tiempo}");
        
        }

        return listaTiempoPerdido;

    }

    [HttpGet("tiempoPerdidoActual2turnoDespues0am")]

    public async Task<List<string>> tiempoPerdidoActual2turnoDespues0am()
    {
        
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);

        DateTime final = DateTime.Today.AddHours(6);

        List<string> listaTiempoPerdido = new List<string>();

        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            
            var codigo = item.CodigotuplaNavigation.Codigoproceso;

            string tiempo = item.Timespan.ToString();

            listaTiempoPerdido.Add($"{codigo}: {tiempo}");
            
        }

        return listaTiempoPerdido;

    }

    [HttpGet ("tiempoEjecutadoActual1")]

    public async Task<List<string>> tiempoEjecutadoActual1()
    {

        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);

        DateTime final = DateTime.Today.AddHours(16);

        List<string> listaTiempoActual = new List<string>();

        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            
            var codigo = item.CodigotuplaNavigation.Codigoproceso;

            string tiempo = item.Horasejecutadas.ToString();

            listaTiempoActual.Add($"{codigo}: {tiempo}");

        }

        return listaTiempoActual;

    }

    [HttpGet ("tiempoEjecutadoActual2turnoAntes0am")]

    public async Task<List<string>> tiempoEjecutadoActual2turnoAntes0am()
    {
        
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        
        DateTime final = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);

        List<string> listaTiempoActual = new List<string>();

        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            var codigo = item.CodigotuplaNavigation.Codigoproceso;

            string tiempo = item.Horasejecutadas.ToString();

            listaTiempoActual.Add($"{codigo}: {tiempo}");
            
        }

        return listaTiempoActual;

    }

    [HttpGet ("tiempoEjecutadoActual2turnoDespues0am")]

    public async Task<List<string>> tiempoEjecutadoActual2turnoDespues0am()
    {

        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);

        DateTime final = DateTime.Today.AddHours(5).AddMinutes(50);

        List<string> listaTiempoActual = new List<string>();

        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            
            var codigo = item.CodigotuplaNavigation.Codigoproceso;

            string tiempo = item.Horasejecutadas.ToString();

            listaTiempoActual.Add($"{codigo}: {tiempo}");
            
        }

        return listaTiempoActual;
        
    }

    [HttpGet("tiempoTrabajadoActual1turno")]
    public async Task<List<string>> TiempoTrabajadoActual1Turno()
    {
    // Se obtienen las listas de tiempos ejecutados y tiempos perdidos usando los métodos ya definidos.
    var listaEjecutado = await tiempoEjecutadoActual1();
    
    var listaPerdido = await TiempoPerdidoActual1turno();

    // Convertimos cada elemento de lista en un objeto anónimo con dos propiedades: Key y Value.
    var ejecutado = listaEjecutado.Select(e => {
        var parts = e.Split(':');
        return new 
        { 
            Key = parts[0].Trim(), 
            Value = float.Parse(parts[1].Trim()) 
        };
    }).ToList();

    var perdido = listaPerdido.Select(p => {
        var parts = p.Split(':');
        return new 
        { 
            Key = parts[0].Trim(), 
            Value = float.Parse(parts[1].Trim()) 
        };
    }).ToList();

    // Usamos LINQ para realizar un left join en base a la Key y calcular el tiempo neto trabajado.
    var resultado = from ej in ejecutado
                    join p in perdido on ej.Key equals p.Key into perdGroup
                    from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                    select $"{ej.Key}: {ej.Value - (perd != null ? perd.Value : 0f)}";

    // Retornamos el resultado como una lista de strings.
    return resultado.ToList();
    
    }

    [HttpGet("tiempoTrabajadoActual2turno")]

    public async Task<List<string>> tiempoTrabajadoActual2turno(bool band)
    {
        
        List<string> listaEjecutado;
        List<string> listaPerdido;

        if (band)
        {
            
            listaEjecutado = await tiempoEjecutadoActual2turnoAntes0am();

            listaPerdido = await tiempoPerdidoActual2turnoAntes0am();

        }
        else
        {

            listaEjecutado = await tiempoEjecutadoActual2turnoDespues0am();

            listaPerdido = await tiempoPerdidoActual2turnoDespues0am();
            
        }

        var ejecutado = listaEjecutado.Select(e =>
        {

            var parts = e.Split(":");
            return new { Key = parts[0].Trim(), Value = float.Parse(parts[1].Trim())};

        }).ToList();

        var perdido = listaPerdido.Select(p =>
        {
            
            var parts = p.Split(":");
            return new { Key = parts[0].Trim(), Value = float.Parse(parts[1].Trim())};

        }).ToList();

        var resultado = (from ej in ejecutado join p in perdido on ej.Key equals p.Key 
                        into perdGroup from perd in perdGroup.DefaultIfEmpty() 
                        select $"{ej.Key}: {ej.Value - (perd != null ? perd.Value : 0f)}").ToList();

        return resultado;    
        
    }

}







