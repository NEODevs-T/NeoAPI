using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NeoAPI.Models.Neo;

public partial class Pai
{
    public int IdPais { get; set; }

    public string Pnombre { get; set; } = null!;

    public bool Pestado { get; set; }

    public DateTime Pfecha { get; set; }

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();
}
