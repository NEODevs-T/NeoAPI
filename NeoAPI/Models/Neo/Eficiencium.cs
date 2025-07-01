using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

/// <summary>
/// Tabla de eficiencia de producción
/// </summary>
public partial class Eficiencium
{
    /// <summary>
    /// identificación de la eficiencia
    /// </summary>
    public int IdEficienc { get; set; }

    /// <summary>
    /// identificacion de la maestra
    /// </summary>
    public int IdMaster { get; set; }

    /// <summary>
    /// Fecha de la eficiencia
    /// </summary>
    public DateOnly EfechaEfic { get; set; }

    /// <summary>
    /// Grupo al cual se le asinga la eficiencia
    /// </summary>
    public string Egrupo { get; set; } = null!;

    /// <summary>
    /// Turno rotativo
    /// </summary>
    public string Eturno { get; set; } = null!;

    /// <summary>
    /// Porcentaje de la eficiencia
    /// </summary>
    public double Evalor { get; set; }

    /// <summary>
    /// Objetivo resultante teniendo en cuenta los tiempos programados
    /// </summary>
    public double Eobjetivo { get; set; }

    /// <summary>
    /// Ficha del usuario creador del registro
    /// </summary>
    public string Eusuario { get; set; } = null!;

    /// <summary>
    /// Fecha de creacion del registro
    /// </summary>
    public DateTime EfechaCrea { get; set; }

    /// <summary>
    /// Fecha de actualización del registro
    /// </summary>
    public DateTime EfechaAct { get; set; }

    public double EprodReal { get; set; }

    public double EprodAdici { get; set; }

    public virtual Master IdMasterNavigation { get; set; } = null!;
}
