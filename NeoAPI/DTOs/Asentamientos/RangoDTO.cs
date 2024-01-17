using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class RangoDTO
    {
        public int IdRango { get; set; }

        public int IdMaster { get; set; }

        public int IdProducto { get; set; }

        public int IdVariable { get; set; }

        public double Rmin { get; set; }

        public double Rmax { get; set; }

        public double Robj { get; set; }

        public double RlimMin { get; set; }

        public double RlimMax { get; set; }


        public DateTime RfechaCrea { get; set; }


        public virtual ICollection<AsentumDTO> AsentaDTO { get; set; } = new List<AsentumDTO>();

        //public virtual ICollection<BloqRang> BloqRangs { get; set; } = new List<BloqRang>();

        //public virtual Master IdMasterNavigation { get; set; } = null!;

        // public virtual Producto IdProductoNavigation { get; set; } = null!;
        
        public virtual VariableDTO? VariableDTONavigation { get; set; } = null!;
    }
}
