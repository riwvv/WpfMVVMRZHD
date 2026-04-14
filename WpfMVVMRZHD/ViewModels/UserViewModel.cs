using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WpfMVVMRZHD.Views.Pages;

namespace WpfMVVMRZHD.ViewModels;

public partial class UserViewModel : ObservableObject {
    [ObservableProperty]
    private object _currentPage;

    private readonly IServiceProvider _serviceProvider;
    public UserViewModel(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
        NavigateToSchedule();
    }

    [RelayCommand]
    public void NavigateToPassportData() => CurrentPage = _serviceProvider.GetRequiredService<DataPassportPage>();

    [RelayCommand]
    public void NavigateToSchedule() => CurrentPage = _serviceProvider.GetRequiredService<SchedulePage>();

    [RelayCommand]
    public void NavigateToPurchases() => CurrentPage = _serviceProvider.GetRequiredService<PurchasesPage>();

    [RelayCommand]
    public void NavigateToMyPurchases() => CurrentPage = _serviceProvider.GetRequiredService<MyPurchasesPage>();
}
