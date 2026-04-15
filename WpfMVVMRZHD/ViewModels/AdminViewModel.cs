using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WpfMVVMRZHD.Views.Pages;

namespace WpfMVVMRZHD.ViewModels;

public partial class AdminViewModel : ObservableObject {
    [ObservableProperty]
    private object _currentPage;

    private readonly IServiceProvider _serviceProvider;
    public AdminViewModel(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
        NavigateToSchedule();
    }

    [RelayCommand]
    public void NavigateToSchedule() => CurrentPage = _serviceProvider.GetRequiredService<AdminSchedulePage>();

    [RelayCommand]
    public void NavigateToAddSchedule() => CurrentPage = _serviceProvider.GetRequiredService<AddSchedulePage>();
}
