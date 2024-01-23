using System;
using System.Collections.Generic;

namespace NeoAPI.Models.NeoVieja;

public partial class LibroNove
{
    public int IdlibrNov { get; set; }

    public int IdLinea { get; set; }

    public string IdEquipo { get; set; } = null!;

    public string Lndiscrepa { get; set; } = null!;

    public double LntiePerMi { get; set; }

    public string LnfichaRes { get; set; } = null!;

    public DateTime Lnfecha { get; set; }

    public string Lngrupo { get; set; } = null!;

    public string Lnturno { get; set; } = null!;

    public string IdMaquina { get; set; } = null!;

    public int IdTipoNove { get; set; }

    public int IdAreaCar { get; set; }

    public string? Lnobserv { get; set; }

    public string? IdParada { get; set; }

    public bool LnisPizUni { get; set; }

    public int IdCtpm { get; set; }

    public int? LnisResu { get; set; }

    public virtual AreaCarga IdAreaCarNavigation { get; set; } = null!;

    public virtual ClasifiTpm IdCtpmNavigation { get; set; } = null!;

    public virtual TiParTp IdTipoNoveNavigation { get; set; } = null!;
}
