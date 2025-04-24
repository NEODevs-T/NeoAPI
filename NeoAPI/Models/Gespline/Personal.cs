using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Gespline;

public partial class Personal
{
    public string Codigopersonal { get; set; } = null!;

    public string? Cedula { get; set; }

    public byte[]? Foto { get; set; }

    public string? Tipodocumento { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Sexo { get; set; }

    public DateTime? Fechanacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefonos { get; set; }

    public string? Codigosueldo { get; set; }

    public string? Codidocargospersonal { get; set; }

    public int? Estado { get; set; }

    public string? Codbarras { get; set; }

    public DateTime? Fechaingreso { get; set; }

    public string? Experienciacargos1 { get; set; }

    public string? Experienciacargos2 { get; set; }

    public string? Experienciacargos3 { get; set; }

    public string? Estudios { get; set; }

    public string? Cursos { get; set; }

    public DateTime? Timespan { get; set; }

    public virtual Tipocargospersonal? CodidocargospersonalNavigation { get; set; }

    public virtual ICollection<Entradaejecucion> Entradaejecucions { get; set; } = new List<Entradaejecucion>();

    public virtual ICollection<Paradasejecutada> Paradasejecutada { get; set; } = new List<Paradasejecutada>();
}
