using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class SeccionDTO
    {
        public int IdSeccion { get; set; }

        public string Snombre { get; set; } = null!;

        public string? Sdescri { get; set; }
        public virtual ICollection<VariableDTO> VariablesDTO { get; set; } = new List<VariableDTO>();


    }
}
