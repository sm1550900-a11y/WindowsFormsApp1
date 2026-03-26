using ParkingMauiApp.Models;

namespace ParkingMauiApp.Services;

public interface IParkingService
{
    IList<ParkingSpot> GetSpots();
    IList<Booking> GetBookingHistory();
    void BookSpot(string spotId, DateTime from, DateTime to);
}
