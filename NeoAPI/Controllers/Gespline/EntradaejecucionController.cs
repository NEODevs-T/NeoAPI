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
using NeoAPI.DTOs.Gespline;
using System.Text.RegularExpressions;

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
    
    [HttpGet("GetParadasActuales1Turno")]
    // Este atributo indica que el método responderá a solicitudes HTTP GET en la ruta "GetParadasActuales1Turno".
    public async Task<List<ParadaActual1TurnoDTO>> GetParadasActuales1Turno(string centroCosto)
    // Declara un método asíncrono público que retorna una lista de objetos del tipo ParadasejecutadaDTO.
    // Recibe como parámetro un string (centroCosto), que se usará para filtrar los registros según el centro de costo.
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);  
        // Se define la variable "inicio" que representa el inicio del turno, que se calcula tomando la fecha actual a medianoche (DateTime.Today)
        // y sumándole 5 horas y 50 minutos para obtener las 05:50 del día actual.
    
        DateTime final  = DateTime.Today.AddHours(18);                 
        // Se define la variable "final" que representa el fin del turno, calculada como la fecha actual con 18 horas añadidas,
        // lo que equivale a las 16:00 del mismo día.

        var query =
        // Se declara la variable "query" para almacenar la consulta LINQ que se construirá a continuación.

        from pe in _context.Paradasejecutadas
        // Se inicia la consulta obteniendo cada registro (alias "pe") de la entidad Paradasejecutadas del contexto de datos.

        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada
        // Se realiza una unión (inner join) entre Paradasejecutadas y Paradas, relacionando ambos mediante el campo Codigoparada.
        // Esto asegura que por cada registro en Paradasejecutadas se recupere el registro correspondiente en Paradas.

        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada
        // Se une la entidad Gruposdeparadas a la consulta usando el campo Codigogrupoparada proveniente de la entidad Paradas,
        // obteniendo así información del grupo al que pertenece cada parada.

        join part in _context.Partes
            on pe.Codigoparada.Substring(0, 3).ToUpper() equals part.Codigo into partJoin
        // Se intenta unir la entidad Partes con Paradasejecutadas: se toma la subcadena de los primeros 3 caracteres de Codigoparada
        // (convertidos a mayúsculas) y se compara con el campo Codigo de Partes.
        // El resultado se agrupa en "partJoin", ya que puede no existir coincidencia (por ello se hará un left join en el siguiente paso).

        from pa in partJoin.DefaultIfEmpty() // left join, para Parte (opcional)        
        // Se realiza un left join sobre el grupo "partJoin": si no se encuentra ningún registro en Partes que coincida, pa será null.
        // Esto permite que la información procedente de Partes sea opcional y no descartar el registro principal.

        // AÑADIMOS EL JOIN CON LA ENTIDAD PROCESO:
        join pr in _context.Procesos 
            on pe.CodigoentradaejecucionNavigation.Codigoentradaejecucion.ToString() equals pr.Codigoproceso
        // Se une la entidad Procesos (alias pr) para relacionar el proceso asociado al registro.
        // Se accede a la propiedad de navegación CodigoentradaejecucionNavigation (que representa la relación con la entrada de ejecución)
        // y se toma el valor de Codigoentradaejecucion, el cual se convierte a string y se compara con el campo Codigoproceso en Procesos.
        // Esto sirve para aplicar el filtro por centro de costo.

        where pe.Codigoregistrso != null && 
            p.Nombreparada != null &&
            gp.Codigogrupoparada != null &&
        // Se aplican filtros iniciales para asegurar que los campos obligatorios no sean nulos:
        //   - Codigoregistrso (identificador en Paradasejecutadas),
        //   - Nombreparada en Paradas,
        //   - Codigogrupoparada en Gruposdeparadas.

          // Filtrar por Fechaentrada (de Entradaejecucion)
            pe.CodigoentradaejecucionNavigation.Fechaentrada >= inicio &&
            pe.CodigoentradaejecucionNavigation.Fechaentrada < final &&
        // Se filtra por la fecha de entrada (obtenida desde la entidad relacionada a través de CodigoentradaejecucionNavigation)
        // para que se encuentre entre el inicio (05:50) y el final (16:00) del turno.

            pe.CodigoentradaejecucionNavigation.Fechaentrada.HasValue &&
        // Se verifica que la propiedad Fechaentrada tenga un valor (sea distinta de null).

            pe.CodigoentradaejecucionNavigation.Fechaentrada.Value.Hour < 17 &&
        // Se aplica un filtro adicional para que la hora de Fechaentrada sea menor a 17 (lo que cumple la condición de DATENAME(HOUR) < 17 en el SQL original).

            !p.Codigoparada.EndsWith("0114") &&
        // Se descarta cualquier registro en el que el Codigoparada de la entidad Paradas termine en "0114".

          // Filtro del centro de costo usando la entidad proceso:
            pr.Codigoproceso == centroCosto
        // Se aplica el filtro del centro de costo: se seleccionan solo aquellos registros cuyo proceso (proporcionado por pr.Codigoproceso)
         // es igual al valor recibido en el parámetro centroCosto.

        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
          // Se ordena el resultado de la consulta de forma descendente en función del "Tiempo Perdido".
         // EF.Functions.DateDiffMinute calcula la diferencia en minutos entre Fechayhoraparada y Timespan para cada registro.

        select new ParadaActual1TurnoDTO
        {
            CodigoRegistro = pe.Codigoregistrso.ToString(),
            // Se asigna al DTO el valor de Codigoregistrso convertido a string.

            CodigoGrupoParada = gp.Codigogrupoparada,
            // Se asigna el valor del grupo de paradas obtenido (Codigogrupoparada).

            NombreParada = p.Nombreparada,
            // Se asigna el nombre de la parada obtenido de la entidad Paradas.

            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0).ToString(),
            // Se calcula el tiempo perdido usando EF.Functions.DateDiffMinute. Si el resultado es null, se asigna 0.

            ParteNombre = pa != null ? pa.ParteNombre : null,
            // Se asigna el nombre de la parte (si existe, de lo contrario se asigna null).

            CodigoParte = pa != null ? pa.Codigo : null
            // Se asigna el código de la parte (si existe, de lo contrario se asigna null).
        };
        // Fin de la consulta LINQ; se ha construido la proyección a ParadasejecutadaDTO.

        var resultado = await query.ToListAsync();
        // Se ejecuta la consulta de forma asíncrona y se transforma el resultado en una lista de ParadasejecutadaDTO.

        return resultado;
        // Se devuelve la lista resultante.
    }
    
    
    [HttpGet("GetParadasActuales1TurnoAgrupados")]
    
    public async Task<List<ParadaActual1TurnoAgrupadoDTO>> GetParadasActuales1TurnoAgrupados(string centroCosto)
    {

        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
    
        DateTime final  = DateTime.Today.AddHours(18);

    var query =
        
        from en in _context.Entradaejecucions
        
        where en.Fechaentrada >= inicio && en.Fechaentrada < final
        
        let tupla = en.CodigotuplaNavigation
        
        from pe in _context.Paradasejecutadas
        
                    .Where(pe => pe.Codigoentradaejecucion == en.Codigoentradaejecucion)
        
        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada
        
        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada
        
        join a in _context.Areas on p.Codigoparada equals a.AcodGes
        
        where p.Codigoparada.Length >= 4 && 
                p.Codigoparada.Substring(p.Codigoparada.Length - 4, 4) != "0114" &&
                tupla.Codigoproceso == centroCosto
        group new { pe, p, gp, a } by new
        {
    
            p.Codigoparada,
    
            gp.Codigogrupoparada,
    
            a.AcodGes,
    
            p.Nombreparada,
    
            a.Aparte
        } into grp
    select new ParadaActual1TurnoAgrupadoDTO
    {
    
    CodigoParada = grp.Key.Codigoparada,
    
    CodigoGrupoParada = grp.Key.Codigogrupoparada,
    
    ACodGes = grp.Key.AcodGes,
    
    NombreParada = grp.Key.Nombreparada,
    
    Aparte = grp.Key.Aparte, // Usamos "Aparte" igual que en el group key.
    
    TiempoPerdido = (grp.Sum(x => 
    
        (x.pe.Timespan.HasValue && x.pe.Fechayhoraparada.HasValue 
    
            ? (x.pe.Timespan.Value - x.pe.Fechayhoraparada.Value).TotalDays 
    
            : 0) * 1440)).ToString()
            
    };
        // OJO: Usar el nombre exacto definido en el DTO, respetando mayúsculas/minúsculas
        query = query.OrderByDescending(x => x.TiempoPerdido);  

        return await query.ToListAsync();
    }

    [HttpGet("GetParadasActuales2turnoAntesDeLas0am")]

    public async Task<List<ParadasActuales2turnoAntesDeLas0amDTO>> GetParadasActuales2TurnoAntesDeLas0s(string centroCosto)
    {
        DateTime inicio = DateTime.Today.AddHours(18);
        
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);

        var query =
        
        from pe in _context.Paradasejecutadas

        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada

        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada

        join part in _context.Partes on pe.Codigoparada.Substring(0, 3).ToUpper() equals part.Codigo into partJoin

        from pa in partJoin.DefaultIfEmpty()

        join pr in _context.Procesos on pe.CodigoentradaejecucionNavigation.Codigoentradaejecucion.ToString() equals pr.Codigoproceso

        where 
        
        pe.Codigoregistrso != null &&

        p.Nombreparada != null &&
        
        gp.Codigogrupoparada != null &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada >= inicio &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada < final &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada.HasValue &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada.Value.Hour >= 17 &&

        !p.Codigoparada.EndsWith("0114") &&

        pr.Codigoproceso == centroCosto

        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending

        select new ParadasActuales2turnoAntesDeLas0amDTO
        {
            CodigoRegistro = pe.Codigoregistrso.ToString(),

            CodigoGrupoParada = gp.Codigogrupoparada,

            NombreParada = p.Nombreparada,

            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0).ToString(),

            ParteNombre = pa != null ? pa.ParteNombre : null,

            CodigoParte = pa != null ? pa.Codigo : null,
        };

        var resultado = await query.ToListAsync();

        return resultado; 

    }

    [HttpGet ("GetParadasActuales2turnoDespuesDeLas0am")]

    public async Task<List<ParadasActuales2turnoDespuesDeLas0amDTO>> GetParadasActuales2turnoDespuesDeLas0am(string centroCosto)
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(18);

        DateTime final = DateTime.Today.AddHours(6);

        var query =

        from pe in _context.Paradasejecutadas

        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada

        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada

        join part in _context.Partes on pe.Codigoparada.Substring(0, 3).ToUpper() equals part.Codigo into partJoin

        from pa in partJoin.DefaultIfEmpty()

        join pr in _context.Procesos on pe.CodigoentradaejecucionNavigation.Codigoentradaejecucion.ToString() equals pr.Codigoproceso

        where 

        pe.Codigoregistrso != null &&

        p.Nombreparada != null &&

        gp.Codigogrupoparada != null &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada >= inicio &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada < final &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada.HasValue &&

        pe.CodigoentradaejecucionNavigation.Fechaentrada.Value.Hour >= 17 &&

        !p.Codigoparada.EndsWith("0114") &&

        pr.Codigoproceso == centroCosto

        orderby EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending

        select new ParadasActuales2turnoDespuesDeLas0amDTO
        {

            CodigoRegistro = pe.Codigoregistrso.ToString(),

            CodigoGrupoParada = gp.Codigogrupoparada,

            NombreParada = p.Nombreparada,

            TiempoPerdido = (EF.Functions.DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0).ToString(),

            ParteNombre = pa != null ? pa.ParteNombre : null,

            CodigoParte = pa != null ? pa.Codigo : null,

        };

        var resultado = await query.ToListAsync();

        return resultado;
    }



}