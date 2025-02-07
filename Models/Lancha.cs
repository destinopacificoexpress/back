public class Lancha
{
    public int LanchaId { get; set; }
    public required string Nombre { get; set; }
    public required int Capacidad { get; set; }
    public string? Descripcion { get; set; }
    public string Estado { get; set; } = "Operativa"; // Puede ser 'Operativa', 'En Mantenimiento', 'Fuera de Servicio'
}
