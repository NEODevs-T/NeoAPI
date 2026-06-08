using System;
using System.Collections.Generic;

namespace NeoAPI.RRHHModels;

public partial class PeriodosV
{
    public string Ciafpr { get; set; } = null!;

    public string Tpnfpr { get; set; } = null!;

    public decimal Añofpr { get; set; }

    public decimal Prdfpr { get; set; }

    public decimal Fecifp { get; set; }

    public decimal Fecffp { get; set; }

    public decimal Mesfpr { get; set; }
}
