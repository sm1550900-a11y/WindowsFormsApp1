using Microsoft.Extensions.Logging;
using ParkingMauiApp.Services;
using ParkingMauiApp.ViewModels;
using ParkingMauiApp.Views;

namespace ParkingMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IParkingService, InMemoryParkingService>();

        builder.Services.AddSingleton<WelcomeViewModel>();
        builder.Services.AddSingleton<AuthViewModel>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<BookingHistoryViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();

        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<AuthPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<BookingHistoryPage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<SettingsPage>();

        return builder.Build();
    }
}
