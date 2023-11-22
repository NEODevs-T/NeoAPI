using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Variable
{
    public int IdVariable { get; set; }

    public int IdTipoVar { get; set; }

    public int IdUnidad { get; set; }

    public int IdEquiMedi { get; set; }

    public int IdSeccion { get; set; }

    public int IdEstandar { get; set; }

    public int IdClasiVar { get; set; }

    public string Vnombre { get; set; } = null!;

    public string? Vdescri { get; set; }

    public bool VisObser { get; set; }

    public DateOnly VfechaCrea { get; set; }

    public bool Vestado { get; set; }

    public virtual ClasiVar IdClasiVarNavigation { get; set; } = null!;

    public virtual EquiMedi IdEquiMediNavigation { get; set; } = null!;

    public virtual Estandar IdEstandarNavigation { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;

    public virtual TipoVar IdTipoVarNavigation { get; set; } = null!;

    public virtual Unidad IdUnidadNavigation { get; set; } = null!;

    public virtual ICollection<Rango> Rangos { get; set; } = new List<Rango>();
}
