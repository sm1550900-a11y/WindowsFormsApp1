namespace ParkingMauiApp.ViewModels;

public class AuthViewModel : BaseViewModel
{
    private string _name = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _hint = "Введите данные для входа или регистрации";

    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public string Email { get => _email; set => SetProperty(ref _email, value); }
    public string Password { get => _password; set => SetProperty(ref _password, value); }
    public string ConfirmPassword { get => _confirmPassword; set => SetProperty(ref _confirmPassword, value); }
    public string Hint { get => _hint; set => SetProperty(ref _hint, value); }

    public Command LoginCommand => new(async () =>
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            Hint = "Заполните email и пароль";
            return;
        }

        await Shell.Current.GoToAsync("//main");
    });

    public Command RegisterCommand => new(() =>
    {
        Hint = Password == ConfirmPassword ? "Регистрация успешна" : "Пароли не совпадают";
    });
}
