using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Views;

public partial class ProductosV
{
    public int IdMaster { get; set; }

    public int IdProducto { get; set; }

    public int IdLinea { get; set; }

    public int IdTipoProd { get; set; }

    public string TipoDeProducto { get; set; } = null!;

    public string? Producto { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public bool Estado { get; set; }
}
