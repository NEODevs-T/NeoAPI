using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Pago finales del Bono
/// </summary>
public partial class Pago
{
    public int IdPago { get; set; }

    public int IdTipIncen { get; set; }

    public int IdDeparBon { get; set; }

    /// <summary>
    /// Tipo de moneda del monto
    /// </summary>
    public int IdMonedBon { get; set; }

    /// <summary>
    /// dia que corresponde el pago
    /// </summary>
    public DateTime Pdia { get; set; }

    /// <summary>
    /// Ficha de la persona que recibe el pago
    /// </summary>
    public string Pficha { get; set; } = null!;

    /// <summary>
    /// Nombre de la persona que recibe el pago
    /// </summary>
    public string Pnombre { get; set; } = null!;

    /// <summary>
    /// Monto del dia
    /// </summary>
    public double Pmonto { get; set; }

    /// <summary>
    /// Grupo del dia
    /// </summary>
    public string Pgrupo { get; set; } = null!;

    /// <summary>
    /// Turno del dia
    /// </summary>
    public string Pturno { get; set; } = null!;

    /// <summary>
    /// Fecha en la que se realizo el registro
    /// </summary>
    public DateTime Pfregistro { get; set; }

    public virtual DeparBon IdDeparBonNavigation { get; set; } = null!;

    public virtual MonedBon IdMonedBonNavigation { get; set; } = null!;

    public virtual TipIncen IdTipIncenNavigation { get; set; } = null!;
}
