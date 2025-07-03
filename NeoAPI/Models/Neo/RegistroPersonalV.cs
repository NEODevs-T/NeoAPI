using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class RegistroPersonalV
{
    public DateTime FechaDeTrabajo { get; set; }

    public string? Dia { get; set; }

    public string? Mes { get; set; }

    public int? Ano { get; set; }

    public int Turno { get; set; }

    public string Grupo { get; set; } = null!;

    public string Ficha { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Centro { get; set; } = null!;

    public string Linea { get; set; } = null!;

    public string? CentroCosto { get; set; }

    public string Puesto { get; set; } = null!;

    public int IdPuesto { get; set; }

    public string TipoDeIncidencia { get; set; } = null!;

    public string TipoDeSuplencia { get; set; } = null!;

    public string? Suplido { get; set; }
}
