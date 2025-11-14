using System;
using System.Collections.Generic;

namespace NeoAPI.DTOs.ReunionDiaria;

public class PorcentajeAsistenciaDiariaResponseDTO
{
    
    public double PorcentajeGlobal { get; set; }
    public double PorcentajeGlobalSuplencia { get; set; }
    public List<AsistenReuPorcetanjeDTO> DetallePorCargo { get; set; }
}
