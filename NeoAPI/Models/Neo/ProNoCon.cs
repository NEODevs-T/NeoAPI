using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class ProNoCon
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

    public int IdCaUnidad { get; set; }

    public int? IdIdentif { get; set; }

    public int IdProDisp { get; set; }

    public int IdLugaEven { get; set; }

    public DateOnly Pncfecha { get; set; }

    public string? PncordFabr { get; set; }

    public int IdCausa { get; set; }

    public virtual CaUnidad IdCaUnidadNavigation { get; set; } = null!;

    public virtual Causa IdCausaNavigation { get; set; } = null!;

    public virtual DispDefi IdDisDefiNavigation { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Identifi? IdIdentifNavigation { get; set; }

    public virtual PropDisp IdProDispNavigation { get; set; } = null!;

    public virtual Tipo IdTipoNavigation { get; set; } = null!;
}
