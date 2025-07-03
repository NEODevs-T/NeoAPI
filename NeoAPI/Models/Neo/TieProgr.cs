using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Tiempo programdos de las maquina
/// </summary>
public partial class TieProgr
{
    /// <summary>
    /// identificacion del tiempo programado
    /// </summary>
    public int IdTieProgr { get; set; }

    /// <summary>
    /// identificacion de la maestra
    /// </summary>
    public int IdMaster { get; set; }

    /// <summary>
    /// Fecha de las horas programadas
    /// </summary>
    public DateTime TpfechaPro { get; set; }

    public string Tpturno { get; set; } = null!;

    /// <summary>
    /// Horas programadas
    /// </summary>
    public double TphorasPro { get; set; }

    /// <summary>
    /// Justificacion de las horas programadas
    /// </summary>
    public string Tpjustific { get; set; } = null!;

    /// <summary>
    /// Ficha del usuario que creo el registro
    /// </summary>
    public string Tpusuario { get; set; } = null!;

    /// <summary>
    /// Fecha de creción del registro
    /// </summary>
    public DateTime TpfechaReg { get; set; }

    /// <summary>
    /// Fecha de modificacion del registro
    /// </summary>
    public DateTime TpfechaAct { get; set; }

    public virtual Master IdMasterNavigation { get; set; } = null!;
}
