using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Montos de Bonificacion
/// </summary>
public partial class MontoBon
{
    public int IdMontoBon { get; set; }

    /// <summary>
    /// Id de tipo de incidencia
    /// </summary>
    public int IdTipIncen { get; set; }

    /// <summary>
    /// Id maestra
    /// </summary>
    public int IdMaster { get; set; }

    /// <summary>
    /// Id Nivel de puesto
    /// </summary>
    public int IdNivePues { get; set; }

    /// <summary>
    /// Id del escalon
    /// </summary>
    public int IdEscalon { get; set; }

    /// <summary>
    /// Id Moneda
    /// </summary>
    public int IdMonedBon { get; set; }

    /// <summary>
    /// Monto a cancelar
    /// </summary>
    public double Mmonto { get; set; }

    /// <summary>
    /// Fecha de creacion del registro
    /// </summary>
    public DateTime Mfcreacion { get; set; }

    /// <summary>
    /// usuario creador del registro
    /// </summary>
    public string MusuaCread { get; set; } = null!;

    /// <summary>
    /// 1 activo, 0: Inactivo
    /// </summary>
    public bool Mestado { get; set; }

    /// <summary>
    /// Motivo de la desactivación
    /// </summary>
    public string? MmotDesac { get; set; }

    /// <summary>
    /// Usuario que desactivo el registro
    /// </summary>
    public string? MusuaDesac { get; set; }

    /// <summary>
    /// Fecha de desactivación
    /// </summary>
    public DateTime? Mfdesac { get; set; }

    public virtual Escalon IdEscalonNavigation { get; set; } = null!;

    public virtual Master IdMasterNavigation { get; set; } = null!;

    public virtual MonedBon IdMonedBonNavigation { get; set; } = null!;

    public virtual NivePue IdNivePuesNavigation { get; set; } = null!;

    public virtual TipIncen IdTipIncenNavigation { get; set; } = null!;
}
