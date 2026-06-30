using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.Bono;
using NeoAPI.Models.Bono;
using BonoResuman = NeoAPI.Models.Bono.Resuman;
using NeoDbContext = NeoAPI.Models.Neo.DbNeoIiContext;

namespace NeoAPI.Controllers.Bono;

[ApiController]
[Route("api/[controller]")]
public class BonoController : ControllerBase
{

    private readonly DbNeoBonoContext _context;
    private readonly NeoDbContext _neoContext;

    public BonoController(DbNeoBonoContext context, NeoDbContext neoContext)
    {
        _context = context;
        _neoContext = neoContext;
    }

    [HttpPost("InsertarResumen")]
    public async Task<IActionResult> InsertarResumen([FromBody] CrearResumenDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (dto == null)
                return BadRequest(new { message = "Debe enviar información." });

            if (string.IsNullOrWhiteSpace(dto.Rgrupo))
                return BadRequest(new { message = "El campo Rgrupo es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.RuserVali))
                return BadRequest(new { message = "El campo RuserVali es obligatorio." });

            if (dto.IdPersonal <= 0)
                return BadRequest(new { message = "El campo IdPersonal debe ser mayor a cero." });

            if (dto.IdTipSuple <= 0)
                return BadRequest(new { message = "El campo IdTipSuple debe ser mayor a cero." });

            if (dto.IdTipIncen <= 0)
                return BadRequest(new { message = "El campo IdTipIncen debe ser mayor a cero." });

            if (dto.Rturno <= 0)
                return BadRequest(new { message = "El campo Rturno debe ser mayor a cero." });

            if (dto.RhoraTrab <= 0)
                return BadRequest(new { message = "El campo RhoraTrab debe ser mayor a cero." });

            if (dto.EsEspecial)
            {
                if (string.IsNullOrWhiteSpace(dto.Motivo))
                    return BadRequest(new { message = "El campo Motivo es obligatorio cuando EsEspecial es true." });

                if (string.IsNullOrWhiteSpace(dto.UsuarioSolicita))
                    return BadRequest(new { message = "El campo UsuarioSolicita es obligatorio cuando EsEspecial es true." });
            }

            const int idMontoEspecialTemporal = 156;

            var resumen = new BonoResuman
            {
                IdTipSuple = dto.IdTipSuple,
                Rfecha = dto.Rfecha,
                Rturno = dto.Rturno,
                Rgrupo = dto.Rgrupo,
                IdPersonal = dto.IdPersonal,
                Rsuplido = dto.Rsuplido,
                IdMontos = dto.EsEspecial ? idMontoEspecialTemporal : dto.IdMontos,
                RuserVali = dto.RuserVali,
                IdTipIncen = dto.IdTipIncen,
                RisMarcaje = dto.RisMarcaje,
                RhoraTrab = dto.RhoraTrab,
                RfechaReal = dto.RfechaReal,
                EsEspecial = dto.EsEspecial
            };

            _context.Resumen.Add(resumen);
            await _context.SaveChangesAsync();

            ResumEspecial? especial = null;

            if (dto.EsEspecial)
            {
                especial = new ResumEspecial
                {
                    IdResumen = resumen.IdResumen,
                    Motivo = dto.Motivo!,
                    IdEstado = 1,
                    FechaSolicitud = DateTime.Now,
                    UsuarioSolicita = dto.UsuarioSolicita!
                };

                _context.ResumEspecials.Add(especial);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return Ok(new
            {
                message = dto.EsEspecial
                    ? "Registro especial insertado correctamente."
                    : "Registro insertado correctamente.",
                data = new
                {
                    idResumen = resumen.IdResumen,
                    esEspecial = resumen.EsEspecial,
                    idEspecial = especial?.IdEspecial,
                    estadoInicial = especial?.IdEstado
                }
            });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return StatusCode(500, new
            {
                message = "Error al insertar el registro.",
                error = ex.Message,
                innerError = ex.InnerException?.Message,
                detalleCompleto = ex.ToString()
            });
        }
    }
    
    [HttpPut("especial/{idEspecial}/estado")]
    public async Task<IActionResult> ActualizarEstadoEspecial(
        int idEspecial,
        [FromBody] ActualizarEstadoEspecialDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (dto == null)
                return BadRequest(new { message = "Debe enviar información." });

            if (string.IsNullOrWhiteSpace(dto.UsuarioAprobador))
                return BadRequest(new { message = "El campo UsuarioAprobador es obligatorio." });

            var especial = await _context.ResumEspecials
                .FirstOrDefaultAsync(x => x.IdEspecial == idEspecial);

            if (especial == null)
                return NotFound(new { message = "No se encontró el registro especial." });

            var estadoAnterior = especial.IdEstado;

            if (estadoAnterior == dto.NuevoIdEstado)
            {
                return BadRequest(new
                {
                    message = "El nuevo estado es igual al estado actual. No hay cambios para guardar."
                });
            }

            // Actualizar estado actual
            especial.IdEstado = dto.NuevoIdEstado;

            // Definir acción para el histórico
            string accion = dto.NuevoIdEstado == 5
                ? "Rechazado"
                : "Aprobado";

            // Insertar histórico
            var historico = new ResumEspecialAproba
            {
                IdEspecial = especial.IdEspecial,
                Nivel = dto.Nivel,
                UsuarioAprobador = dto.UsuarioAprobador,
                Accion = accion,
                Comentario = dto.Comentario,
                FechaAccion = DateTime.Now
            };

            _context.ResumEspecialAprobas.Add(historico);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                message = "Estado actualizado correctamente.",
                data = new
                {
                    idEspecial = especial.IdEspecial,
                    estadoAnterior,
                    nuevoEstado = especial.IdEstado,
                    historico = historico
                }
            });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return StatusCode(500, new
            {
                message = "Ocurrió un error al actualizar el estado del registro especial.",
                error = ex.Message
            });
        }
    }

    [HttpGet("especial/{idEspecial}/historial")]
    public async Task<IActionResult> ObtenerHistorialEspecial(int idEspecial)
    {
        try
        {
            var existeEspecial = await _context.ResumEspecials
                .AnyAsync(x => x.IdEspecial == idEspecial);

            if (!existeEspecial)
            {
                return NotFound(new
                {
                    message = $"No existe un registro en ResumEspecial con IdEspecial = {idEspecial}."
                });
            }

            var historial = await _context.ResumEspecialAprobas
                .Where(x => x.IdEspecial == idEspecial)
                .OrderBy(x => x.FechaAccion)
                .Select(x => new
                {
                    x.IdAprobacion,
                    x.IdEspecial,
                    x.Nivel,
                    x.UsuarioAprobador,
                    x.Accion,
                    x.Comentario,
                    x.FechaAccion
                })
                .ToListAsync();

            return Ok(new
            {
                message = historial.Any()
                    ? "Historial obtenido correctamente."
                    : "El registro existe, pero aún no tiene historial.",
                idEspecial = idEspecial,
                total = historial.Count,
                data = historial
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Ocurrió un error al consultar el historial del registro especial.",
                error = ex.Message
            });
        }
    }

    [HttpGet("especiales-pendientes")]
    public async Task<IActionResult> ObtenerEspecialesPendientes()
    {
        try
        {
            var especiales = await _context.ResumEspecials
                .Include(e => e.IdResumenNavigation)
                .Where(e => e.IdEstado == 1 || e.IdEstado == 2 || e.IdEstado == 3)
                .Select(e => new
                {
                    e.IdEspecial,
                    e.IdResumen,
                    e.IdEstado,
                    e.Motivo,
                    e.FechaSolicitud,
                    e.UsuarioSolicita,

                    IdPersonal = e.IdResumenNavigation.IdPersonal,
                    Rfecha = e.IdResumenNavigation.Rfecha,
                    RfechaReal = e.IdResumenNavigation.RfechaReal,
                    Rturno = e.IdResumenNavigation.Rturno,
                    Rgrupo = e.IdResumenNavigation.Rgrupo,
                    Rsuplido = e.IdResumenNavigation.Rsuplido,
                    RhoraTrab = e.IdResumenNavigation.RhoraTrab,
                    RuserVali = e.IdResumenNavigation.RuserVali,
                    RisMarcaje = e.IdResumenNavigation.RisMarcaje
                })
                .OrderBy(x => x.IdEstado)
                .ThenByDescending(x => x.FechaSolicitud)
                .ToListAsync();

            var idsPersonal = especiales
                .Select(x => x.IdPersonal)
                .Distinct()
                .ToList();

            var personalDict = await _neoContext.Personals
                .Where(p => idsPersonal.Contains(p.IdPersonal))
                .Select(p => new
                {
                    p.IdPersonal,
                    p.PeFicha,
                    p.PeNombre,
                    p.PeApellido
                })
                .ToDictionaryAsync(p => p.IdPersonal);

            var data = especiales.Select(e =>
            {
                personalDict.TryGetValue(e.IdPersonal, out var personal);

                return new
                {
                    idEspecial = e.IdEspecial,
                    idResumen = e.IdResumen,
                    idEstado = e.IdEstado,
                    motivo = e.Motivo,
                    fechaSolicitud = e.FechaSolicitud,
                    usuarioSolicita = e.UsuarioSolicita,

                    idPersonal = e.IdPersonal,

                    peFicha = personal?.PeFicha ?? string.Empty,

                    nombreTrabajador = personal == null
                        ? string.Empty
                        : $"{personal.PeNombre ?? string.Empty} {personal.PeApellido ?? string.Empty}".Trim(),

                    rfecha = e.Rfecha,
                    rfechaReal = e.RfechaReal,
                    rturno = e.Rturno,
                    rgrupo = e.Rgrupo,
                    rsuplido = e.Rsuplido,
                    rhoraTrab = e.RhoraTrab,
                    ruserVali = e.RuserVali,
                    risMarcaje = e.RisMarcaje
                };
            })
            .ToList();

            return Ok(new
            {
                message = data.Any()
                    ? "Pendientes obtenidos correctamente."
                    : "No hay trabajos especiales pendientes.",
                total = data.Count,
                data
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Ocurrió un error al consultar los pendientes.",
                error = ex.Message,
                innerError = ex.InnerException?.Message
            });
        }
    }

    [HttpGet("especiales-historico")]
    public async Task<IActionResult> ObtenerHistoricoEspeciales()
    {
        try
        {
            var especiales = await _context.ResumEspecials
                .Include(e => e.IdResumenNavigation)
                .Where(e => e.IdEstado == 4 || e.IdEstado == 5)
                .Select(e => new
                {
                    e.IdEspecial,
                    e.IdResumen,
                    e.IdEstado,
                    e.Motivo,
                    e.FechaSolicitud,
                    e.UsuarioSolicita,

                    IdPersonal = e.IdResumenNavigation.IdPersonal,
                    Rfecha = e.IdResumenNavigation.Rfecha,
                    RfechaReal = e.IdResumenNavigation.RfechaReal,
                    Rturno = e.IdResumenNavigation.Rturno,
                    Rgrupo = e.IdResumenNavigation.Rgrupo,
                    Rsuplido = e.IdResumenNavigation.Rsuplido,
                    RhoraTrab = e.IdResumenNavigation.RhoraTrab,
                    RuserVali = e.IdResumenNavigation.RuserVali,
                    RisMarcaje = e.IdResumenNavigation.RisMarcaje
                })
                .OrderByDescending(x => x.FechaSolicitud)
                .ToListAsync();

            var idsPersonal = especiales
                .Select(x => x.IdPersonal)
                .Distinct()
                .ToList();

            var personalDict = await _neoContext.Personals
                .Where(p => idsPersonal.Contains(p.IdPersonal))
                .Select(p => new
                {
                    p.IdPersonal,
                    p.PeFicha,
                    p.PeNombre,
                    p.PeApellido
                })
                .ToDictionaryAsync(p => p.IdPersonal);

            var idsEspeciales = especiales
                .Select(x => x.IdEspecial)
                .Distinct()
                .ToList();

            var comentariosRechazoDict = await _context.ResumEspecialAprobas
                .Where(a => idsEspeciales.Contains(a.IdEspecial)
                            && a.Accion == "Rechazado")
                .OrderByDescending(a => a.FechaAccion)
                .GroupBy(a => a.IdEspecial)
                .Select(g => new
                {
                    IdEspecial = g.Key,
                    Comentario = g.First().Comentario
                })
                .ToDictionaryAsync(x => x.IdEspecial, x => x.Comentario);

            var data = especiales.Select(e =>
            {
                personalDict.TryGetValue(e.IdPersonal, out var personal);
                comentariosRechazoDict.TryGetValue(e.IdEspecial, out var comentarioRechazo);

                return new
                {
                    idEspecial = e.IdEspecial,
                    idResumen = e.IdResumen,
                    idEstado = e.IdEstado,
                    motivo = e.Motivo,
                    fechaSolicitud = e.FechaSolicitud,
                    usuarioSolicita = e.UsuarioSolicita,

                    idPersonal = e.IdPersonal,

                    peFicha = personal?.PeFicha ?? string.Empty,

                    nombreTrabajador = personal == null
                        ? string.Empty
                        : $"{personal.PeNombre ?? string.Empty} {personal.PeApellido ?? string.Empty}".Trim(),

                    rfecha = e.Rfecha,
                    rfechaReal = e.RfechaReal,
                    rturno = e.Rturno,
                    rgrupo = e.Rgrupo,
                    rsuplido = e.Rsuplido,
                    rhoraTrab = e.RhoraTrab,
                    ruserVali = e.RuserVali,
                    risMarcaje = e.RisMarcaje,

                    comentarioRechazo = e.IdEstado == 5
                        ? comentarioRechazo
                        : null
                };
            })
            .ToList();

            return Ok(new
            {
                message = data.Any()
                    ? "Histórico consultado correctamente."
                    : "No hay pagos especiales en histórico.",
                total = data.Count,
                data
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Ocurrió un error al consultar el histórico de pagos especiales.",
                error = ex.Message,
                innerError = ex.InnerException?.Message
            });
        }
    }
}