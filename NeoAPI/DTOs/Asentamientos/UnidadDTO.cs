using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class UnidadDTO
    {
        public int IdUnidad { get; set; }
        public string Unombre { get; set; } = null!;
        public string? Udescri { get; set; }
        public virtual ICollection<VariableDTO> VariablesDTO { get; set; } = new List<VariableDTO>();
    }
}
