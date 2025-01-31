public class TipoUsuario
{
    public int Id { get; set; }
    public required  string Nombre { get; set; }

    // Relación uno a muchos
    public required  ICollection<Usuario> Usuarios { get; set; }
}