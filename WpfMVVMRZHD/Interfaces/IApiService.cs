using WpfMVVMRZHD.Models;

namespace WpfMVVMRZHD.Interfaces;

public interface IApiService {
    Task<User> AuthorizationUserAsync(string login);
    Task<User> RegistrationUserAsync(string login, string password);
    Task<List<Schedule>> GetScheduleAsync();
    Task<List<Station>> GetStationsAsync();
    Task<Passport_datum> GetPassportDataAsync(string login);
    Task<Passport_datum> SendPassportDataAsync(Passport_datum passport);
    Task<List<Schedule>> SearchSchedulesAsync(Schedule schedule, DateTime? date = null);
}
