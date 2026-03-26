using System.Collections.ObjectModel;
using ParkingMauiApp.Models;
using ParkingMauiApp.Services;

namespace ParkingMauiApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IParkingService _parkingService;
    private string _result = "Выберите свободное место";

    public ObservableCollection<ParkingSpot> Spots { get; }
    public string Result { get => _result; set => SetProperty(ref _result, value); }

    public MainViewModel(IParkingService parkingService)
    {
        _parkingService = parkingService;
        Spots = new ObservableCollection<ParkingSpot>(_parkingService.GetSpots());
    }

    public Command<ParkingSpot> BookCommand => new(spot =>
    {
        if (spot is null) return;

        try
        {
            _parkingService.BookSpot(spot.Id, DateTime.Now, DateTime.Now.AddHours(2));
            Result = $"Место {spot.Id} забронировано";
            Refresh();
        }
        catch (Exception ex)
        {
            Result = ex.Message;
        }
    });

    private void Refresh()
    {
        Spots.Clear();
        foreach (var spot in _parkingService.GetSpots())
            Spots.Add(spot);
    }
}
