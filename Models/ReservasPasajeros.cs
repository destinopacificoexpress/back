public class ReservaPasajero
{
    public int ReservaId { get; set; }

    public int HorarioId { get; set; }
    public Viaje Viajes { get; set; } = null!;

    public int PasajeroId { get; set; }
    public Pasajero Pasajero { get; set; } = null!;

    public int CantidadPasajeros { get; set; }
    public string Estado { get; set; } = "Reservado"; // 'Reservado', 'Cancelado'
}
