using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Gespline;

public partial class TipocargospersonalDTO
{
    public string Codidocargospersonal { get; set; } = null!;

    public string? Nombredelcargo { get; set; }

    public int? Estado { get; set; }

    public DateTime? Timespan { get; set; }
    
}
