using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class HorarioDisponible
{
    [Key]
    public int HorarioId { get; set; }

    [ForeignKey("Lancha")]
    public int LanchaId { get; set; }
    public required Lancha Lancha { get; set; } // Relación con la tabla Lanchas

    [Required]
    [MaxLength(20)]
    public required string DiaSemana { get; set; } // Lunes, Martes, etc.

    [Required]
    public TimeSpan HoraInicio { get; set; }

    [Required]
    public TimeSpan HoraFin { get; set; }
}
