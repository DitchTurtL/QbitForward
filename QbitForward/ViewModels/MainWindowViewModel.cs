using CommunityToolkit.Mvvm.ComponentModel;
using QbitForward.Data;
using QbitForward.Properties;
using QbitForward.Services;

namespace QbitForward.ViewModels;

internal partial class MainWindowViewModel : ObservableObject
{
    private readonly IQbittorrentService qBittorrentService;

    [ObservableProperty] private string? hostName;
    [ObservableProperty] private int? port = 8444;
    [ObservableProperty] private string? username;
    [ObservableProperty] private string? password;

    public MainWindowViewModel(IQbittorrentService qBittorrentService)
    {
        this.qBittorrentService = qBittorrentService;

        HostName = Settings.Default.HostName;
        Port = Settings.Default.Port;
        Username = Settings.Default.Username;
        Password = Settings.Default.Password;
    }

    public async Task AddUrlAsync(string url)
    {
        var clientConfig = new ClientConfig(HostName!, Port, Username, Password);
        var isAdded = await qBittorrentService.AddMagnetLink(clientConfig, url);
        if (!isAdded)
        {
            // Error handling
        }
    }

}
