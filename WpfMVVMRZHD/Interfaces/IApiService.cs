using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.Interfaces;

public interface IApiService {
    Task<User> AuthorizationUserAsync(string login);
    Task<User> RegistrationUserAsync(string login, string password);
    Task<List<Schedule>> GetScheduleAsync();
    Task<List<Station>> GetStationsAsync();
}
