namespace NeoAPI.DTOs.Bonificaciones
{
    public class ResumenGeneralDTO
    {

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Ficha { get; set; } 
        public string? Grupo { get; set; } 
        public int? Turno { get; set; }
        public string? Concepto { get; set; }
        public string? Suplencia { get; set; }
        public string? FichaSuplida { get; set; }
        public string? Pais { get; set; }
        public string? Empresa { get; set; }
        public string? Centro { get; set; }
        public string? Linea { get; set; }
        public string? PuestoTrabajo { get; set; }
        public double? Monto { get; set; }
        public string? Moneda { get; set; }
        public DateTime? FechaResumen { get; set; }
        public DateTime? FechaPago{ get; set; }
        public string? FichaResumen { get; set; }
        public string? FichaPago { get; set; }



    }
}
