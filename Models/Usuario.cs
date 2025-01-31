
    // public class Usuario
    // {
    //     public int id { get; set; }
    //     public string? Name { get; set; }
    //     public string? LastName { get; set; }
    //     public required string Password { get; set; }
    //     public string? Second_Password { get; set; }
    //     public DateTime? FechaCreacion { get; set; }
    //     public required string Email { get; set; }
    //     // public string Email { get; set; }
    // }

public class Usuario
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string Second_Password { get; set; }
    public required string Email { get; set; }
    public DateTime FechaCreacion { get; set; }
    public required int RoleId { get; set; }

// Foreign Key

    public ICollection<Sesion> Sesiones { get; set; }
}