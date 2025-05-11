
namespace MauiTquillaSunrise.Model;
public partial class ServerModel : ObservableObject, IComparable<ServerModel>
{
    [ObservableProperty]
    string serverName;

    [ObservableProperty]
    string domainName;

    [ObservableProperty]
    string port;

    public int CompareTo(ServerModel other)
    {
        if (other == null) return 1;

        int domainNameComparison = string.Compare(this.domainName, other.domainName, StringComparison.Ordinal);
        if (domainNameComparison != 0) return domainNameComparison;

        int serverNameComparison = string.Compare(this.serverName, other.serverName, StringComparison.Ordinal);
        if (serverNameComparison != 0) return serverNameComparison;

        return string.Compare(this.port, other.port, StringComparison.Ordinal);
    }
}
