using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Monto
{
    public int IdMontos { get; set; }

    public int IdLinea { get; set; }

    public int IdPuesTrab { get; set; }

    public bool Mesta { get; set; }

    public DateTime MfecAct { get; set; }

    public string Muser { get; set; } = null!;

    public int IdMaster { get; set; }

    public virtual Linea IdLineaNavigation { get; set; } = null!;

    public virtual PuesTrab IdPuesTrabNavigation { get; set; } = null!;

    public virtual ICollection<Resuman> Resumen { get; set; } = new List<Resuman>();
}
