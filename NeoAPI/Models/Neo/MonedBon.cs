using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Tipo de moneda
/// </summary>
public partial class MonedBon
{
    public int IdMonedBon { get; set; }

    /// <summary>
    /// tipo de moneda
    /// </summary>
    public string Mbtipo { get; set; } = null!;

    /// <summary>
    /// Descripción
    /// </summary>
    public string Mbdescri { get; set; } = null!;

    /// <summary>
    /// 1 activo, 0 inactivo
    /// </summary>
    public bool Mbestado { get; set; }

    public DateTime Mbfcrea { get; set; }

    public virtual ICollection<MontoBon> MontoBons { get; set; } = new List<MontoBon>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
