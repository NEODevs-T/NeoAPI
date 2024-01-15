using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class CategoriaDTO
    {
        public int IdCategori { get; set; }

        public string Cnombre { get; set; } = null!;

        public string Ccodigo { get; set; } = null!;

        public DateTime CfechaCrea { get; set; }

        public string? Cdescri { get; set; }

        public bool Cesta { get; set; }

        public virtual ICollection<CorteDiscDTO> CorteDisDTO { get; set; } = new List<CorteDiscDTO>();

    }
}
