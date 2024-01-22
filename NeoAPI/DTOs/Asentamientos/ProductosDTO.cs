using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class ProductosDTO
    {
        public int IdProducto { get; set; }

        public int IdTipoProd { get; set; }

        public string Pnombre { get; set; } = null!;

        public string? Pdescri { get; set; }
        public virtual ICollection<RangoDTO> RangosDTO { get; set; } = new List<RangoDTO>();

    }
}
