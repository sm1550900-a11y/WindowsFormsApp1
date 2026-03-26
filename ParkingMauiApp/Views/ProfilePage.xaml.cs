using ParkingMauiApp.ViewModels;

namespace ParkingMauiApp.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
