// Pasajero.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pasajero
{
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


public class PasajerosCortecia {
    [Key]
    public int PasajerosCorteciasId { get; set; }
    public string documento { get; set; }
    public string Nombre { get; set; }

    [ForeignKey("PasajeroId")]
    public int PasajeroId { get; set; }
    public DateTime FechaOtorgada { get; set; }
    // public Pasajero Pasajero { get; set; }
}



public class Aprobador {
    [Key]
    public int aprobadorId { get; set; }
    public string Nombre { get; set; }
    public string Cargo { get; set; }

    [ForeignKey("AgenciaId")]
    public int AgenciaId { get; set; }
    // public Agencias Agencia { get; set; }
}