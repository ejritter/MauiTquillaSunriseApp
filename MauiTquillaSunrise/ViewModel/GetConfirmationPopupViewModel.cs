using Windows.Networking.Sockets;

namespace MauiTquillaSunrise.ViewModel;
public partial class GetConfirmationPopupViewModel : ObservableObject
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    List<ServerModel> servers;

    [ObservableProperty]
    string message;

    public GetConfirmationPopupViewModel(string title, List<ServerModel> servers)
    {
        Title = title;
        Servers = servers;
    }

    public GetConfirmationPopupViewModel(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public GetConfirmationPopupViewModel(string title, string message, List<ServerModel> servers)
    {
        Title = title;
        Message = message;
        Servers = servers;
    }


}
