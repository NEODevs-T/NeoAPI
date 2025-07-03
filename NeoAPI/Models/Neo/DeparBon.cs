using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Departamentos de los montos
/// </summary>
public partial class DeparBon
{
    public int IdDeparBon { get; set; }

    /// <summary>
    /// Departamento del monto del Bono
    /// </summary>
    public string Dbnombre { get; set; } = null!;

    public int Dbestado { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
