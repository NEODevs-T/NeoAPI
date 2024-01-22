using NeoAPI.Models;

namespace NeoAPI.DTOs.Asentamientos
{
    public class CortesVistaDTO
    {
        public int IdCorteDis { get; set; }
        public int IdCategori { get; set; }
        public string CdaccCorr { get; set; } = null!;
        public bool CdisListo { get; set; }
        public int IdAsenta { get; set; }
        public string Cnombre { get; set; } = null!;
        public double Avalor { get; set; }
        public int IdRango  { get; set; }
        public double Rmin { get; set; }
        public double Rmax { get; set; }
        public double Robj { get; set; }
        public double RlimMin { get; set; }
        public double RlimMax { get; set; }
        public int IdVariable { get; set; }
        public int IdProducto { get; set; }
        public string Pnombre { get; set; } = null!;

        public string Unombre { get; set; } = null!;
        public int IdSeccion { get; set; }
       
        public string SNombre { get; set; } = null!;
        public string Vnombre { get; set; } = null!;
        public string? Vdescri { get; set; }
    }
}
