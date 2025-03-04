using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cortesia {
    [Key]
    public int CortesiaId { get; set; }
    public DateTime FechaOtorgada { get; set; }

    [ForeignKey("VendedorId")]
    public int VendedorId { get; set; }

    [ForeignKey("PasajeroAutorizadoId")]
    public int PasajeroAutorizadoId { get; set; }

    [ForeignKey("aprobadorId")]
    public int aprobadorId { get; set; }

    [ForeignKey("AgenciaId")]
    public int AgenciaId { get; set; }
    public string Motivo { get; set; }
    public string Estado { get; set; }
    public int modificada { get; set; }
    // public Aprobador Aprobador { get; set; }
    // public PasajerosCortecia PasajerosCortecia { get; set; }
    // public Agencias Agencia { get; set; }
}
