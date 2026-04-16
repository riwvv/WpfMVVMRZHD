using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.ViewModels;

public partial class AddScheduleViewModel : ObservableObject {
    [ObservableProperty]
    private List<Station> _stations;
    
    [ObservableProperty]
    private Station _departureStation;

    [ObservableProperty]
    private Station _arrivalStation;

    [ObservableProperty]
    private int _numberSeats = 0;

    [ObservableProperty]
    private DateTime _departureDate = DateTime.Now;

    [ObservableProperty]
    private DateTime _arrivalDate = DateTime.Now.AddDays(7);

    [ObservableProperty]
    private decimal _price = 100;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private Brush _colorMessage = Brushes.Black;

    private readonly AdminScheduleViewModel _admin;
    private readonly IApiService _apiService;
    public AddScheduleViewModel(IApiService apiService, AdminScheduleViewModel adminScheduleViewModel) {
        _apiService = apiService;
        LoadData();
        _admin = adminScheduleViewModel;
    }

    [RelayCommand]
    public async Task SendData() {
        if (DepartureStation != ArrivalStation && NumberSeats > 0 && NumberSeats <= 100 && DepartureDate >= DateTime.Now && ArrivalDate > DepartureDate && Price >= 500) {
            var newSchedule = new Schedule {
                Departure_station = DepartureStation.Name,
                Arrival_station = ArrivalStation.Name,
                Departure_date_time = DepartureDate,
                Arrival_date_time = ArrivalDate,
                Number_of_available_seats = NumberSeats,
                Ticket_price = Price,
            };
            try {
                var result = await _apiService.CreateScheduleAsync(newSchedule);
                if (result) {
                    DepartureStation = Stations[0];
                    ArrivalStation = Stations[1];
                    DepartureDate = DateTime.Now;
                    ArrivalDate = DateTime.Now.AddDays(7);
                    NumberSeats = 0;
                    Price = 100;
                    ColorMessage = Brushes.LightGreen;
                    ErrorMessage = "Запись успешно добавлена";
                    _admin.OnAddSchedule?.Invoke();
                    return;
                }
                ColorMessage = Brushes.Red;
                ErrorMessage = "Не удалось создать запись\n";
            }
            catch (Exception ex) {
                ColorMessage = Brushes.Red;
                ErrorMessage = $"Ошибка: {ex.Message}\n";
            }
        }
        ErrorMessage += "ВАЖНО корректно заполнить все поля:\n1. Станции не могут быть одинаковыми\n2. Кол-во мест должно быть больше 0 и меньше 101\n3. Дата отправления не может быть в прошлом\n4. Дата прибытия не может быть меньше чем дата отправления\n5. Цена не может быть меньше 500";
    }

    private async void LoadData() {
        Stations = await _apiService.GetStationsAsync();
        DepartureStation = Stations[0];
        ArrivalStation = Stations[1];
    }
}
