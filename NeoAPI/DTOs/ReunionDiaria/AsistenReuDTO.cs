using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class AsistenReuDTO
{
    public string Ararea { get; set; } = null!;

    public int IdAsistencia { get; set; }

    public DateTime? Arfecha { get; set; }

    public int IdCargoR { get; set; }

    public int ArAsistente { get; set; }

    public int? ArSuplente { get; set; }

    public string? ArObser { get; set; }

    public CargoReuDTO? Cargo {get; set; }
}
