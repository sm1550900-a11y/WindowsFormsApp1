using System.Globalization;
using ParkingMauiApp.Models;

namespace ParkingMauiApp.Converters;

public class SpotStatusToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not SpotStatus status)
            return Colors.Gray;

        return status switch
        {
            SpotStatus.Free => Color.FromArgb("#28A745"),
            SpotStatus.Busy => Color.FromArgb("#D9534F"),
            _ => Color.FromArgb("#F39C12")
        };
    }

    public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
