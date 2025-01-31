// Pasajero.cs
using System.ComponentModel.DataAnnotations;

public class Pasajero
{
    [Key]
    public int PasajeroId { get; set; }
    public required int TipoDocumentoId  { get; set; }
    public required string Documento { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public int Edad { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;
    public bool Activo { get; set; } = true;


}
