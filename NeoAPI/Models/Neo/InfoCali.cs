using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class InfoCali
{
    public int IdInfoCali { get; set; }

    public int IdInfoAse { get; set; }

    public string Iclote { get; set; } = null!;

    public string IccodProd { get; set; } = null!;

    public bool IcisCamPro { get; set; }

    public virtual InfoAse IdInfoAseNavigation { get; set; } = null!;
}
