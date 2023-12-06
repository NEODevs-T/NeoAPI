using System;
using System.Collections.Generic;

namespace NeoAPI.ModelsViews;

public partial class CentrosV
{
    public int IdEmpresa { get; set; }

    public int IdCentro { get; set; }

    public string Centro { get; set; } = null!;

    public bool Estado { get; set; }
}
