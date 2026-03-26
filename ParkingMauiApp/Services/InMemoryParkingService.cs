using ParkingMauiApp.Models;

namespace ParkingMauiApp.Services;

public class InMemoryParkingService : IParkingService
{
    private readonly List<ParkingSpot> _spots = new()
    {
        new() { Id = "A1", PricePerHour = 100, Status = SpotStatus.Free },
        new() { Id = "A2", PricePerHour = 100, Status = SpotStatus.Busy },
        new() { Id = "B1", PricePerHour = 80, Status = SpotStatus.Booked },
        new() { Id = "B2", PricePerHour = 80, Status = SpotStatus.Free }
    };

    private readonly List<Booking> _history = new();

    public IList<ParkingSpot> GetSpots() => _spots;
    public IList<Booking> GetBookingHistory() => _history.OrderByDescending(x => x.Start).ToList();

    public void BookSpot(string spotId, DateTime from, DateTime to)
    {
        var spot = _spots.First(x => x.Id == spotId);
        if (spot.Status != SpotStatus.Free)
            throw new InvalidOperationException("Место недоступно");

        var hours = Math.Max(1, (to - from).Hours);
        spot.Status = SpotStatus.Booked;
        _history.Add(new Booking
        {
            SpotId = spotId,
            Start = from,
            End = to,
            Cost = hours * spot.PricePerHour,
            Status = "Забронировано"
        });
    }
}
