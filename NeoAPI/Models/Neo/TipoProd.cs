using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Tipo de producto
/// </summary>
public partial class TipoProd
{
    public int IdTipoProd { get; set; }

    public string Tpnombre { get; set; } = null!;

    public string? Tpdescri { get; set; }

    public bool Tpestado { get; set; }

    public DateTime TpfechCrea { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
