using System.ComponentModel.DataAnnotations;

namespace NeoAPI.Models.NeoVieja
{ 
        public partial class EquipoEamViejo
    {
            public int IdEquipo { get; set; }
            [Required(ErrorMessage = "Campo Requerido.")]
            public int IdLinea { get; set; }
            [Required(ErrorMessage = "Campo Requerido.")]
            public string EcodEquiEam { get; set; } = null!;
            [Required(ErrorMessage = "Campo Requerido.")]
            public string EnombreEam { get; set; } = null!;
            [Required(ErrorMessage = "Campo Requerido.")]
            public string EdescriEam { get; set; } = null!;
            [Required(ErrorMessage = "Campo Requerido.")]
            public bool EestaEam { get; set; }  
    }
}
