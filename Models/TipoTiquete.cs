
using System.ComponentModel.DataAnnotations;

public class TipoTiquete
{
    [Key]
    public int TipoTiqueteId { get; set; }

    public required string NombreTipo { get; set; }

    public required string Descripcion { get; set; }

    public bool Incluye_Impuestos { get; set; }

}

