using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class CorteDiscDTO
    {
        public int IdCorteDis { get; set; }

        public int IdCategori { get; set; }

        public string CdaccCorr { get; set; } = null!;

        public bool CdisListo { get; set; }

        public int IdAsenta { get; set; }

        public virtual CategoriaDTO? CategoriaDTONavigation { get; set; } = null!;
        public virtual AsentumDTO? AsentumDTONavigation { get; set; } = null!;


    }
}
