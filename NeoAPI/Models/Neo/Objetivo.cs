using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Tabla de Objetivos de Produccion
/// </summary>
public partial class Objetivo
{
    /// <summary>
    /// Identificacion del objetivo
    /// </summary>
    public int IdObjetivo { get; set; }

    /// <summary>
    /// Identificacion de la mestra
    /// </summary>
    public int IdMaster { get; set; }

    /// <summary>
    /// Codigo del producto
    /// </summary>
    public string OcodProd { get; set; } = null!;

    /// <summary>
    /// Valor del objetivo
    /// </summary>
    public double Ovalor { get; set; }

    /// <summary>
    /// Ficha de la persona que cargo el objetivo
    /// </summary>
    public string Ocargador { get; set; } = null!;

    /// <summary>
    /// Ficha de la persona que solicito la creacion del objetivo
    /// </summary>
    public string Osolicitante { get; set; } = null!;

    /// <summary>
    /// Fecha de Creación del objetivo
    /// </summary>
    public DateTime OfechCrea { get; set; }

    /// <summary>
    /// identificacion del objetivo a la que sustituyo este registro
    /// </summary>
    public int? OcodSustit { get; set; }

    /// <summary>
    /// Estado del objetivo (1: Activo, 0: Inactivo)
    /// </summary>
    public bool Oestado { get; set; }

    /// <summary>
    /// Razon por lo cual se desactivo
    /// </summary>
    public string? OrazDesac { get; set; }

    /// <summary>
    /// unidad
    /// </summary>
    public int IdUnidaObj { get; set; }

    public virtual Master IdMasterNavigation { get; set; } = null!;

    public virtual UnidaObj IdUnidaObjNavigation { get; set; } = null!;
}
