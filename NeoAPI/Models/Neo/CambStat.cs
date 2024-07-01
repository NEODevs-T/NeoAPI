using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CambStat
{
    public int IdCambStat { get; set; }

    public int IdReuDia { get; set; }

    public DateTime? Cbfecha { get; set; }

    public string? Cbstat { get; set; }

    public string? Cbuser { get; set; }

    public virtual ReuDium IdReuDiaNavigation { get; set; } = null!;
}
