using System.ComponentModel.DataAnnotations;

public class Agencias
{
    [Key]
    public int AgenciaId { get; set; } // ID en formato texto
    public string Nit { get; set; } = string.Empty; // ID en formato texto
    public string NombreAgencia { get; set; } = string.Empty;
    public string DireccionAgencia { get; set; } = string.Empty;
    public string? TelefonoAgencia { get; set; }
    public string? EmailAgencia { get; set; }
    public string? CupoAsignado { get; set; }
    public string? ImpuestoCartera { get; set; }
    public string? CupoAcumulado { get; set; }
    public string? Cortecias { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}