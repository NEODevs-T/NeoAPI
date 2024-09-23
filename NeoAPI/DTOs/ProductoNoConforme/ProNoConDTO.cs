using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.PNC;


public partial class ProNoConDTO
{
    public int IdProNoCon { get; set; }

    public string PnccodProd { get; set; } = null!;

    public string PncdesProd { get; set; } = null!;

    public string? Pnclote { get; set; }

    public int Pnccantida { get; set; }

    public string PnccauLibe { get; set; } = null!;

    public string PncindLibe { get; set; } = null!;

    public string Pnccargador { get; set; } = null!;

    public int IdTipo { get; set; }

    public int IdDisDefi { get; set; }

    public int IdEstado { get; set; }

    public int IdUnidad { get; set; }

    public int? IdIdentif { get; set; }

    public int IdProDisp { get; set; }

    public int IdLugaEven { get; set; }

    public DateTime Pncfecha { get; set; }

    public string? PncordFabr { get; set; }

    public int IdCausa { get; set; }

    public virtual Pnccausa IdCausaNavigation { get; set; } = null!;

    public virtual PncdisDefi IdDisDefiNavigation { get; set; } = null!;

    public virtual Pncestado IdEstadoNavigation { get; set; } = null!;

    public virtual Pncidentif? IdIdentifNavigation { get; set; }

    //public virtual Linea IdLugaEvenNavigation { get; set; } = null!;

    public virtual PncproDisp IdProDispNavigation { get; set; } = null!;

    public virtual Pnctipo IdTipoNavigation { get; set; } = null!;

    public virtual Pncunidad IdUnidadNavigation { get; set; } = null!;
}
