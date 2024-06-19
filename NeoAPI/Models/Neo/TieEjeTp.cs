using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class TieEjeTp
{
    public int IdTieEjeTp { get; set; }

    public int IdParsiOee { get; set; }

    public DateTime Tefechai { get; set; }

    public DateTime? Tefechaf { get; set; }

    public double? Teduracion { get; set; }

    public double? Tevelocidad { get; set; }

    public int? Tebueno { get; set; }

    public int? Temalo { get; set; }

    public int? TenumVuelt { get; set; }

    public int? Teproducidos { get; set; }

    public virtual ParsiOee IdParsiOeeNavigation { get; set; } = null!;
}
