using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Puestosdetrabajo
{
    public string Codigopuesto { get; set; } = null!;

    public string? Nombrepuesto { get; set; }

    public int? Numerooperarios { get; set; }

    public int? Tipopuestotrabajo { get; set; }

    public string? Velocidad { get; set; }

    public string? CargaAmp { get; set; }

    public double? Horamantenimientotrabajo { get; set; }

    public double? Horasmantenimientocalendario { get; set; }

    public string? Usuario { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public int? Estado { get; set; }

    public string? Estadoequipos { get; set; }

    public DateTime? Timespan { get; set; }

    public string? Iddispositivo { get; set; }

    public string? Codigocelula { get; set; }

    public string? Codigotecnologia { get; set; }

    public string? Planta { get; set; }

    public decimal? Toleranciadesperdiciodigitado { get; set; }

    public decimal? Toleranciadesperdiciopuestaenmarcha { get; set; }

    public decimal? Toleranciadesperdiciomaterialppal { get; set; }

    public bool? Decidecelula { get; set; }

    public virtual ICollection<Puestotrabajosegunproducto> Puestotrabajosegunproductos { get; set; } = new List<Puestotrabajosegunproducto>();

    public virtual ICollection<Standarparada> Standarparada { get; set; } = new List<Standarparada>();

    public virtual Transmicionweb? Transmicionweb { get; set; }
}
