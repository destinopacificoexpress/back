using System.ComponentModel.DataAnnotations;

public class Grupos
{
    [Key]
    public int GrupoId { get; set; }
    public required string NombreGrupo { get; set; }
    public  ICollection<InfoDestino> InfoDestino { get; set; } = new List<InfoDestino>(); // Relación
}
