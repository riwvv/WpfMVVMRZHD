using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.ViewModels;

public partial class DataPassportViewModel : ObservableObject {
    [ObservableProperty]
    private string? _series;

    [ObservableProperty]
    private string? _number;

    [ObservableProperty]
    private string? _first;

    [ObservableProperty]
    private string? _last;

    [ObservableProperty]
    private string? _middle;

    [ObservableProperty]
    private bool _hasSend = true;

    private readonly IApiService _apiService;
    public DataPassportViewModel(IApiService apiService) {
        _apiService = apiService;
        if (App.CurrentUser.IsPassportData)
            LoadData();
    }

    [RelayCommand]
    public async Task SendPassportData() {
        if (!string.IsNullOrEmpty(Series) && !string.IsNullOrEmpty(Number) && !string.IsNullOrEmpty(First) && !string.IsNullOrEmpty(Last) && !string.IsNullOrEmpty(Middle)) {
            var newData = new Passport_datum {
                Login = App.CurrentUser.Login,
                Passport_series = Series,
                Passport_number = Number,
                First_name = First,
                Last_name = Last,
                Middle_name = Middle
            };
            var result = await _apiService.SendPassportDataAsync(newData);
            if (result != null) {
                HasSend = false;
            }
        }
    }

    private async void LoadData() {
        var passportData = await _apiService.GetPassportDataAsync(App.CurrentUser.Login);
        if (passportData == null)
            return;
        Series = passportData.Passport_series;
        Number = passportData.Passport_number;
        First = passportData.First_name;
        Last = passportData.Last_name;
        Middle = passportData.Middle_name;
        HasSend = false;
    }
}
