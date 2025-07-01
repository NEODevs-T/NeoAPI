using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Resuman
{
    public int IdResumen { get; set; }

    public int IdTipSuple { get; set; }

    public DateTime Rfecha { get; set; }

    public int Rturno { get; set; }

    public string Rgrupo { get; set; } = null!;

    public int IdPersonal { get; set; }

    public string? Rsuplido { get; set; }

    public int IdMontos { get; set; }

    public string RuserVali { get; set; } = null!;

    public int IdTipIncen { get; set; }

    public bool RisMarcaje { get; set; }

    public int RhoraTrab { get; set; }

    public DateTime? RfechaReal { get; set; }

    public virtual Monto IdMontosNavigation { get; set; } = null!;

    public virtual Personal IdPersonalNavigation { get; set; } = null!;

    public virtual TipIncen IdTipIncenNavigation { get; set; } = null!;

    public virtual TipSuple IdTipSupleNavigation { get; set; } = null!;
}
