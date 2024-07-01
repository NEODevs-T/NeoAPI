using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CentroCostoV
{
    public int Idpais { get; set; }

    public string Wid { get; set; } = null!;

    public decimal Wwrkc { get; set; }

    public string Wdesc { get; set; } = null!;
}
