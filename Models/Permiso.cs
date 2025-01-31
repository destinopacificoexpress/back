using System.ComponentModel.DataAnnotations;

public class Permisos
{
    [Key]
    public int PermisoId { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public ICollection<RolePermisos> RolePermisos { get; set; }
}


public class RolePermisos
{

    [Key]
    public int RoleId { get; set; }
    public required Roles Rol { get; set; }
    public int PermisoId { get; set; }
    public required Permisos Permisos { get; set; }
}