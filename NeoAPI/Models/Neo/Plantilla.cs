using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Plantilla
{
    public int IdPlantilla { get; set; }

    public int IdPersonal { get; set; }

    public string? Plinea { get; set; }

    public int? PidLinea { get; set; }

    public string? Ppuesto { get; set; }

    public int? PidPuesto { get; set; }

    public string? Pcentro { get; set; }

    public int? PidCentro { get; set; }

    public int? PidMaestra { get; set; }

    public virtual Personal IdPersonalNavigation { get; set; } = null!;
}
