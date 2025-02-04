public class Viaje
{
    public int ViajeId { get; set; }
    public int? LanchaId { get; set; }
    public int LugarPartidaId { get; set; }
    public int DestinoId { get; set; }
    public DateTime FechaViaje { get; set; }
    // public DateTime FechaRetorno { get; set; }
    public TimeSpan HoraSalida { get; set; }
    public TimeSpan HoraRetorno { get; set; }
    
    public required string Estado { get; set; } // -- Activo, Cancelado, Finalizado
}

