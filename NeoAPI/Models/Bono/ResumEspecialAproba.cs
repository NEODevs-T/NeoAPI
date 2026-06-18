using System;
using System.Collections.Generic;

namespace NeoAPI.Models.Bono;

public partial class ResumEspecialAproba
{
    public int IdAprobacion { get; set; }

    public int IdEspecial { get; set; }

    public int Nivel { get; set; }

    public string UsuarioAprobador { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public string? Comentario { get; set; }

    public DateTime FechaAccion { get; set; }

    public virtual ResumEspecial IdEspecialNavigation { get; set; } = null!;
}
