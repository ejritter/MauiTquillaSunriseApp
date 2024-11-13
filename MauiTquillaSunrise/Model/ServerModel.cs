
namespace MauiTquillaSunrise.Model;
public partial class ServerModel : ObservableObject
{
    [ObservableProperty]
    string serverName;

    [ObservableProperty]
    string domainName;

    [ObservableProperty]
    string port;
}
