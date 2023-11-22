using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models;

namespace NeoAPI.Controllers.Asentamientos
{
    [ApiController]
    [Route("[controller]")]

     public class Asentamientos : ControllerBase
    {
        //Obtener Paises 
        [HttpGet("Prueba")]
        public async Task<ActionResult<string>> GetPrueba()
        {


            return "Hola";
        }

    }
}