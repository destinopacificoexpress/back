using System.ComponentModel.DataAnnotations;

public class Sesion
{
    [Key]
    public int SesionId { get; set; }
    public int UsuarioId { get; set; }
    // public  string Usuario { get; set; }
    public required string Token { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public bool Activa { get; set; }
}
