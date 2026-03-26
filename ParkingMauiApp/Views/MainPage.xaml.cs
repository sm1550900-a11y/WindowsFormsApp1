using ParkingMauiApp.ViewModels;

namespace ParkingMauiApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
