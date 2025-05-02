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

        [HttpGet("TiempoPerdidoActual1turno")]

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

    [HttpGet("tiempoPerdidoActual2turnoDespues0am")]

    public async Task<List<string>> tiempoPerdidoActual2turnoDespues0am()
    {
        
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);

        DateTime final = DateTime.Today.AddHours(6);

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

    [HttpGet ("tiempoEjecutadoActual1")]

    public async Task<List<string>> tiempoEjecutadoActual1()
    {

        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);

        DateTime final = DateTime.Today.AddHours(18);

        List<string> listaTiempoActual = new List<string>();

        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions.Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final).Include(e => e.CodigotuplaNavigation).ToListAsync();

        foreach (var item in listaEjecucion)
        {
            
            var codigo = item.CodigotuplaNavigation.Codigoproceso;

            string tiempo = item.Horasejecutadas.ToString();

            listaTiempoActual.Add($"{codigo}: Tiempo ejecutado total {tiempo} horas");

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

            listaTiempoActual.Add($"{codigo}: Tiempo ejecutado total {tiempo} horas");
            
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

            listaTiempoActual.Add($"{codigo}: Tiempo ejecutado total {tiempo} horas");
            
        }

        return listaTiempoActual;
        
    }

[HttpGet("tiempoTrabajadoActual1turno")]
public async Task<List<string>> TiempoTrabajadoActual1Turno()
{
    // Se obtienen las listas de tiempos ejecutados y tiempos perdidos.
    var listaEjecutado = await tiempoEjecutadoActual1();

    var listaPerdido = await TiempoPerdidoActual1turno();

    // Conversión de la listaEjecutado a objetos con Key y Value
    var ejecutado = listaEjecutado.Select(e => 
    {

        var parts = e.Split(':');

        return new 

        { 

            Key = parts[0].Trim(), 

            Value = float.Parse(

                parts[1].Replace("Tiempo ejecutado total", "")

                        .Replace("horas", "")

                        .Trim())
        };

    }).ToList();

    // Conversión de la listaPerdido a objetos con Key y Value
    var perdido = listaPerdido.Select(p => 
    {

        var parts = p.Split(':');

        return new 
        { 

            Key = parts[0].Trim(), 

            Value = float.Parse(

                parts[1].Replace("Tiempo perdido total", "")

                        .Replace("horas", "")

                        .Trim())
        };
    }).ToList();

    // Left Join usando LINQ y cálculo del tiempo neto con mensaje dinámico
    var resultado = from ej in ejecutado
                    
                    join p in perdido on ej.Key equals p.Key into perdGroup
                    
                    from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                    
                    let tiempoEjecutado = ej.Value
                    
                    let tiempoPerdido = perd != null ? perd.Value : 0f
                    
                    let tiempoNeto = tiempoEjecutado - tiempoPerdido
                    
                    let comentario = tiempoNeto switch
                    
                    {
                    
                        var t when t >= 10f  => "¡Excelente rendimiento!",
                    
                        _                  => "Rendimiento aceptable."
                    }
                    
                    select $"Código {ej.Key} -> Tiempo Ejecutado: {tiempoEjecutado} h, Tiempo Perdido: {tiempoPerdido} h, Tiempo Neto: {tiempoNeto} h. {comentario}";
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
   // Conversión de la listaEjecutado a objetos con Key y Value
    var ejecutado = listaEjecutado.Select(e => 
    {

        var parts = e.Split(':');

        return new 

        { 

            Key = parts[0].Trim(), 

            Value = float.Parse(

                parts[1].Replace("Tiempo ejecutado total", "")

                        .Replace("horas", "")

                        .Trim())
        };

    }).ToList();

    // Conversión de la listaPerdido a objetos con Key y Value
    var perdido = listaPerdido.Select(p => 
    {

        var parts = p.Split(':');

        return new 
        { 

            Key = parts[0].Trim(), 

            Value = float.Parse(

                parts[1].Replace("Tiempo perdido total", "")

                        .Replace("horas", "")

                        .Trim())
        };
    }).ToList();

    // Left Join usando LINQ y cálculo del tiempo neto con mensaje dinámico
    var resultado = from ej in ejecutado
                    
                    join p in perdido on ej.Key equals p.Key into perdGroup
                    
                    from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                    
                    let tiempoEjecutado = ej.Value
                    
                    let tiempoPerdido = perd != null ? perd.Value : 0f
                    
                    let tiempoNeto = tiempoEjecutado - tiempoPerdido
                    
                    let comentario = tiempoNeto switch
                    
                    {
                    
                        var t when t >= 10f  => "¡Excelente rendimiento!",
                    
                        _                  => "Rendimiento aceptable."
                    }
                    
                    select $"Código {ej.Key} -> Tiempo Ejecutado: {tiempoEjecutado} h, Tiempo Perdido: {tiempoPerdido} h, Tiempo Neto: {tiempoNeto} h. {comentario}";
    // Retornamos el resultado como una lista de strings.
    return resultado.ToList();
    }

    // Para Paradasejecutadas, que tienen la propiedad Fechaentrada en la entidad de navegación:
