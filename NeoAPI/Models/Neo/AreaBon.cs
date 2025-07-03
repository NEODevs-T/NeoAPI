using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Area de los escalones del bono
/// </summary>
public partial class AreaBon
{
    public int IdAreaBon { get; set; }

    /// <summary>
    /// Nombre del Area
    /// </summary>
    public string Abnombre { get; set; } = null!;

    /// <summary>
    /// Descripcion del Area
    /// </summary>
    public string? Abdescrip { get; set; }

    /// <summary>
    /// Estado del Area (1:Activo, 0:Inactivo)
    /// </summary>
    public bool Abestado { get; set; }

    public virtual ICollection<Escalon> Escalons { get; set; } = new List<Escalon>();
}
