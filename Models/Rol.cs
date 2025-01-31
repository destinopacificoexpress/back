using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Roles
{
    [Key]
    public int RoleId { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public ICollection<UsuarioRoles> UsuarioRoles { get; set; }
    public ICollection<RolePermisos> RolePermisos { get; set; }
}


public class UsuarioRoles
{
    [Key]
    public int UsuarioId { get; set; }
    public int RoleId  { get; set; }
    // public Usuario Usuario { get; set; }
    // public int RoleId { get; set; }
    // public Roles Rol { get; set; }
    
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuarios { get; set; }
    [ForeignKey("RoleId")]
    public virtual Roles Role { get; set; }
}

// public class Role
// {
//     public int RoleId { get; set; }
//     public string Nombre { get; set; }
//     public string Descripcion { get; set; }

//     public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; }
// }