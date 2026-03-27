using ParkingMauiApp.ViewModels;

namespace ParkingMauiApp.Views;

public partial class BookingHistoryPage : ContentPage
{
    public BookingHistoryPage(BookingHistoryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
