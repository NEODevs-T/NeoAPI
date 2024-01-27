using NeoAPI.Models;
using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.Asentamientos;

public partial class InfoAseDTO

{
    public int IdInfoAse { get; set; }

    public string Iagrupo { get; set; } = null!;

    public string Iaturno { get; set; } = null!;

    public string Iaficha { get; set; } = null!;

    public string? IafichaCor { get; set; }

    public string? Iaobser { get; set; }

    public DateTime IafechCrea { get; set; }
    public DateTime IafechBpcs { get; set; }

    public virtual ICollection<AsentumDTO> AsentaDTO { get; set; } = new List<AsentumDTO>();

}
