namespace ParkingMauiApp.Models;

public class Booking
{
    public string SpotId { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public decimal Cost { get; set; }
    public string Status { get; set; } = "Активна";
}
