using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class LibroNovedadesV
{
    public int Id { get; set; }

    public string Pais { get; set; } = null!;

    public string Empresa { get; set; } = null!;

    public string Centro { get; set; } = null!;

    public string Division { get; set; } = null!;

    public string Linea { get; set; } = null!;

    public string CodigoEquipo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string TipoDeNovedad { get; set; } = null!;

    public string Discrepancia { get; set; } = null!;

    public int TiempoPerdido { get; set; }

    public string FichaDelRegistrador { get; set; } = null!;

    public string Grupo { get; set; } = null!;

    public string Turno { get; set; } = null!;

    public string? Observacion { get; set; }

    public string AreaCargador { get; set; } = null!;

    public string ClasificacionTpm { get; set; } = null!;

    public DateTime Lnfecha { get; set; }

    public int IdPais { get; set; }

    public int IdEmpresa { get; set; }

    public int IdCentro { get; set; }

    public int IdDivision { get; set; }

    public int IdLinea { get; set; }

    public string IsReunion { get; set; } = null!;
}
