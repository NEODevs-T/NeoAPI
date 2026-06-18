using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.Bono;
using NeoAPI.Models.Bono;

namespace NeoAPI.Controllers.Bono;

[ApiController]
[Route("api/[controller]")]
public class BonoController : ControllerBase
{
    private readonly DbNeoBonoContext _context;

    public BonoController(DbNeoBonoContext context)
    {
        _context = context;
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

            if (dto.EsEspecial)
            {
                if (string.IsNullOrWhiteSpace(dto.Motivo))
                    return BadRequest(new { message = "El campo Motivo es obligatorio cuando EsEspecial es true." });

                if (!dto.IdEstado.HasValue)
                    return BadRequest(new { message = "El campo IdEstado es obligatorio cuando EsEspecial es true." });

                if (!dto.FechaSolicitud.HasValue)
                    return BadRequest(new { message = "El campo FechaSolicitud es obligatorio cuando EsEspecial es true." });

                if (string.IsNullOrWhiteSpace(dto.UsuarioSolicita))
                    return BadRequest(new { message = "El campo UsuarioSolicita es obligatorio cuando EsEspecial es true." });
            }

            var resumen = new Resuman
            {
                IdTipSuple = dto.IdTipSuple,
                Rfecha = dto.Rfecha,
                Rturno = dto.Rturno,
                Rgrupo = dto.Rgrupo,
                IdPersonal = dto.IdPersonal,
                Rsuplido = dto.Rsuplido,
                IdMontos = dto.IdMontos,
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
                    IdEstado = dto.IdEstado!.Value,
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
                    ? "Registro insertado correctamente en Resuman y ResumEspecial."
                    : "Registro insertado correctamente en Resuman.",
                data = new
                {
                    resumen,
                    resumEspecial = especial
                }
            });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return StatusCode(500, new
            {
                message = "Error al insertar el registro.",
                error = ex.Message
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

}