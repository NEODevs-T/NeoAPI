using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public partial class AsistenReuPorcetanjeDTO
{
    public int IdCargoR { get; set; }
    public string Nombre { get; set; }
    public int ReunionesProgramadas { get; set; }
    public int ReunionesAsistidas { get; set; }
    public double  PorcentajeAsistencia { get; set; }
}