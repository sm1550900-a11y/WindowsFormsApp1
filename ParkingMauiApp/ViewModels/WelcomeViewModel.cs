namespace ParkingMauiApp.ViewModels;

public class WelcomeViewModel : BaseViewModel
{
    public Command GoToLoginCommand => new(async () => await Shell.Current.GoToAsync("//auth"));
    public Command GoToRegisterCommand => new(async () => await Shell.Current.GoToAsync("//auth"));
}
