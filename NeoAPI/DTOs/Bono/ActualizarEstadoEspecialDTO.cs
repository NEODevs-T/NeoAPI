namespace NeoAPI.DTOs.Bono;

public class ActualizarEstadoEspecialDto
{
    public int NuevoIdEstado { get; set; }
    public int Nivel { get; set; }
    public string UsuarioAprobador { get; set; } = null!;
    public string? Comentario { get; set; }
}