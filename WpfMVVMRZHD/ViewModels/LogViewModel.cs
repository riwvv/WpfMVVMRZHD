using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Views.Windows;

namespace WpfMVVMRZHD.ViewModels;

public partial class LogViewModel : ObservableObject {
    [ObservableProperty]
    private string _login;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _errorMessage;

    private readonly IApiService _apiService;
    private readonly IServiceProvider _serviceProvider;
    public LogViewModel(IApiService apiService, IServiceProvider serviceProvider) {
        _apiService = apiService;
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    public async Task Authorization() {
        if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)) {
            var user = await _apiService.AuthorizationUserAsync(Login);
            if (user == null) {
                ErrorMessage = "Такого пользователя нет";
                return;
            }
            if (user.Login != Login || user.Password != Password) {
                ErrorMessage = "Неверный логин или пароль";
                return;
            }
            App.CurrentUser = user;
            if (user.Role == "admin")
                _serviceProvider.GetRequiredService<AdminWindow>().Show();
            else
                _serviceProvider.GetRequiredService<UserWindow>().Show();
            _serviceProvider.GetRequiredService<LogWindow>().Close();
            return;
        }
        else
            ErrorMessage = "Заполните все поля";
    }

    [RelayCommand]
    public async Task Registration() {
        if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)) {
            var user = await _apiService.RegistrationUserAsync(Login, Password);
            if (user == null) {
                ErrorMessage = "Не удалось зарегистрироваться";
                return;
            }
            App.CurrentUser = user;
            _serviceProvider.GetRequiredService<UserWindow>().Show();
            _serviceProvider.GetRequiredService<LogWindow>().Close();
        }
        else
            ErrorMessage = "Заполните все поля";
    }
}
