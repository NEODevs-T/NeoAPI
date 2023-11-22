using Microsoft.AspNetCore.Mvc;

namespace NeoAPI.Controllers.Asentamientos
{
    [ApiController]
    [Route("[controller]")]

     public class CorteDiscrepanciaController : ControllerBase
    {
        //Obtener Paises 
        [HttpGet("Prueba")]
        public async Task<ActionResult<string>> GetPrueba()
        {


            return "Hola";
        }
    }
}