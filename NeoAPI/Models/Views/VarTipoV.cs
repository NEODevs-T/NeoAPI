using System;
using System.Collections.Generic;

namespace NeoAPI.ModelsViews;

public partial class VarTipoV
{
    public int IdMaster { get; set; }

    public int IdLinea { get; set; }

    public int IdTipoVar { get; set; }

    public string TipoDeVariable { get; set; } = null!;

    public bool Estado { get; set; }
}
