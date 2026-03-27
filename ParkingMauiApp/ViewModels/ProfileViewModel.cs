namespace ParkingMauiApp.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private string _name = "Иван Петров";
    private string _email = "ivan@example.com";

    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public string Email { get => _email; set => SetProperty(ref _email, value); }

    public Command SaveCommand => new(async () => await Application.Current!.MainPage!.DisplayAlert("Профиль", "Данные обновлены", "OK"));
}
