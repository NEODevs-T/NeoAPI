using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CambiReuV
{
    public int IdCambFec { get; set; }

    public int IdReuDia { get; set; }

    public DateTime FechaCambio { get; set; }

    public int TipoReunion { get; set; }

    public string? Discrepancia { get; set; }

    public string? Estado { get; set; }

    public string? Accion { get; set; }

    public DateTime FechaTrabajo { get; set; }

    public string? Centro { get; set; }

    public string? Division { get; set; }

    public string? Linea { get; set; }

    public string? EquipoCodigo { get; set; }

    public string Responsable { get; set; } = null!;
}
