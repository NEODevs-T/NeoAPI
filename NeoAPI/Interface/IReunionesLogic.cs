using Microsoft.AspNetCore.Mvc;
using NeoAPI.DTOs.Maestra;
using NeoAPI.DTOs.ReunionDiaria;
using NeoAPI.Models.Neo;

namespace NeoAPI.Interface
{
    public interface IReunionesLogic
    {
        Task<List<CarReuDTO>> GetCargoReuDiaria();
        Task<List<AsistenReuDTO>> GetAsisReuDiaria();
        public DateTime GetClosetThursday(DateTime candidate);
        public List<DateTime> ObtenerJuevesObjetivo(DateTime inicio, DateTime final);   
}
}

