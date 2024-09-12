using QbitForward.Data;

namespace QbitForward.Services;

internal interface IQbittorrentService
{
    Task<bool> AddMagnetLink(ClientConfig clientConfig, string magnetLink);
}