using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QbitForward.Services;
using QbitForward.Views;
using System.Windows;

namespace QbitForward;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services
                    .AddSingleton<MainWindow>()
                    .AddSingleton<IQbittorrentService, QbittorrentService>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var mainForm = AppHost.Services.GetRequiredService<MainWindow>();
        mainForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
