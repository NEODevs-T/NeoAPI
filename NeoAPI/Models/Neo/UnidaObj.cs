using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Unidades de los objetivos de produccion
/// </summary>
public partial class UnidaObj
{
    public int IdUnidaObj { get; set; }

    /// <summary>
    /// Unidad
    /// </summary>
    public string Uotipo { get; set; } = null!;

    /// <summary>
    /// Descripción de la unidad
    /// </summary>
    public string? Uodescri { get; set; }

    /// <summary>
    /// 1 activo , 0 Inactivo
    /// </summary>
    public bool Uoestado { get; set; }

    public virtual ICollection<Objetivo> Objetivos { get; set; } = new List<Objetivo>();
}
