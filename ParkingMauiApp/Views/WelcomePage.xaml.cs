using ParkingMauiApp.ViewModels;

namespace ParkingMauiApp.Views;

public partial class WelcomePage : ContentPage
{
    public WelcomePage(WelcomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
