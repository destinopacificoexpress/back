using System.ComponentModel.DataAnnotations;

public class TipoDocumento
{
    [Key]
    public int TipoDocumentoId { get; set; }
    public required string Nombre { get; set; }
    public string? Descripcion { get; set; }

    // public required ICollection<Pasajero> Pasajeros { get; set; }
}