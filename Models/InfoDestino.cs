
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class InfoDestino
{
     [Key]
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public int GrupoId { get; set; }
     public Grupos Grupos { get; set; }  = null!; // Relación inversa
}