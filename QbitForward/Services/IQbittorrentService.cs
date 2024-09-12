using QbitForward.Data;

namespace QbitForward.Services;

public interface IQbittorrentService
{
    Task<bool> AddMagnetLink(ClientConfig clientConfig, string magnetLink, Action<string> showErrors);
}