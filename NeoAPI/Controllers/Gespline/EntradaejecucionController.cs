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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Globalization;

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


    [HttpGet("GetMaquinasGesplineActivos1turno")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos1turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);//new DateTime(2025,04,22,5,50,0);
        DateTime final = DateTime.Today.AddHours(18);//new DateTime(2025,04,22,18,0,0);
        List<string> listaCodigoProceso = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio &&  e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .ToListAsync();

        foreach (var item in listaEjecucion)
        {
            listaCodigoProceso
            .Add(item.CodigotuplaNavigation.Codigoproceso);
        }

        listaCodigoProceso = listaCodigoProceso.Distinct().OrderBy(l => l).ToList();
        return Ok(listaCodigoProceso);
    }

    [HttpGet("GetMaquinasGesplineActivos2turnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos2turnoDespues0am()
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(17).AddMinutes(50);//DateTime.Today.AddHours(5).AddMinutes(50);//new DateTime(2025,04,22,5,50,0);
        DateTime final = DateTime.Today.AddHours(6);//new DateTime(2025,04,22,18,0,0);
        List<string> listaCodigoProceso = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio &&  e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .ToListAsync();

        foreach (var item in listaEjecucion)
        {
            listaCodigoProceso
            .Add(item.CodigotuplaNavigation.Codigoproceso);
        }
        
        listaCodigoProceso = listaCodigoProceso.Distinct().OrderBy(l => l).ToList();
        return Ok(listaCodigoProceso);
    }

    [HttpGet("GetMaquinasGesplineActivos2turnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetMaquinasGesplineActivos2turnoAntes0am()
    {
        DateTime inicio = DateTime.Today.AddHours(17).AddMinutes(50);
        DateTime final = DateTime.Today.AddDays(1).AddHours(6);
        List<string> listaCodigoProceso = new List<string>();
        List<Entradaejecucion> listaEjecucion = await this._context.Entradaejecucions
        .Where(e => e.Fechaentrada >= inicio && e.Fechaentrada < final)
        .Include(e => e.CodigotuplaNavigation)
        .ToListAsync();
        
        foreach (var item in listaEjecucion)
        {
            listaCodigoProceso
            .Add(item.CodigotuplaNavigation.Codigoproceso);
        }
        
        listaCodigoProceso = listaCodigoProceso.Distinct().OrderBy(l => l).ToList();
        return Ok(listaCodigoProceso);

    }

    [HttpGet("GetTiempoPerdidoActual1turno")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual1turno()
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);
        DateTime final = DateTime.Today.AddHours(18);
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
                TiempoPerdido = g.Sum(p => EF.Functions.DateDiffMinute(p.Fechayhoraparada.Value, p.Timespan.Value)) / 60.0f
            })
            .ToListAsync();
    
        List<string> listaTiempoPerdido = tiempoPerdido
        .OrderBy(tp => tp.Codigoproceso)
        .Select(tp =>$"{tp.Codigoproceso}: {tp.TiempoPerdido}")
        .ToList();

        return Ok(listaTiempoPerdido);
    }

    [HttpGet("GetTiempoPerdidoActual2turnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2turnoAntes0am()
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
        
            TiempoPerdido = g.Sum(p => EF.Functions.DateDiffMinute(p.Fechayhoraparada.Value, p.Timespan.Value)) / 60.0f
        })
        .ToListAsync();

        List<string> listaTiempoPerdido = tiempoPerdido
        .OrderBy(tp => tp.Codigoproceso)
        .Select(tp =>$"{tp.Codigoproceso}: {tp.TiempoPerdido}")
        .ToList();

        return Ok(listaTiempoPerdido);
    }
    
    [HttpGet("GetTiempoPerdidoActual2turnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoPerdidoActual2turnoDespues0am()
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
        
            TiempoPerdido = g.Sum(p => EF.Functions.DateDiffMinute(p.Fechayhoraparada.Value, p.Timespan.Value)) / 60.0f
        })
        .ToListAsync();

        List<string> listaTiempoPerdido = tiempoPerdido
        .OrderBy(tp => tp.Codigoproceso)
        .Select(tp =>$"{tp.Codigoproceso}: {tp.TiempoPerdido}")
        .ToList();

        return Ok(listaTiempoPerdido);
    }


    [HttpGet ("GetTiempoEjecutadoActual1Turno")]

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

        return Ok(listaTiempoActual);

    }

    [HttpGet ("GetTiempoEjecutadoActual2turnoAntes0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2turnoAntes0am()
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

        return Ok(listaTiempoActual);

    }

    [HttpGet ("GetTiempoEjecutadoActual2turnoDespues0am")]
    public async Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2turnoDespues0am()
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
            listaTiempoActual.Add($"{codigo}:{tiempo}");
        }

        return Ok(listaTiempoActual);
    }
    
    [HttpGet("GetTiempoTrabajadoActual1turno")]
    public async Task<ActionResult<List<string>>> TiempoTrabajadoActual1Turno()
    {
        var listaEjecutadoR = await GetTiempoEjecutadoActual1Turno();
        List<string> listaEjecutado;
        if (listaEjecutadoR.Result is OkObjectResult okEjecutado 
        && okEjecutado.Value is List<string> dataEjecutado)
        {
            listaEjecutado = dataEjecutado;
        }
        else
        {
            return BadRequest("No se pudo obtener la lista de tiempo ejecutado.");
        }
        var listaPerdidoR = await GetTiempoPerdidoActual1turno();
        List<string> listaPerdido;
        if (listaPerdidoR.Result is OkObjectResult okPerdido 
        && okPerdido.Value is List<string> dataEjecutado2)
        {
            listaPerdido = dataEjecutado2;
        }
        else
        {
            return BadRequest("No se pudo obtener la lista de tiempo perdido.");
        }
        var ejecutado = listaEjecutado.Select(e => 
    {
        var parts = e.Split(':');
        return new 
        { 
            Key = parts[0].Trim(), 
            Value = float.Parse(
                parts[1].Trim(),
                new CultureInfo("es-VE"))
        };
    }).ToList();
    var perdido = listaPerdido.Select(p => 
    {
        var parts = p.Split(':');
        return new 
        { 
            Key = parts[0].Trim(), 
            Value = float.Parse(
                parts[1].Trim(),
                new CultureInfo("es-VE"))
        };
    }).ToList();
    var resultado = from ej in ejecutado
                    join p in perdido on ej.Key equals p.Key into perdGroup
                    from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                    let tiempoEjecutado = ej.Value
                    let tiempoPerdido = perd != null ? perd.Value : 0f
                    let tiempoNeto = tiempoEjecutado - tiempoPerdido
                    select $"{tiempoNeto}";
    return Ok(resultado);
    }
    
    [HttpGet("GetTiempoTrabajadoActual2turno")]
    public async Task<ActionResult<List<string>>> GetTiempoTrabajadoActual2turno(bool band)
    {
        List<string> listaEjecutado;
        List<string> listaPerdido;
        if (band)
        {            
            var listaEjecutadoR = await GetTiempoEjecutadoActual2turnoAntes0am();
            if (listaEjecutadoR.Result is OkObjectResult okEjectudado 
            && okEjectudado.Value is List<string> dataEjecutado)
            {
                listaEjecutado = dataEjecutado;
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de tiempo ejecutado (antes 0 am).");
            }
            var listaPerdidoR = await GetTiempoPerdidoActual2turnoAntes0am();
            if (listaPerdidoR.Result is OkObjectResult okPerdido
            && okPerdido.Value is List<string> dataEjecutado2)
            {
                listaPerdido = dataEjecutado2;
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de tiempo perdido (antes 0 am).");
            }  
        }
        else
        {
            var listaEjecutadoR = await GetTiempoEjecutadoActual2turnoDespues0am();
            if (listaEjecutadoR.Result is OkObjectResult okEjectudado 
            && okEjectudado.Value is List<string> dataEjecutado)
            {
                listaEjecutado = dataEjecutado;
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de tiempo ejecutado (después 0 am).");
            }
            var listaPerdidoR = await GetTiempoPerdidoActual2turnoDespues0am();
            if (listaPerdidoR.Result is OkObjectResult okPerdido
            && okPerdido.Value is List<string> dataEjecutado2)
            {
                listaPerdido = dataEjecutado2;
            }
            else
            {
                return BadRequest("No se pudo obtener la lista de tiempo perdido (después 0 am).");
            }
        }
        var ejecutado = listaEjecutado.Select(e => 
        {
        var parts = e.Split(':');
        return new 
        {
            Key = parts[0].Trim(), 
            Value = float.Parse(
                parts[1].Trim(),
                new CultureInfo("es-VE"))
        };
    }).ToList();
    var perdido = listaPerdido.Select(p => 
    {
        var parts = p.Split(':');
        return new 
        { 
            Key = parts[0].Trim(), 
            Value = float.Parse(
                parts[1].Trim(),
                new CultureInfo("es-VE"))
        };
    }).ToList();
    var resultado = from ej in ejecutado                   
                    join p in perdido on ej.Key equals p.Key into perdGroup
                    from perd in perdGroup.DefaultIfEmpty() // Si no hay coincidencia, perd será null.
                    let tiempoEjecutado = ej.Value
                    let tiempoPerdido = perd != null ? perd.Value : 0f
                    let tiempoNeto = tiempoEjecutado - tiempoPerdido
                    select $"{tiempoNeto}";
    return Ok(resultado);
    }
    
    [HttpGet("GetParadasActuales1Turno")]
    public async Task<ActionResult<List<ParadaActual1TurnoDTO>>> GetParadasActuales1Turno(string centroCosto)
    {
        DateTime inicio = DateTime.Today.AddHours(5).AddMinutes(50);  
        DateTime final  = DateTime.Today.AddHours(18);                 

        var query = from pe in _context.Paradasejecutadas
        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada
        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada
        join part in _context.Partes on pe.Codigoparada
        .Substring(0, 3)
        .ToUpper() equals part.Codigo into partJoin
        from pa in partJoin.DefaultIfEmpty()
        join pr in _context.Tuplaejecucions on pe.CodigoentradaejecucionNavigation.Codigoentradaejecucion
        .ToString() equals pr.Codigoproceso
        where pe.Codigoregistrso != null && 
            p.Nombreparada != null &&
            gp.Codigogrupoparada != null &&
            pe.CodigoentradaejecucionNavigation.Fechaentrada >= inicio &&
            pe.CodigoentradaejecucionNavigation.Fechaentrada < final &&
            pe.CodigoentradaejecucionNavigation.Fechaentrada.HasValue &&
            pe.CodigoentradaejecucionNavigation.Fechaentrada.Value.Hour < 17 &&
            !p.Codigoparada
            .EndsWith("0114") &&
            pr.Codigoproceso == centroCosto
        orderby EF.Functions
        .DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) descending
        select new ParadaActual1TurnoDTO
        {
            CodigoRegistro = pe.Codigoregistrso
            .ToString(),
            CodigoGrupoParada = gp.Codigogrupoparada,
            NombreParada = p.Nombreparada,
            TiempoPerdido = (EF.Functions
            .DateDiffMinute(pe.Fechayhoraparada, pe.Timespan) ?? 0)
            .ToString(),
            ParteNombre = pa != null ? pa.ParteNombre : null,
            CodigoParte = pa != null ? pa.Codigo : null
        };
        var resultado = await query.ToListAsync();
        return Ok(resultado);
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

    [HttpGet("GetParadasActuales2turnoAntesDeLas0amAgrupadas")]

    public async Task<List<ParadasActuales2turnoAntesDeLas0amAgrupadasDTO>> GetParadasActuales2TurnoAntesDeLas0AmAgrupadas(string centroCosto)
    {
        
        DateTime inicio = DateTime.Today.AddHours(18);

        DateTime final = DateTime.Today.AddDays(1).AddHours(6);

        var query =

        from en in _context.Entradaejecucions

        where en.Fechaentrada >= inicio && en.Fechaentrada < final 

        let tupla = en.CodigotuplaNavigation

        from pe in _context.Paradasejecutadas.Where(pe => pe.Codigoentradaejecucion == en.Codigoentradaejecucion)

        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada

        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada

        join a in _context.Areas on p.Codigoparada equals a.AcodGes

        where p.Codigoparada.Length >= 4 &&

            p.Codigoparada.Substring(p.Codigoparada.Length - 4, 4) != "0114" &&
            
            tupla.Codigoproceso == centroCosto
        
        group new {pe, p, gp, a} by new
        {
            
            p.Codigoparada,

            gp.Codigogrupoparada,

            a.AcodGes,

            p.Nombreparada,

            a.Aparte

        }into grp select new ParadasActuales2turnoAntesDeLas0amAgrupadasDTO{

            CodigoParada = grp.Key.Codigoparada,

            CodigoGrupoParada = grp.Key.Codigogrupoparada,

            ACodGes = grp.Key.AcodGes,

            NombreParada = grp.Key.Nombreparada,

            Aparte = grp.Key.Aparte,

            TiempoPerdido = (grp.Sum(x => (x.pe.Timespan.HasValue && x.pe.Fechayhoraparada.HasValue)

            ? (x.pe.Timespan.Value - x.pe.Fechayhoraparada.Value).TotalDays
            
            : 0) * 1440).ToString()

        };

        query = query.OrderByDescending(x => x.TiempoPerdido);

        return await query.ToListAsync();

    }

    [HttpGet ("GetParadasActuales2turnoDespuesDeLas0amAgrupadas")]

    public async Task<List<ParadasActuales2turnoDespuesDeLas0amAgrupadasDTO>> GetParadasActuales2turnoDespuesDeLas0amAgrupadas(string centroCosto)
    {
        DateTime inicio = DateTime.Today.AddDays(-1).AddHours(18);

        DateTime final = DateTime.Today.AddHours(6);

        var query = from en in _context.Entradaejecucions
                    where en.Fechaentrada >= inicio && en.Fechaentrada < final

        let tupla = en.CodigotuplaNavigation

        from pe in _context.Paradasejecutadas.Where(pe => pe.Codigoentradaejecucion == en.Codigoentradaejecucion)

        join p in _context.Paradas on pe.Codigoparada equals p.Codigoparada

        join gp in _context.Gruposdeparadas on p.Codigogrupoparada equals gp.Codigogrupoparada

        join a in _context.Areas on p.Codigoparada equals a.AcodGes

        where p.Codigoparada.Length >= 4 &&

        p.Codigoparada.Substring(p.Codigoparada.Length - 4,4) != "0114" &&

        tupla.Codigoproceso == centroCosto

        group new {pe, p, gp, a}by new
        {
            p.Codigoparada,

            gp.Codigogrupoparada,

            a.AcodGes,

            p.Nombreparada,

            a.Aparte

        }into grp select new ParadasActuales2turnoDespuesDeLas0amAgrupadasDTO{

            CodigoParada = grp.Key.Codigoparada,

            CodigoGrupoParada = grp.Key.Codigogrupoparada,

            ACodGes = grp.Key.AcodGes,

            NombreParada = grp.Key.Nombreparada,

            Aparte = grp.Key.Aparte,

            TiempoPerdido = (grp.Sum(x =>(x.pe.Timespan.HasValue && x.pe.Fechayhoraparada.HasValue)
            
            ?(x.pe.Timespan.Value - x.pe.Fechayhoraparada.Value).TotalDays
            
            : 0)*1440).ToString()

        };

        query = query.OrderByDescending(x => x.TiempoPerdido);

        return await query.ToListAsync();
    }



    [HttpGet("GetParadasActuales2turnoAntesDeLas0am")]

    public async Task<List<ParadasActuales2turnoAntesDeLas0amDTO>> GetParadasActuales2turnoAntesDeLas0am(string centroCosto)
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

        where   pe.Codigoregistrso != null &&
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

    [HttpGet("GetParadasActuales2turno")]
    public async Task<List<List<string>>> GetParadasActuales2turno(string centroCosto)
    {
        DateTime hoy = DateTime.Now;
        
        var resultado = hoy.Hour < 6 
        
        ? (await this.GetParadasActuales2turnoAntesDeLas0am(centroCosto))
        
        .Select(dto => new List<string>
        {
        
            dto.CodigoRegistro, 
        
            dto.CodigoGrupoParada,
        
            dto.NombreParada,
        
            dto.TiempoPerdido,
        
            dto.ParteNombre,
        
            dto.CodigoParte
        
        }).ToList()
        
        : (await this.GetParadasActuales2turnoDespuesDeLas0am(centroCosto))
        .Select(dto => new List<string>
        {
        
            dto.CodigoRegistro, 
        
            dto.CodigoGrupoParada,
        
            dto.NombreParada,
        
            dto.TiempoPerdido,
        
            dto.ParteNombre,
        
            dto.CodigoParte
        
        }).ToList();
        
        return resultado;
        
    }

    [HttpGet("FiltrarDatos")]
    public List<List<string>> FiltrarDatos(List<List<string>> datos, string cadenaIdRegistros)
    {
        string[] filtros = cadenaIdRegistros
            .Replace("[", "")
            .Replace("]", "")
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();

        foreach (var filtro in filtros)
        {
            int index = datos[0].FindIndex(d => d.Contains(filtro));
            if (index >= 0)
            {
                for (int i = 0; i < datos.Count; i++)
                {
                    if (index < datos[i].Count)
                        datos[i].RemoveAt(index);
                }
            }
        }
        return datos;
    }


