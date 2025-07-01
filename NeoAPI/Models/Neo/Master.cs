using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class Master
{
    public int IdMaster { get; set; }

    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public int IdPais { get; set; }

    public int IdEmpresa { get; set; }

    public int IdLinea { get; set; }

    public virtual ICollection<Eficiencium> Eficiencia { get; set; } = new List<Eficiencium>();

    public virtual ICollection<FechaProg> FechaProgs { get; set; } = new List<FechaProg>();

    public virtual Centro IdCentroNavigation { get; set; } = null!;

    public virtual Division IdDivisionNavigation { get; set; } = null!;

    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    public virtual Linea IdLineaNavigation { get; set; } = null!;

    public virtual Pai IdPaisNavigation { get; set; } = null!;

    public virtual ICollection<LibroNove> LibroNoves { get; set; } = new List<LibroNove>();

    public virtual ICollection<MontoBon> MontoBons { get; set; } = new List<MontoBon>();

    public virtual ICollection<Nivel> Nivels { get; set; } = new List<Nivel>();

    public virtual ICollection<Objetivo> Objetivos { get; set; } = new List<Objetivo>();

    public virtual ICollection<ProNoCon> ProNoCons { get; set; } = new List<ProNoCon>();

    public virtual ICollection<Rango> Rangos { get; set; } = new List<Rango>();

    public virtual ICollection<Reunion> Reunions { get; set; } = new List<Reunion>();

    public virtual ICollection<TieProgr> TieProgrs { get; set; } = new List<TieProgr>();
}
