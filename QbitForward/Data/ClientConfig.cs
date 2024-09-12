namespace QbitForward.Data;

public class ClientConfig
{
    public string? HostName { get; set; }
    public int? Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public ClientConfig(string hostName, int? port, string? username, string? password)
    {
        HostName = hostName;
        Port = port;
        Username = username;
        Password = password;
    }
}
