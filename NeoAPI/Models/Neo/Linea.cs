﻿using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Linea
{
    public int IdLinea { get; set; }

    public string Lnom { get; set; } = null!;

    public string? Ldetalle { get; set; }

    public bool Lestado { get; set; }

    public string? LcenCos { get; set; }

    public DateTime Lfecha { get; set; }

    public string? Lofic { get; set; }

    public virtual ICollection<EquipoEam> EquipoEams { get; set; } = new List<EquipoEam>();

    public virtual Master? Master { get; set; }

    public virtual ICollection<Monto> Montos { get; set; } = new List<Monto>();
}
