using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class ParaTp
{
    public int IdParaTp { get; set; }

    public int IdTiParTp { get; set; }

    public string? Pcodigo { get; set; }

    public string Pnombre { get; set; } = null!;

    public bool Pestado { get; set; }

    public virtual TiParTp IdTiParTpNavigation { get; set; } = null!;

    public virtual ICollection<TieParTp> TieParTps { get; set; } = new List<TieParTp>();
}
