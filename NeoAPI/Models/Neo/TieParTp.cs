using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TieParTp
{
    public int IdTieParTp { get; set; }

    public int IdParsiOee { get; set; }

    public int IdParaTp { get; set; }

    public DateTime Tefechai { get; set; }

    public DateTime? Tefechaf { get; set; }

    public double? Teduracion { get; set; }

    public int? IdAreAfect { get; set; }

    public virtual AreAfect? IdAreAfectNavigation { get; set; }

    public virtual ParaTp IdParaTpNavigation { get; set; } = null!;

    public virtual ParsiOee IdParsiOeeNavigation { get; set; } = null!;
}
