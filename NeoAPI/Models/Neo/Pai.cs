using System;
using System.Collections.Generic;

namespace NeoAPI.Models;

public partial class Pai
{
    public int IdPais { get; set; }

    public string Pnombre { get; set; } = null!;

    public bool Pestado { get; set; }

    public virtual ICollection<Master> Masters { get; set; } = new List<Master>();

    internal object Select(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
