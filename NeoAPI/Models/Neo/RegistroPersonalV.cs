using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class RegistroPersonalV
{
    public DateTime? FechaDeTrabajo { get; set; }

    public string? Dia { get; set; }

    public string? Mes { get; set; }

    public int? Ano { get; set; }

    public int? Turno { get; set; }

    public string? Grupo { get; set; }

    public string? Ficha { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string Centro { get; set; } = null!;

    public string Linea { get; set; } = null!;

    public string? CentroCosto { get; set; }

    public string Puesto { get; set; } = null!;

    public int IdPuesto { get; set; }

    public int? Mescalon { get; set; }

    public double? Mmonto { get; set; }
}
