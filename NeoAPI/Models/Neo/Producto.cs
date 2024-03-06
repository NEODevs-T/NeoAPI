using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdTipoProd { get; set; }

    public string Pnombre { get; set; } = null!;

    public string? Pdescri { get; set; }

    public bool Pestado { get; set; }

    public string Pcodigo { get; set; } = null!;

    public DateTime PfechaCrea { get; set; }

    public virtual TipoProd IdTipoProdNavigation { get; set; } = null!;

    public virtual ICollection<Rango> Rangos { get; set; } = new List<Rango>();
}
