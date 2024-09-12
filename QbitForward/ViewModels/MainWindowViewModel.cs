using CommunityToolkit.Mvvm.ComponentModel;
using QbitForward.Data;
using QbitForward.Properties;
using QbitForward.Services;
using QbitForward.Views;
using System.Windows;
using System.Windows.Media;

namespace QbitForward.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IQbittorrentService qBittorrentService;
    public MainWindow MainWindow;

    [ObservableProperty] private string? hostName;
    [ObservableProperty] private int? port = 8444;
    [ObservableProperty] private string? username;
    [ObservableProperty] private string? password;

    [ObservableProperty] private string messageText;
    [ObservableProperty] private Visibility messageVisible = Visibility.Hidden;
    [ObservableProperty] private double messageOpacity = 0;
    [ObservableProperty] private SolidColorBrush messageColor;

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
        if (!url.StartsWith("magnet:"))
        {
            // Error handling
            return;
        }

        var clientConfig = new ClientConfig(HostName!, Port, Username, Password);
        if (await qBittorrentService.AddMagnetLink(clientConfig, url, ShowError))
            ShowSuccess("Magnet link added!");
    }

    public void UpdateClientConfig()
    {
        Settings.Default.HostName = HostName;
        Settings.Default.Port = Port ?? 8444;
        Settings.Default.Username = Username;
        Settings.Default.Password = Password;
        Settings.Default.Save();
    }

    public void ShowError(string message)
    {
        MessageText = message;
        MessageColor = new SolidColorBrush(Colors.Red);
        MessageOpacity = 1;
        MainWindow.ShowMessage();
        MessageVisible = Visibility.Visible;
    }

    public void ShowSuccess(string message)
    {
        MessageText = message;
        MessageColor = new SolidColorBrush(Colors.Green);
        MessageOpacity = 1;
        MainWindow.ShowMessage();
        MessageVisible = Visibility.Visible;
    }

}
