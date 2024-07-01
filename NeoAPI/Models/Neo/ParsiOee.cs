using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class ParsiOee
{
    public int IdParsiOee { get; set; }

    public int IdTurnoTp { get; set; }

    public int IdArea { get; set; }

    public double? Ptrabajado { get; set; }

    public double? Pperdido { get; set; }

    public int? Ppbueno { get; set; }

    public int? Ppmalo { get; set; }

    public double? Pvelocidad { get; set; }

    public double? Prendi { get; set; }

    public double? Pcalidad { get; set; }

    public double? Pdispo { get; set; }

    public double? Poee { get; set; }

    public virtual TurnoTp IdTurnoTpNavigation { get; set; } = null!;

    public virtual ICollection<TieEjeTp> TieEjeTps { get; set; } = new List<TieEjeTp>();

    public virtual ICollection<TieParTp> TieParTps { get; set; } = new List<TieParTp>();
}
