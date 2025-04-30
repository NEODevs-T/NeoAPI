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

namespace NeoAPI.Controllers.Gespline;


[ApiController]
[Route("api/[controller]")]

public class ParadasejecutadaController : ControllerBase
{
    private readonly GesplineContext _context;

    public ParadasejecutadaController(GesplineContext context)
    {
        _context = context;
    }

    public async Task<List<string>> TiempoPerdidoActual1turno()
    {
        // Define el inicio del turno: hoy a las 05:50 AM.
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
    
        // Define el final del turno: hoy a las 06:00 PM (18:00).
        DateTime final = DateTime.Today.AddHours(18);
    
        // Obtiene la lista de registros de Entradaejecucion cuyo campo Fechaentrada se encuentre dentro del rango [inicio, final).
        // Se incluye la propiedad de navegación CodigotuplaNavigation para poder acceder a los datos del proceso.
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .ToListAsync();
    
            // Consulta la tabla Paradasejecutadas:
            // - Filtra solo aquellos registros que tienen valores válidos en Timespan y Fechayhoraparada.
            // - Además, filtra aquellos registros cuyos registros de Entradaejecucion asociados (a través de CodigoentradaejecucionNavigation) 
            //   tienen un Fechaentrada dentro del mismo rango [inicio, final).
        var tiempoPerdido = await this._context.Paradasejecutadas
            .Where(e => e.Timespan != null && e.Fechayhoraparada != null 
                    && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio 
                    && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
            // Agrupa los registros por el código del proceso, accediendo a él desde la relación: 
            // CodigoentradaejecucionNavigation → CodigotuplaNavigation → Codigoproceso.
            .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
            // Para cada grupo se crea un objeto anónimo que contiene:
            // - Codigoproceso: El código del proceso (clave del grupo).
            // - TiempoPerdido: La suma de las diferencias (en horas) entre Timespan y Fechayhoraparada para todos los registros en ese grupo.
            .Select(g => new
            {
                Codigoproceso = g.Key, 
                TiempoPerdido = g.Sum(p => (float)((p.Timespan.Value - p.Fechayhoraparada.Value).TotalHours))
            })
            .ToListAsync();
    
        // Inicializa una lista de cadenas para almacenar la salida final.
        List<string> listaTiempoPerdido = new List<string>();

        // Recorre cada registro de Entradaejecucion obtenido anteriormente.
        foreach (var item in listaEjecucion)
        {
            // Extrae el código del proceso a partir de la propiedad de navegación.
            var codigo = item.CodigotuplaNavigation.Codigoproceso;
        
            // Busca en el resultado agrupado el objeto que tenga el mismo código de proceso.
            var tiempogrupo = tiempoPerdido.FirstOrDefault(x => x.Codigoproceso == codigo);
        
            // Si se encuentra el grupo se asigna el tiempo perdido, de lo contrario se asigna 0.
            float tiempo = tiempogrupo != null ? tiempogrupo.TiempoPerdido : 0f;
        
            // Agrega a la lista una cadena formateada con el código del proceso y el tiempo perdido total.
            listaTiempoPerdido.Add($"{codigo}: Tiempo perdido total {tiempo} horas");
        }
    
        // Retorna la lista de cadenas con el resumen del tiempo perdido para cada proceso.
        return listaTiempoPerdido;
    }

    [HttpGet("tiempoPerdidoActual2turnoAntes0am")]

    public async Task<List<string>> tiempoPerdidoActual2turnoAntes0am()
    {
        
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        
        .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
        
        .Include(e => e.CodigotuplaNavigation)
        
        .ToListAsync();

        var tiempoPerdido = await this._context.Paradasejecutadas
        
        .Where(e => e.Timespan != null && e.Fechayhoraparada != null 
        
        && e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio 
        
        && e.CodigoentradaejecucionNavigation.Fechaentrada < final)
        
        .GroupBy(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso)
        
        .Select(g => new
        
        {
            Codigoproceso = g.Key, 
        
            TiempoPerdido = g.Sum(p => (float)((p.Timespan.Value - p.Fechayhoraparada.Value).TotalHours))
        })
        
        .ToListAsync();

        List<string> listaTiempoPerdido = new List<string>();

        foreach (var item in listaEjecucion)
        {
        
            var codigo = item.CodigotuplaNavigation.Codigoproceso;
            
            var tiempogrupo = tiempoPerdido.FirstOrDefault(x => x.Codigoproceso == codigo);
            
            float tiempo = tiempogrupo != null ? tiempogrupo.TiempoPerdido : 0f;

            listaTiempoPerdido.Add($"{codigo}: Tiempo perdido total {tiempo} horas");
        
        }

        return listaTiempoPerdido;

    }


}   
