using NeoAPI.ModelsDOCIng;
using Microsoft.EntityFrameworkCore;
using NeoAPI.DTOs.Maestra;
using NeoAPI.Models.Neo;

namespace NeoAPI.Interface 
{
    public interface IReunionDiaLogic
    {
        public Task<CentroDivisionDTO> GetCentroDivi(string centro, string division, int tipo);
        public Task<List<CambFec>> GetPendientesQuincenal(CentroDivisionDTO centrodiv);
        public CentroDivisionDTO BuildCentroDivisionDTO(Master centrodiscrepancia);
    }
}