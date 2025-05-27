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
using NeoAPI.Models.PolybaseBPCSVen;
using System.Drawing.Drawing2D;

namespace NeoAPI.Controllers.OEE;


[ApiController]
[Route("api/[controller]")]

public class OEEController : ControllerBase
{
    private readonly PolybaseBPCSVenContext _context;

    public OEEController(PolybaseBPCSVenContext context)
    {
        _context = context;
    }

    [HttpGet("GetMaquinaProductosProduccionActual1Turno")]
    public async Task<ActionResult<List<string>>> GetMaquinaProductosProduccionActual1Turno()
    {
        var fechaActual = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMdd"));
        var consulta =
            from th in _context.Iths
            where th.Ttype == "R"
                && th.Ttdte == fechaActual
                && th.Twhs == "PT "
                && th.Thtime >= 6000
                && th.Thtime < 180000
            group th by new { th.Thwrkc, th.Tprod } into grp
            orderby grp.Key.Thwrkc
            select new
            {
                thwrkc = grp.Key.Thwrkc,
                tprod = grp.Key.Tprod,
                produccion = grp.Sum(x => x.Tqty)
            };

        return Ok(consulta.ToList());
    }

   /* SELECT ITH.THWRKC, ITH.TPROD, Sum(ITH.TQTY) AS PRODUCCION 
                        FROM DbBpcsVen.bpcs.ITH
                        WHERE (ITH.TTYPE='R') AND (ITH.TTDTE ='20250527') AND (ITH.TWHS='PT ') AND (ITH.THTIME>=60000 And ITH.THTIME<180000) 
                        GROUP BY ITH.THWRKC, ITH.TPROD 
                        ORDER BY ITH.THWRKC*/


}   