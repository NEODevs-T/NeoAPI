using System;
using System.Collections.Generic;
using NeoAPI.Models.Neo;

namespace NeoAPI.DTOs.LibroNovedades;

public class LibroNoveDTO
{
    public int IdlibrNov { get; set; }

    public int IdLinea { get; set; }

    public string IdEquipo { get; set; } = null!;

    public string Lndiscrepa { get; set; } = null!;

    public double LntiePerMi { get; set; }

    public string LnfichaRes { get; set; } = null!;

    public DateTime Lnfecha { get; set; }

    public string Lngrupo { get; set; } = null!;

    public string Lnturno { get; set; } = null!;

    public int IdTipoNove { get; set; }

    public int IdAreaCar { get; set; }

    public string? Lnobserv { get; set; }

    public string? IdParada { get; set; }

    public bool LnisPizUni { get; set; }

    public int IdCtpm { get; set; }

    public int? LnisResu { get; set; }

    public int IdMaster { get; set; }

    public string? Linea { get; set; } = null!;

    public string? AreaCarga { get; set; } = null!;

    public void Deconstruct(out int idLinea, out string idEquipo, out DateTime lnfecha, out bool lnisPizUni)
    {
        idLinea = this.IdLinea;
        idEquipo = this.IdEquipo;
        lnfecha = this.Lnfecha;
        lnisPizUni = this.LnisPizUni;
    }
}
