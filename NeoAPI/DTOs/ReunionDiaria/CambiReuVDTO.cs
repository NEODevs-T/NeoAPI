using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class CambiReuVDTO
{
    public int IdCambFec { get; set; }

    public int IdReuDia { get; set; }

    public DateTime FechaCambio { get; set; }

    public int TipoFecha { get; set; }

    public string? Discrepancia { get; set; }

    public string? Estado { get; set; }

    public string? Accion { get; set; }

    public DateTime FechaTrabajo { get; set; }

    public string? Centro { get; set; }

    public string? Division { get; set; }

    public string? Linea { get; set; }

    public string? EquipoCodigo { get; set; }
}
