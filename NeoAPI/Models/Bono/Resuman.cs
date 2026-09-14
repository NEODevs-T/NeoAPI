using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Bono;

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

    public bool EsEspecial { get; set; }

    public virtual ICollection<ResumEspecial> ResumEspecials { get; set; } = new List<ResumEspecial>();
}
