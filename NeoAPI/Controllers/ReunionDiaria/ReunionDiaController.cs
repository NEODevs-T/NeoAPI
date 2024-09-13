// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ReunionDiaApi.Models;


// namespace ReunionDiaApi.Controllers
// {
//     [Route("/[controller]")]
//     [ApiController]
//     public class ReunionDiaController : ControllerBase
//     {

//         private readonly DbNeoContext _Neocontext;


//         public ReunionDiaController(DbNeoContext _DbNeo)
//         {

//             _Neocontext = _DbNeo;
//         }

//         public static List<Centro> centro = new List<Centro> { };
//         public static List<Linea> linea = new List<Linea> { };
//         public static List<Empresa> empresa = new List<Empresa> { };
//         public static List<Pai> pais = new List<Pai> { };
//         public static List<Division> div = new List<Division> { };
//         public static List<Ksf> ksfs = new List<Ksf>();
//         public static List<RespoReu> resporeu = new List<RespoReu>();
//         public static List<ReunionDium> reunionditabla = new List<ReunionDium>();
//         public static List<Division> divisions = new List<Division>();
//         public static List<AsistenReu> asistenreus = new List<AsistenReu>();
//         public static List<CargoReu> cargoreus = new List<CargoReu>();



//         //Api para obtener discrepancias pendientes.




//         ////Obtener los datos de la discrepancia a editar
//         //[HttpGet("discrepancia/{id}")]
//         //public async Task<ActionResult<List<ReunionDium>>> GetEditDiscrepancia(int id)
//         //{
//         //    var disc = await _Neocontext.ReunionDia
//         //        .FirstOrDefaultAsync(h => h.Id == id);

//         //    if (disc == null)
//         //    {
//         //        return NotFound("No se encontro registro");
//         //    }
//         //    return Ok(disc);
//         //}



//         //private async Task<List<ReunionDium>> GetDiscrencacias(string div)
//         //{

//         //    string f1 = DateTime.Now.AddDays(-1).ToString("yyyMMdd");
//         //    string f2 = DateTime.Now.ToString("yyyMMdd");
//         //    return await _Neocontext.ReunionDia
//         //     .Where(a => a.Fecha2 == f1 && a.Div == div || a.Fecha2 == f2 && a.Div == div)
//         //     .OrderByDescending(b => b.Id)
//         //      .ToListAsync();
//         //}



//         //[HttpPost("Discrep")]
//         //public async Task<ActionResult<List<ReunionDium>>> InsertDiscrepancia(ReunionDium reudia)
//         //{
//         //    try
//         //    {
//         //        _Neocontext.ReunionDia.Add(reudia);
//         //        await _Neocontext.SaveChangesAsync();
//         //        return Ok();
//         //    }
//         //    catch
//         //    {
//         //        return BadRequest();
//         //    }

//         //}


//     }
// }
