using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CargoReu
{
    public int IdCargoR { get; set; }

    public string Crnombre { get; set; } = null!;

    public bool Cresta { get; set; }

    public string Crempresa { get; set; } = null!;

    public string? Crarea { get; set; }

    public int? Crbloque { get; set; }

    public virtual ICollection<AsistenReu> AsistenReus { get; set; } = new List<AsistenReu>();
}
