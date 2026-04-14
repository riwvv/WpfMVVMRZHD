using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.Services;

public class ApiService : IApiService {
    private readonly HttpClient _httpClient;
    public ApiService(string baseUrl) {
        _httpClient = new();
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<User> AuthorizationUserAsync(string login) {
        try {
            var response = await _httpClient.GetAsync($"api/Users/{login}");
            if (response.IsSuccessStatusCode) {
                var json = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(json);
                return user;
            }
            MessageBox.Show($"Такого пользователя не существует: {response.StatusCode}");
            return new User();
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка входа: {ex.Message}");
            return new User();
        }
    }

    public async Task<User> RegistrationUserAsync(string login, string password) {
        try {
            var response = await _httpClient.PostAsync("api/Users", new StringContent(JsonConvert.SerializeObject(new User { Login = login, Password = password}), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode) {
                var json = await response.Content.ReadAsStringAsync();
                var newUser = JsonConvert.DeserializeObject<User>(json);
                return newUser;
            }
            MessageBox.Show($"Не получилось зарегистрировать пользователя: {response.StatusCode}");
            return new User();
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка регистрации: {ex.Message}");
            return new User();
        }
    }

    public async Task<List<Schedule>> GetScheduleAsync() {
        try {
            var response = await _httpClient.GetAsync("api/Schedules");
            if (response.IsSuccessStatusCode) {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Schedule>>(json);
                return data;
            }
            MessageBox.Show($"Ошибка получения расписания: {response.StatusCode}");
            return new List<Schedule>();
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка: {ex.Message}");
            return new List<Schedule>();
        }
    }

    public async Task<List<Station>> GetStationsAsync() {
        try {
            var response = await _httpClient.GetAsync("api/Stations");
            if (response.IsSuccessStatusCode) {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Station>>(json);
                return data;
            }
            MessageBox.Show($"Ошибка получения станций: {response.StatusCode}");
            return new List<Station>();
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка: {ex.Message}");
            return new List<Station>();
        }
    }
}