private IQueryable<Paradasejecutada> FiltrarParadasejecutadasPorFecha(IQueryable<Paradasejecutada> query, DateTime inicio, DateTime final)
{
    return query.Where(e => e.CodigoentradaejecucionNavigation.Fechaentrada >= inicio &&
                             e.CodigoentradaejecucionNavigation.Fechaentrada < final);
}

// Y para Paradas, si aún deseas aplicar algún filtro similar, necesitarás determinar 
// cuál es la propiedad que te permita filtrar por fecha. Si no la tienen, quizá debas omitir el filtro.
private IQueryable<Parada> FiltrarParadasPorFecha(IQueryable<Parada> query, DateTime inicio, DateTime final)
{
    // Si Paradas no tiene Fechaentrada, quizás puedas filtrar a través de otra relación.
    // Por ejemplo, si Paradas se relaciona con otra entidad que sí tiene la fecha,
    // podrías hacer un join o un filtro sobre la propiedad de esa entidad.
    // Si no, simplemente retorna el query sin filtrar:
    return query;
}



    [HttpGet("ParadasActuales1turno")]
    public async Task<string> ParadasActuales1turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        
        DateTime final = DateTime.Today.AddHours(18);

        // Consulta para Paradasejecutadas con su filtro
    List<Paradasejecutada> listaParadaEjecutada = await FiltrarParadasejecutadasPorFecha
    (_context.Paradasejecutadas, inicio, final)
    .Include(e => e.Codigoregistrso)
    .ToListAsync();

    // Consulta para Paradas (si aplica algún filtro, en este caso lo dejamos sin cambiar)
    List<Parada> listadaParada = await FiltrarParadasPorFecha
    (_context.Paradas, inicio, final)
    .Include(e => e.Codigogrupoparada)
    .Include(e => e.Nombreparada)
    .ToListAsync();

    }




/*

        // Realizamos la consulta asíncrona usando ToListAsync().
        var query = await (from pe in _context.Paradasejecutadas
        
        join p in _context.Paradas
        on pe.CodigoParada equals p.CodigoParada
        join gp in _context.GruposDeParadas 
        on p.CodigoGrupoParada equals gp.CodigoGrupoParada
        join part in _context.Partes 
        on pe.CodigoParada.Substring(0, 3).ToUpper() equals part.Codigo into partJoin
        from part in partJoin.DefaultIfEmpty()
        where 
        pe.FechaEntrada >= DateTime.Today.Add(new TimeSpan(5, 50, 0)) &&
        pe.FechaEntrada < DateTime.Today.Add(new TimeSpan(18, 0, 0)) &&
        pe.FechaEntrada.Hour < 17 &&
        (p.CodigoParada.Length < 4 || p.CodigoParada.Substring(p.CodigoParada.Length - 4, 4) != "0114") &&
        pe.CodigoProceso == centroCosto
        orderby ((double)pe.TIMESPAN - (double)pe.FECHAYHORAPARADA) * 1440 descending
        select new
        {
            CodRegistro    = pe.CodigoRegistroSO,
            CodGrupo       = gp.CodigoGrupoParada,
            NombreParada   = p.NombreParada,
            TiempoPerdido  = ((double)pe.TIMESPAN - (double)pe.FECHAYHORAPARADA) * 1440,
            ParteNombre    = part != null ? part.ParteNombre : string.Empty,
            CodigoPart     = part != null ? part.Codigo : string.Empty
            }).ToListAsync();

    // Extraemos los datos a listas de cadena.
    List<string> idRegistro = query.Select(r => r.CodRegistro.ToString()).ToList();
    List<string> codigos    = query.Select(r => r.CodGrupo).ToList();
    List<string> parada     = query.Select(r => r.NombreParada).ToList();
    List<string> tiempo     = query.Select(r => r.TiempoPerdido.ToString()).ToList();
    List<string> idArea     = query.Select(r => r.ParteNombre).ToList();
    List<string> Area       = query.Select(r => r.CodigoPart).ToList();

    // Agrupamos las listas en una lista de listas.
    List<List<string>> datos = new List<List<string>>();
    datos.Add(idRegistro);
    datos.Add(codigos);
    datos.Add(parada);
    datos.Add(tiempo);
    datos.Add(idArea);
    datos.Add(Area);

    // Construimos la URI a partir de los datos.
    StringBuilder sb = new StringBuilder("http://example.com/api?");
    for (int i = 0; i < datos.Count; i++)
    {
        // Une los elementos de cada lista separándolos por comas.
        string valores = string.Join(",", datos[i]);
        // Codifica la cadena para que sea segura en la URL.
        string valorCodificado = Uri.EscapeDataString(valores);
        sb.Append($"lista{i}={valorCodificado}&");
    }
    // Elimina el último carácter '&' y retorna la URI.
    string uri = sb.ToString().TrimEnd('&');
    return uri;
}*/


        
}













