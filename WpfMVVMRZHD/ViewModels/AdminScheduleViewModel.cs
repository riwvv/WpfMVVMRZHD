using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.ViewModels;

public partial class AdminScheduleViewModel : ObservableObject {
    [ObservableProperty]
    private ObservableCollection<Schedule> _currentSchedule;

    private readonly IApiService _apiService;

    public AdminScheduleViewModel(IApiService apiService) {
        _apiService = apiService;
        CurrentSchedule = [];
        LoadSchedule();
    }

    private async void LoadSchedule() {
        CurrentSchedule.Clear();
        var response = await _apiService.GetScheduleAsync();
        foreach (var item in response) {
            CurrentSchedule.Add(item);
        }
    }
}
