using NeoAPI.ModelsDOCIng;
using Microsoft.EntityFrameworkCore;
using NeoAPI.Models.Neo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NeoAPI.Interface 
{
    public interface IMaquinasGesplineLogic
    {
        Task<List<string>> GetMaquinasGesplineActivos1turno();
        Task<List<string>> GetMaquinasGesplineActivos2turnoDespues0am();
        Task<List<string>> GetMaquinasGesplineActivos2turnoAntes0am();
    }
}