/*   [HttpGet("GetPrimeraParadaporLinea")]

    public async Task<ActionResult<PrimeraParadaporLineaDTO>> GetPrimeraParadaporLinea()
    {
        DateTime today = DateTime.Now;
        List<string> maquinas;

        if (today.Hour >= 6 && today.Hour < 18)
        {
           // maquinas = await this.MaquinasGesplineActivos1turno();
        }
        else if (today.Hour >= 18 && today.Hour < 24)
        {
            maquinas = await this.MaquinasGesplineActivos2turnoAntes0am();
        }
        else
        {
            maquinas = await this.MaquinasGesplineActivos2turnoDespues0am();
        }

        foreach (string maquina in maquinas)
        {
            var query = await _context.Paradasejecutadas
            .Where(p => p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso == maquina)
            .OrderBy(p => p.Fechayhoraparada)
            .Select(p => new PrimeraParadaporLineaDTO
            {
                CodigoProceso = p.CodigoentradaejecucionNavigation.CodigotuplaNavigation.Codigoproceso,
                FechaYHoraParada = p.Fechayhoraparada,
                Timespan = p.Timespan

            })
            .FirstOrDefaultAsync();

        if(query != null)
        {
            return Ok(query);
        }

        }

        return NotFound("No se encontró información para ninguna máquina");
    }*/

    [HttpGet("GetParadasSegundoTurnoPorMaquina/{centroCosto}")]

    public async Task<IActionResult> GetParadasSegundoTurnoPorMaquina(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");
        }
        
        try
        {
            var paradas = await GetParadasActuales2turno(centroCosto);

            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }

            return Ok(paradas);
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

    [HttpGet("GetParadasGesplienActualesAgrupados1turno/{centroCosto}")]

    public async Task<IActionResult> GetParadasGesplienActualesAgrupados1turno(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");
        }

        try
        {
            var paradas = await GetParadasActuales1TurnoAgrupados(centroCosto);

            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }

            return Ok(paradas);
        }

        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

    [HttpGet("GetParadasGesplienActualesAgrupados2turnoAntesDeLas0am/{centroCosto}")]

    public async Task<IActionResult> GetParadasGesplienActualesAgrupados2turnoAntesDeLas0am(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");
        }

        try
        {
            var paradas = await GetParadasActuales2TurnoAntesDeLas0AmAgrupadas(centroCosto);

            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }

            return Ok(paradas);
        }

        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

    [HttpGet("GetParadasGesplienActualesAgrupados2turnoDespuesDeLas0am/{centroCosto}")]

    public async Task<IActionResult> GetParadasGesplienActualesAgrupados2turnoDespuesDeLas0am(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");            
        }

        try
        {
            var paradas = await GetParadasActuales2turnoDespuesDeLas0amAgrupadas(centroCosto);

            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }

            return Ok(paradas);
        }

        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }
/*
    [HttpGet("GetParadasActuales1turnoPorLinea/{centroCosto}")]

    public async Task<IActionResult> GetParadasActuales1turnoPorLinea(string centroCosto)
    {
        if (string.IsNullOrWhiteSpace(centroCosto))
        {
            return BadRequest("El centro de costo es obligatorio.");
        }

        try 
        {
            var paradas = await GetParadasActuales1Turno(centroCosto);
            
            if (paradas == null || !paradas.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }

            return Ok(paradas);
        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }

    [HttpGet("GetLaPrimeraParadaPorLinea")]

    public async Task<IActionResult> GetLaPrimeraParadaPorLinea()
    {
        try
        {
            var resultado = await Task.Run(() => GetLaPrimeraParadaPorLinea());

            if (resultado == null)
            {
                return NotFound("No se encontró la información solicitada.");
            }

            return Ok(resultado);

        }

        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }
    
    [HttpGet("GetParadasSegundoTurnoPorMaquina/{centroCosto}/{cadenas}")]
    public async Task<IActionResult> GetParadasSegundoTurnoPorMaquina(string centroCosto, string cadenas)
    {
        if (string.IsNullOrWhiteSpace(centroCosto) || string.IsNullOrWhiteSpace(cadenas))
        {
            return BadRequest("El centro de costo y la cadena son obligatorios.");
        }        
        try
        
        {
            var paradasSinFiltro = await Task.Run(() => GetParadasActuales2turno(centroCosto));
            if (paradasSinFiltro == null || !paradasSinFiltro.Any())
            {
                return NotFound("No se encontraron paradas para el centro de costo especificado.");
            }

        
            var paradasFiltradas = await Task.Run(() => FiltrarDatos(paradasSinFiltro, cadenas));
            if (paradasFiltradas == null || !paradasFiltradas.Any())
            {
                return NotFound("No se encontraron paradas filtradas para el centro de costo especificado.");
            }
            
            return Ok(paradasFiltradas);
            }
            catch (Exception ex)
            {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
        }
    }
    
    [HttpGet("GetParadasPrimerTurnoPorMaquina/{centroCosto}/{cadenas}")]

    public async Task<IActionResult> GetParadasPrimerTurnoPorMaquina(string centroCosto, string cadenas)
    {
        if(string.IsNullOrWhiteSpace(centroCosto) || string.IsNullOrWhiteSpace(cadenas))
        {
            return BadRequest("El centro de costo y la cadena son obligaotirios.");
        }
        try
        {
            var paradasFiltradas = await Task.Run(() => GetParadasActuales1Turno(centroCosto));
            if (paradasFiltradas == null || !paradasFiltradas.Any())
            {
                return NotFound("No se encontraron paradas filtradas para el centro de costo especificado.");
            }

            return Ok(paradasFiltradas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ha ocurrido un error en el servidor.");
            }
    }*/

}