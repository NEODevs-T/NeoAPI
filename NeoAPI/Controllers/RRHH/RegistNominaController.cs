using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.RRHH;
using NeoAPI.RRHHModels;
using NeoAPI.Models.RRHHModels;

namespace NeoAPI.Controllers.RRHH;

[ApiController]
[Route("api/[controller]")]
public class RegistNominaController : ControllerBase
{
    private readonly DbRegistNominaContext _context;

    public RegistNominaController(DbRegistNominaContext context)
    {
        _context = context;
    }

        [HttpPut("cambiar-estado")]
            public async Task<IActionResult> CambiarEstado(
                string ficha,
                int anio,
                int periodo,
                string tpnom,
                bool stareg)
            {
                var registros = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    UPDATE dbo.RegistNomina
                    SET STAREG = {stareg}
                    WHERE FICHNH = {ficha}
                    AND AÑOHNH = {anio}
                    AND PRDHNH = {periodo}
                    AND TPNOM = {tpnom}");

                return Ok(new
                {
                    RegistrosActualizados = registros,
                    Estado = stareg
                });
            }

    // resto de métodos...

    // ======================================
    // CONSULTA DETALLADA
    // ======================================

    [HttpGet]
    public async Task<IActionResult> GetRegistros(
        string? ciahnh,
        string? tpnhnh,
        int? anio,
        int? periodo,
        string? ficha,
        string? departamento,
        string? tpnom,
        int page = 1,
        int pageSize = 5000)
    {
        bool consultaPorPeriodo =
            anio.HasValue &&
            periodo.HasValue;

        bool consultaPorFicha =
            !string.IsNullOrWhiteSpace(ficha);

        if (!consultaPorPeriodo &&
            !consultaPorFicha)
        {
            return BadRequest(new
            {
                Mensaje = "Debe indicar Año y Período o una Ficha."
            });
        }

        var query = _context.RegistNominas
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(ciahnh))
        {
            query = query.Where(x =>
                (x.Ciahnh ?? "").Trim() == ciahnh.Trim());
        }

