using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.ViewModels;

public partial class ScheduleViewModel : ObservableObject{
    [ObservableProperty]
    private ObservableCollection<Schedule> _currentSchedule;

    [ObservableProperty]
    private List<Station> _stations;

    [ObservableProperty]
    private Station _departureStation;

    [ObservableProperty]
    private Station _arrivalStation;

    [ObservableProperty]
    private DateTime _departureDate = DateTime.Now;

    [ObservableProperty]
    private DateTime? _arrivalDate = null;

    private readonly IApiService _apiService;
    public ScheduleViewModel(IApiService apiService) {
        _apiService = apiService;
        CurrentSchedule = [];
        Stations = [];
        LoadSchedule();
        LoadStations();
        DepartureStation = new();
        ArrivalStation = new();
    }

    [RelayCommand]
    public async Task SearchTicket() {
        if (DepartureStation != null && ArrivalStation != null && DepartureDate >= DateTime.Now && DepartureStation != ArrivalStation) {
            var searchSchedulePattern = new Schedule {
                Departure_station = DepartureStation.Name,
                Arrival_station = ArrivalStation.Name,
                Departure_date_time = DepartureDate
            };
            var result = await _apiService.SearchSchedulesAsync(searchSchedulePattern, ArrivalDate);
            if (result.Count == 0)
                return;
            CurrentSchedule.Clear();
            foreach (var item in result) {
                if (item.Number_of_available_seats > 0)
                    CurrentSchedule.Add(item);
            }
        }
    }

    [RelayCommand]
    public async Task GetSchedule() {
        LoadSchedule();
    }

    private async void LoadSchedule() {
        CurrentSchedule.Clear();
        var response = await _apiService.GetScheduleAsync();
        foreach (var item in response) {
            if (item.Number_of_available_seats > 0)
                CurrentSchedule.Add(item);
        }
    }

    private async void LoadStations() {
        Stations = await _apiService.GetStationsAsync();
        DepartureStation = Stations[0];
        ArrivalStation = Stations[1];
    }
}
