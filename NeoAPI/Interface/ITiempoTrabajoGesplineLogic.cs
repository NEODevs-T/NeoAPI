using NeoAPI.ModelsDOCIng;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models.Neo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoAPI.Interface 
{
    public interface ITiempoTrabajoGesplineLogic
    {
        Task<ActionResult<List<string>>> GetTiempoPerdidoActual1Turno();
        Task<ActionResult<List<string>>> GetTiempoPerdidoActual2TurnoAntes0am();
        Task<ActionResult<List<string>>> GetTiempoPerdidoActual2TurnoDespues0am();
        Task<ActionResult<List<string>>> GetTiempoEjecutadoActual1Turno();
        Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2TurnoAntes0am();
        Task<ActionResult<List<string>>> GetTiempoEjecutadoActual2TurnoDespues0am();
        Task<ActionResult<List<string>>> GetTiempoTrabajadoActual1Turno();
        Task<ActionResult<List<string>>> GetTiempoTrabajadoActual2Turno(bool band);
    }
}