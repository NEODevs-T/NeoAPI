using System;
using System.Collections.Generic;

namespace NeoAPI.ModelsViews;

public partial class MaestraV
{
    public string Pais { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Centro { get; set; } = null!;

    public string? División { get; set; }

    public string Linea { get; set; } = null!;

    public int IdPais { get; set; }

    public int IdEmpresa { get; set; }

    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public int IdLinea { get; set; }

    public int IdMaster { get; set; }
}
