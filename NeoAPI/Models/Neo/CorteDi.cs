﻿using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Neo;

public partial class CorteDi
{
    public int IdCorteDis { get; set; }

    public int IdCategori { get; set; }

    public string CdaccCorr { get; set; } = null!;

    public bool CdisListo { get; set; }

    public int IdAsenta { get; set; }

    public bool CdisLibro { get; set; }

    public DateTime? CdfechAcci { get; set; }

    public DateTime? CdfechList { get; set; }

    public virtual Asentum IdAsentaNavigation { get; set; } = null!;

    public virtual Categori IdCategoriNavigation { get; set; } = null!;
}
