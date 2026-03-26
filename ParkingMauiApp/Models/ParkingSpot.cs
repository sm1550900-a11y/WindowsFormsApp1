namespace ParkingMauiApp.Models;

public class ParkingSpot
{
    public string Id { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public SpotStatus Status { get; set; }
}

public enum SpotStatus
{
    Free,
    Busy,
    Booked
}
