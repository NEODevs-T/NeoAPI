using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Monto
{
    public int IdMontos { get; set; }

    public int IdLinea { get; set; }

    public int IdPuesTrab { get; set; }

    public int? Mescalon { get; set; }

    public double? Mmonto { get; set; }

    public bool? Mesta { get; set; }

    public DateTime MfecAct { get; set; }

    public string? Muser { get; set; }

    public int? IdMoneda { get; set; }

    public virtual Linea IdLineaNavigation { get; set; } = null!;

    public virtual Monedum? IdMonedaNavigation { get; set; }

    public virtual PuesTrab IdPuesTrabNavigation { get; set; } = null!;

    public virtual ICollection<Resuman> Resumen { get; set; } = new List<Resuman>();
}
