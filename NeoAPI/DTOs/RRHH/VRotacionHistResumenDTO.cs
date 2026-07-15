namespace NeoAPI.DTOs.RRHH;

public class VRotacionHistResumenDTO
{
    public decimal Anio { get; set; }

    public int? Mes { get; set; }

    public int Permisos { get; set; }

    public int Faltas { get; set; }

    public int Vacaciones { get; set; }

    public int Libres { get; set; }

    public int Reposos { get; set; }

    public int Ausencias { get; set; }

    public int Utilidades { get; set; }

    public int TotalRegistros { get; set; }
}