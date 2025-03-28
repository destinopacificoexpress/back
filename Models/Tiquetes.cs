// Tiquete.cs
using System.ComponentModel.DataAnnotations;

// public class Tiquete
// {
//     [Key]
//     public int TiqueteId { get; set; }
//     public DateTime FechaExpedicion { get; set; }
//     public required string NumeroTiquete { get; set; }
//     public int TipoTiqueteId { get; set; }
//     public int AgenciaId { get; set; }
//     public int GrupoId { get; set; }
//     public int DestinoId { get; set; }
//     public DateTime FechaAbordo { get; set; }
//     public DateTime? FechaRetorno { get; set; }
//     public bool SoloIda { get; set; }
//     public int CantidadPasajeros { get; set; }
//     public decimal ValorSugerido { get; set; }
//     public decimal ValorIndividual { get; set; }
//     public bool ExcesoEquipaje { get; set; }
//     public decimal TotalVenta { get; set; }
//     public int FormaPagoId { get; set; }
//     public  required string Descripcion { get; set; }
//     public int PasajeroId { get; set; }
//     // public List<Pasajero> Pasajeros { get; set; }

// }



// TipoTiquete.cs
// public class TipoTiquete
// {
//     public int Id { get; set; }
//     public string Nombre { get; set; }
// }

// public class Tiquete
// {
//     public int TiqueteId { get; set; }
//     public DateTime FechaExpedicion { get; set; }
//     public string NumeroTiquete { get; set; } = string.Empty;

//     // Relación con TipoTiquete
//     public int TipoTiqueteId { get; set; }
//     public TipoTiquete? TipoTiquete { get; set; } = null!;

//     // Relación con Agencia
//     public int AgenciaId { get; set; }
//     public Agencias? Agencia { get; set; } = null!;

//     // Relación con Grupo
//     public int GrupoId { get; set; }
//     public Grupos? Grupo { get; set; } = null!;

//     // Relación con Destino
//     public int DestinoId { get; set; }
//     public InfoDestino? Destino { get; set; } = null!;

//     // Relación con FormaPago
//     public int FormaPagoId { get; set; }
//     public FormaPago? FormaPago { get; set; } = null!;

//     // Otros campos
//     public DateTime FechaAbordo { get; set; }
//     public DateTime? FechaRetorno { get; set; }
//     public bool SoloIda { get; set; }
//     public int CantidadPasajeros { get; set; }
//     public decimal ValorSugerido { get; set; }
//     public decimal ValorIndividual { get; set; }
//     public bool ExcesoEquipaje { get; set; }
//     public decimal TotalVenta { get; set; }
//     public string? Descripcion { get; set; }

//     public int PasajeroId {get; set;}
//      public Pasajero? Pasajero { get; set; }
// }

public class Tiquete
{
    public int TiqueteId { get; set; }

    [Required]
    public DateTime FechaExpedicion { get; set; }

    [Required]
    public string NumeroTiquete { get; set; } = string.Empty;

    // Relación con TipoTiquete
    [Required]
    public int TipoTiqueteId { get; set; }
    public TipoTiquete TipoTiquete { get; set; } = null!;

    // Relación con Agencia
    [Required]
    public int AgenciaId { get; set; }
    public Agencias Agencia { get; set; } = null!;

    // Relación con Grupo
    [Required]
    public int GrupoId { get; set; }
    public Grupos Grupo { get; set; } = null!;

    // Relación con Destino
    [Required]
    public int InfoDestinoId { get; set; }
    public InfoDestino InfoDestinos { get; set; } = null!;

    // Relación con FormaPago
    [Required]
    public int FormaPagoId { get; set; }
    public FormaPago FormaPago { get; set; } = null!;

    // Otros campos
    [Required]

    public DateTime FechaAbordo { get; set; }

    [Required]
    public string HoraAbordo{ get; set; }
    public DateTime? FechaRetorno { get; set; }

    public string? HoraRetorno{ get; set; }

    [Required]
    public bool SoloIda { get; set; }

    [Required]
    public int CantidadPasajeros { get; set; }

    [Required]
    public decimal ValorSugerido { get; set; }

    [Required]
    public decimal ValorIndividual { get; set; }

    [Required]
    public bool ExcesoEquipaje { get; set; }

    [Required]
    public decimal TotalVenta { get; set; }

    [Required]
    public string? Descripcion { get; set; }

    // Relación con Pasajero
    [Required]
    public int PasajeroId { get; set; }
    public Pasajero? Pasajeros { get; set; } // Propiedad de navegación opcional
    public int? ViajeId { get; set; }
    public Viaje Viajes { get; set; }
}
