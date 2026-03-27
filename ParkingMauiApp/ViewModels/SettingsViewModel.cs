namespace ParkingMauiApp.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private bool _notificationsEnabled = true;
    private bool _darkThemeEnabled;

    public bool NotificationsEnabled { get => _notificationsEnabled; set => SetProperty(ref _notificationsEnabled, value); }
    public bool DarkThemeEnabled { get => _darkThemeEnabled; set => SetProperty(ref _darkThemeEnabled, value); }
}
