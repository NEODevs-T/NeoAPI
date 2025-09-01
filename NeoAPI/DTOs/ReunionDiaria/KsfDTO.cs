using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class KsfDTO
{
    public int Idksf { get; set; }

    public string KsfNombre { get; set; } = null!;

    public bool KsfEsta { get; set; }
}
