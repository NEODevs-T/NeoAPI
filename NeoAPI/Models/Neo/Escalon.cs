using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Escalones del Bono de Produccion
/// </summary>
public partial class Escalon
{
    public int IdEscalon { get; set; }

    /// <summary>
    /// id del area de bonificacion
    /// </summary>
    public int IdAreaBon { get; set; }

    /// <summary>
    /// Numero de escalon
    /// </summary>
    public int Eposicion { get; set; }

    /// <summary>
    /// porcentaje minimo para alcanzar el escalon
    /// </summary>
    public double EporcenMin { get; set; }

    /// <summary>
    /// porcentaje maximo para alcanzar el escalon
    /// </summary>
    public double EporcenMax { get; set; }

    /// <summary>
    /// 1 activo 0 inactivo
    /// </summary>
    public bool Eestado { get; set; }

    /// <summary>
    /// Motivo de la desactivacion
    /// </summary>
    public string? EmotDesc { get; set; }

    /// <summary>
    /// Creador del registro
    /// </summary>
    public string Ecreador { get; set; } = null!;

    /// <summary>
    /// Actualizador del registro
    /// </summary>
    public string? Eactualiz { get; set; }

    /// <summary>
    /// Fecha de creacion del registro
    /// </summary>
    public DateTime EfechaCrea { get; set; }

    /// <summary>
    /// Fecha de actualizacion del registro
    /// </summary>
    public DateTime EfechaActu { get; set; }

    public virtual AreaBon IdAreaBonNavigation { get; set; } = null!;

    public virtual ICollection<MontoBon> MontoBons { get; set; } = new List<MontoBon>();
}
