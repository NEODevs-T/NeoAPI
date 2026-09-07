    using System.Globalization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NeoAPI.DTOs.RRHH;
    using NeoAPI.RRHHModels;

    namespace NeoAPI.Controllers.RRHH;

    [ApiController]
    [Route("api/[controller]")]
    public class VRotacionHistController : ControllerBase
    {
        private readonly DbRRHHContext _context;

        public VRotacionHistController(DbRRHHContext context)
        {
            _context = context;

            _context.Database.SetCommandTimeout(
                TimeSpan.FromMinutes(20));
        }

        private static (int Inicio, int Fin) ObtenerRangoPeriodos(
            int anio,
            int mes)
        {
            var primerDiaMes = new DateTime(anio, mes, 1);

            var ultimoDiaMes = primerDiaMes
                .AddMonths(1)
                .AddDays(-1);

            int inicio = ISOWeek.GetWeekOfYear(primerDiaMes);
            int fin = ISOWeek.GetWeekOfYear(ultimoDiaMes);

            return (inicio, fin);
        }

        // =========================================
        // CONSULTA DETALLADA CON PAGINACION
        // =========================================
        [HttpGet]
        public async Task<IActionResult> GetRotacionHist(
            decimal anio,
            string? tpnhnh,
            string? ficha,
            string? departamento,
            decimal? periodo,
            int page = 1,
            int pageSize = 500)
        {
            ficha = ficha?.Trim();

            page = page <= 0 ? 1 : page;

            pageSize = pageSize switch
            {
                <= 0 => 500,
                > 2000 => 2000,
                _ => pageSize
            };

            IQueryable<VRotacionHist> query = _context
                .VRotacionHists
                .AsNoTracking();

            if (anio <= 0)
            {
                return BadRequest(new
                {
                    Mensaje = "Debe seleccionar un año para realizar la consulta."
                });
            }

            ficha = ficha?.Trim();
            departamento = departamento?.Trim();

            query = query.Where(x =>
                x.Tpnhnh == "2101" ||
                x.Tpnhnh == "2102" ||
                x.Tpnhnh == "2301" ||
                x.Tpnhnh == "4101" ||
                x.Tpnhnh == "4401");

            if (!string.IsNullOrWhiteSpace(tpnhnh))
            {
                query = query.Where(x =>
                    (x.Tpnhnh ?? "").Trim() == tpnhnh.Trim());
            }

            query = query.Where(x =>
                x.Añohnh == anio);

            if (!string.IsNullOrWhiteSpace(ficha))
            {
                query = query.Where(x =>
                    (x.Fichnh ?? "").Trim() == ficha);
            }

            if (periodo.HasValue)
            {
                query = query.Where(x =>
                    x.Prdhnh == periodo.Value);
            }

            if (!string.IsNullOrWhiteSpace(departamento))
            {
                query = query.Where(x =>
                    (x.Dpthnh ?? "").Trim() == departamento);
            }

            int totalRecords = await query.CountAsync();

            int totalPages = (int)Math.Ceiling(
                totalRecords / (double)pageSize);

            if (page > totalPages && totalPages > 0)
            {
                return BadRequest(new
                {
                    Message =
                        $"La página solicitada ({page}) excede el total de páginas disponibles ({totalPages}).",
                    TotalRecords = totalRecords,
                    TotalPages = totalPages
                });
            }

            var result = await query
                .OrderBy(x => x.Añohnh)
                .ThenBy(x => x.Prdhnh)
                .ThenBy(x => x.Fichnh)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new VRotacionHistDTO
                {
                    Ciahnh = x.Ciahnh ?? "",
                    Tpnhnh = x.Tpnhnh ?? "",
                    Fichnh = x.Fichnh ?? "",

                    Dg01hh = x.Dg01hh ?? "",
                    Dg02hh = x.Dg02hh ?? "",
                    Dg03hh = x.Dg03hh ?? "",
                    Dg04hh = x.Dg04hh ?? "",
                    Dg05hh = x.Dg05hh ?? "",
                    Dg06hh = x.Dg06hh ?? "",
                    Dg07hh = x.Dg07hh ?? "",
                    Dg08hh = x.Dg08hh ?? "",
                    Dg09hh = x.Dg09hh ?? "",
                    Dg10hh = x.Dg10hh ?? "",
                    Dg11hh = x.Dg11hh ?? "",
                    Dg12hh = x.Dg12hh ?? "",
                    Dg13hh = x.Dg13hh ?? "",
                    Dg14hh = x.Dg14hh ?? "",
                    Dg15hh = x.Dg15hh ?? "",

                    Añohnh = x.Añohnh,
                    Prdhnh = x.Prdhnh,
                    Dpthnh = x.Dpthnh ?? ""
                })
                .ToListAsync();

            return Ok(new PagedResponse<VRotacionHistDTO>
            {
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Data = result
            });
        }

        // =========================================
        // RESUMEN DASHBOARD
        // =========================================
        [HttpGet("resumen")]
        public async Task<IActionResult> GetResumen(
            decimal anio,
            int? mes = null)
        {
            var query = _context.VRotacionHists
                .AsNoTracking()
                .Where(x => x.Añohnh == anio);

            // PRDHNH = PERIODO (1-52 / 53)
            // NO ES MES
            if (mes.HasValue)
            {
                var rango = ObtenerRangoPeriodos(
                    (int)anio,
                    mes.Value);

                query = query.Where(x =>
                    x.Prdhnh >= rango.Inicio &&
                    x.Prdhnh <= rango.Fin);
            }

            var data = await query
                .Select(x => new
                {
                    x.Dg01hh,
                    x.Dg02hh,
                    x.Dg03hh,
                    x.Dg04hh,
                    x.Dg05hh,
                    x.Dg06hh,
                    x.Dg07hh,
                    x.Dg08hh,
                    x.Dg09hh,
                    x.Dg10hh,
                    x.Dg11hh,
                    x.Dg12hh,
                    x.Dg13hh,
                    x.Dg14hh,
                    x.Dg15hh
                })
                .ToListAsync();

            int permisos = 0;
            int faltas = 0;

            foreach (var item in data)
            {
                var dias = new[]
                {
                    item.Dg01hh,
                    item.Dg02hh,
                    item.Dg03hh,
                    item.Dg04hh,
                    item.Dg05hh,
                    item.Dg06hh,
                    item.Dg07hh,
                    item.Dg08hh,
                    item.Dg09hh,
                    item.Dg10hh,
                    item.Dg11hh,
                    item.Dg12hh,
                    item.Dg13hh,
                    item.Dg14hh,
                    item.Dg15hh
                };

                foreach (var dia in dias)
                {
                    var valor = (dia ?? "")
                        .Trim()
                        .ToUpper();

                    if (valor == "P")
                    {
                        permisos++;
                    }
                    else if (valor == "F")
                    {
                        faltas++;
                    }
                }
            }

            return Ok(new IndicadoresResumenDTO
            {
                Anio = anio,
                Mes = mes,
                Permisos = permisos,
                Faltas = faltas
            });
        }
    }