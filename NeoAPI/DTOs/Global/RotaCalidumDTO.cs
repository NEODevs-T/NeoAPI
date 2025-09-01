using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Global;

public partial class RotaCalidumDTO
{
    public int RcidRotCal { get; set; }

    public int Rcfecha { get; set; }

    public int Rcturno { get; set; }

    public string Rcgrupo { get; set; } = null!;
}