        if (!string.IsNullOrWhiteSpace(tpnhnh))
        {
            query = query.Where(x =>
                (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
        }

        if (consultaPorPeriodo)
        {
            query = query.Where(x =>
                x.Añohnh == anio &&
                x.Prdhnh == periodo);
        }

        if (consultaPorFicha)
        {
            ficha = ficha!.Trim();

            query = query.Where(x =>
                (x.Fichnh ?? "").Trim() == ficha);
        }

        if (!string.IsNullOrWhiteSpace(departamento))
        {
            query = query.Where(x =>
                (x.Dpthnh ?? "").Trim() ==
                departamento.Trim());
        }

        if (!string.IsNullOrWhiteSpace(tpnom))
        {
            tpnom = tpnom.Trim().ToUpper();

            query = query.Where(x =>
                (x.Tpnom ?? "").Trim().ToUpper() ==
                tpnom);
        }

        var sw = Stopwatch.StartNew();

        var totalRegistros =
            await query.CountAsync();

        var result = await query
            .OrderByDescending(x => x.Fecregt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        sw.Stop();

        if (!result.Any())
        {
            return NotFound("No se encontraron registros.");
        }

        return Ok(new
        {
            TotalRegistros = totalRegistros,
            PaginaActual = page,
            TamanioPagina = pageSize,
            TotalPaginas =
                (int)Math.Ceiling(
                    (double)totalRegistros / pageSize),
            TiempoMs = sw.ElapsedMilliseconds,
            Data = result
        });
    }

    // ======================================
    // CREAR REGISTRO
    // ======================================

    [HttpPost]
    public async Task<IActionResult> CrearRegistro(
        [FromBody] RegistNominaCreateDTO model)
    {
        model.Tpnom =
            (model.Tpnom ?? "")
            .Trim()
            .ToUpper();

        if (model.Tpnom != "D" &&
            model.Tpnom != "M")
        {
            return BadRequest(new
            {
                Mensaje = "TPNOM solo admite D o M."
            });
        }

        var existe =
            await _context.RegistNominas.AnyAsync(x =>
                x.Fichnh == model.Fichnh &&
                x.Añohnh == model.Añohnh &&
                x.Prdhnh == model.Prdhnh &&
                x.Tpnom == model.Tpnom);

        if (existe)
        {
            return BadRequest(new
            {
                Mensaje =
                    "Ya existe un registro para esa ficha, año, período y tipo de nómina."
            });
        }

        if (model.Tpnom == "D")
        {
            if (!string.IsNullOrWhiteSpace(model.Dg08hh) ||
                !string.IsNullOrWhiteSpace(model.Dg09hh) ||
                !string.IsNullOrWhiteSpace(model.Dg010hh) ||
                !string.IsNullOrWhiteSpace(model.Dg011hh) ||
                !string.IsNullOrWhiteSpace(model.Dg012hh) ||
                !string.IsNullOrWhiteSpace(model.Dg013hh) ||
                !string.IsNullOrWhiteSpace(model.Dg014hh) ||
                !string.IsNullOrWhiteSpace(model.Dg015hh))
            {
                return BadRequest(new
                {
                    Mensaje =
                        "Para Nómina Diaria solo puede registrar DG01HH a DG07HH."
                });
            }
        }

        if (model.Tpnom == "M")
        {
            if (string.IsNullOrWhiteSpace(model.Dg01hh) ||
                string.IsNullOrWhiteSpace(model.Dg02hh) ||
                string.IsNullOrWhiteSpace(model.Dg03hh) ||
                string.IsNullOrWhiteSpace(model.Dg04hh) ||
                string.IsNullOrWhiteSpace(model.Dg05hh) ||
                string.IsNullOrWhiteSpace(model.Dg06hh) ||
                string.IsNullOrWhiteSpace(model.Dg07hh) ||
                string.IsNullOrWhiteSpace(model.Dg08hh) ||
                string.IsNullOrWhiteSpace(model.Dg09hh) ||
                string.IsNullOrWhiteSpace(model.Dg010hh) ||
                string.IsNullOrWhiteSpace(model.Dg011hh) ||
                string.IsNullOrWhiteSpace(model.Dg012hh) ||
                string.IsNullOrWhiteSpace(model.Dg013hh) ||
                string.IsNullOrWhiteSpace(model.Dg014hh) ||
                string.IsNullOrWhiteSpace(model.Dg015hh))
            {
                return BadRequest(new
                {
                    Mensaje =
                        "Para Nómina Mensual debe llenar DG01HH a DG15HH."
                });
            }
        }

        var registro = new RegistNomina
        {
            Ciahnh = model.Ciahnh.Trim(),
            Tpnhnh = model.Tpnhnh.Trim(),
            Añohnh = model.Añohnh,
            Prdhnh = model.Prdhnh,
            Fichnh = model.Fichnh.Trim(),
            Dpthnh = model.Dpthnh.Trim(),

            Dg01hh = model.Dg01hh?.Trim().ToUpper(),
            Dg02hh = model.Dg02hh?.Trim().ToUpper(),
            Dg03hh = model.Dg03hh?.Trim().ToUpper(),
            Dg04hh = model.Dg04hh?.Trim().ToUpper(),
            Dg05hh = model.Dg05hh?.Trim().ToUpper(),
            Dg06hh = model.Dg06hh?.Trim().ToUpper(),
            Dg07hh = model.Dg07hh?.Trim().ToUpper(),

            Dg08hh = model.Dg08hh?.Trim().ToUpper(),
            Dg09hh = model.Dg09hh?.Trim().ToUpper(),
            Dg010hh = model.Dg010hh?.Trim().ToUpper(),
            Dg011hh = model.Dg011hh?.Trim().ToUpper(),
            Dg012hh = model.Dg012hh?.Trim().ToUpper(),
            Dg013hh = model.Dg013hh?.Trim().ToUpper(),
            Dg014hh = model.Dg014hh?.Trim().ToUpper(),
            Dg015hh = model.Dg015hh?.Trim().ToUpper(),

            Fecregt = DateTime.Now,
            Tpnom = model.Tpnom,
            Stareg = model.Stareg
        };

        await _context.Database.ExecuteSqlInterpolatedAsync($@"
        INSERT INTO dbo.RegistNomina
        (
            CIAHNH,
            TPNHNH,
            AÑOHNH,
            PRDHNH,
            FICHNH,
            DPTHNH,
            DG01HH,
            DG02HH,
            DG03HH,
            DG04HH,
            DG05HH,
            DG06HH,
            DG07HH,
            DG08HH,
            DG09HH,
            DG010HH,
            DG011HH,
            DG012HH,
            DG013HH,
            DG014HH,
            DG015HH,
            FECREGT,
            TPNOM,
            STAREG
        )
        VALUES
        (
            {registro.Ciahnh},
            {registro.Tpnhnh},
            {registro.Añohnh},
            {registro.Prdhnh},
            {registro.Fichnh},
            {registro.Dpthnh},
            {registro.Dg01hh},
            {registro.Dg02hh},
            {registro.Dg03hh},
            {registro.Dg04hh},
            {registro.Dg05hh},
            {registro.Dg06hh},
            {registro.Dg07hh},
            {registro.Dg08hh},
            {registro.Dg09hh},
            {registro.Dg010hh},
            {registro.Dg011hh},
            {registro.Dg012hh},
            {registro.Dg013hh},
            {registro.Dg014hh},
            {registro.Dg015hh},
            {registro.Fecregt},
            {registro.Tpnom},
            {registro.Stareg}
        )");

        return Ok(new
        {
            Mensaje = "Registro creado correctamente.",
            Registro = registro
        });
    }
}