using QbitForward.Data;
using System.Net.Http;

namespace QbitForward.Services;

public class QbittorrentService : IQbittorrentService
{
    private static readonly HttpClient client = new HttpClient();
    private static string sessionCookie = string.Empty;

    public async Task<bool> AddMagnetLink(ClientConfig clientConfig, string magnetLink, Action<string> showErrors)
    {
        // If Username and password are supplied, login
        var isAuthenticated = (clientConfig.Username == null && clientConfig.Password == null) || await Login(clientConfig, showErrors);
        if (isAuthenticated)
        {
            var isAdded = await PostMagnetLink(clientConfig, magnetLink, showErrors);
            if (!isAdded)
                return false;
            else
                return true;
        }
        return false;
    }

    private async Task<bool> Login(ClientConfig clientConfig, Action<string> showErrors)
    {
        var loginContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("username", clientConfig.Username!),
            new KeyValuePair<string, string>("password", clientConfig.Password!)
        ]);

        try 
        {
            var response = await client.PostAsync($"{GetHostUrl(clientConfig)}/api/v2/auth/login", loginContent);
            if (response.IsSuccessStatusCode)
            {
                sessionCookie = response.Headers?.GetValues("Set-Cookie").FirstOrDefault() ?? string.Empty;

                if (!string.IsNullOrEmpty(sessionCookie))
                {
                    client.DefaultRequestHeaders.Add("Cookie", sessionCookie);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            showErrors("Failed to login!");
        }

        return false;
    }

    private async Task<bool> PostMagnetLink(ClientConfig clientConfig, string magnetLink, Action<string> showErrors)
    {
        var addContent = new FormUrlEncodedContent([
            new KeyValuePair<string, string>("urls", magnetLink)
        ]);

        try
        {
            var response = await client.PostAsync($"{GetHostUrl(clientConfig)}/api/v2/torrents/add", addContent);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
           showErrors("Failed to add magnet link!");
        }

        return false;
    }

    private string GetHostUrl(ClientConfig clientConfig)
    {
        return $"http://{clientConfig.HostName}:{clientConfig.Port}";
    }

}
