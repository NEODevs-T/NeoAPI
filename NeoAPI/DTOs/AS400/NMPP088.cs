namespace NeoAPI.DTOs.AS400
{
    public class NMPP088
    {
        public string? CIAMBA { get; set; } //ID COMPANY
        public string? MODMBA { get; set; } //MODALIDAD
        public string? FICMBA { get; set; } //FICHA
        public string? CTOMBA { get; set; } //CONCEPTO
        public string? VALMBA { get; set; } //MONTO
        public string? FECMBA { get; set; } //FECHA
        public string? YACMBA { get; set; }//CABTAB MOV BATCH
        public double? YAPMBA { get; set; } //PROCESADO BATCH
        public string? SECMBA { get; set; } //SECUENCIA
        public string? TXTMBA { get; set; } //TEXTO MOVIMIENTOS BATCH

    }
}
