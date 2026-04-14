using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WpfMVVMRZHD.Interfaces;
using WpfMVVMRZHD.Models;
using WpfMVVMRZHD.Services;
using WpfMVVMRZHD.ViewModels;
using WpfMVVMRZHD.Views.Pages;
using WpfMVVMRZHD.Views.Windows;

namespace WpfMVVMRZHD;

public partial class App : Application {
    public static User CurrentUser { get; set; }
    public static IServiceProvider Services { get; private set; }
    private readonly IHost _host;

    public App() {
        var builder = Host.CreateDefaultBuilder().ConfigureServices((context, services) => {
            services.AddSingleton<IApiService>(x => new ApiService("https://localhost:7197/"));

            services.AddSingleton<LogViewModel>();
            services.AddSingleton<UserViewModel>();
            services.AddSingleton<AdminViewModel>();
            services.AddSingleton<AddScheduleViewModel>();
            services.AddSingleton<DataPassportViewModel>();
            services.AddSingleton<MyPurchasesViewModel>();
            services.AddSingleton<PurchasesViewModel>();
            services.AddSingleton<ScheduleViewModel>();

            services.AddSingleton<LogWindow>();
            services.AddSingleton<UserWindow>();
            services.AddSingleton<AdminWindow>();
            
            services.AddSingleton<AddSchedulePage>();
            services.AddSingleton<SchedulePage>();
            services.AddSingleton<DataPassportPage>();
            services.AddSingleton<MyPurchasesPage>();
            services.AddSingleton<PurchasesPage>();
        });

        _host = builder.Build();
        Services = _host.Services;
    }

    protected override async void OnStartup(StartupEventArgs e) {
        await _host.StartAsync();

        var logWindow = _host.Services.GetRequiredService<LogWindow>();
        logWindow.DataContext = _host.Services.GetRequiredService<LogViewModel>();
        logWindow.Show();
        
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e) {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }
}
