using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class AreAfect
{
    public int IdAreAfect { get; set; }

    public string Aanom { get; set; } = null!;

    public bool Aaestado { get; set; }

    public string? Aadetalle { get; set; }

    public virtual ICollection<TieParTp> TieParTps { get; set; } = new List<TieParTp>();
}
