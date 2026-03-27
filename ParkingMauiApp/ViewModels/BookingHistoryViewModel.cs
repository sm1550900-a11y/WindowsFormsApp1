using System.Collections.ObjectModel;
using ParkingMauiApp.Models;
using ParkingMauiApp.Services;

namespace ParkingMauiApp.ViewModels;

public class BookingHistoryViewModel : BaseViewModel
{
    public ObservableCollection<Booking> Items { get; }

    public BookingHistoryViewModel(IParkingService parkingService)
    {
        Items = new ObservableCollection<Booking>(parkingService.GetBookingHistory());
    }
}
