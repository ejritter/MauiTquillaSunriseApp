using Windows.Networking.Sockets;

namespace MauiTquillaSunrise.ViewModel;
public partial class GetConfirmationPopupViewModel : BaseViewModel
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    List<ServerModel> servers;

    [ObservableProperty]
    string message;

    [ObservableProperty]
    bool isDismissable = false;

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


    [RelayCommand]
    public async void OkayClicked(object sender)
    {
        if (sender is null)
        {
            return;
        }

        if (sender is Button button)
        {
           
        }

    }

    [RelayCommand]
    public async void CancelClicked(object sender)
    {
        if (sender is null)
        {
            return;
        }

        if (sender is Popup popup)
        {
            popup.Close();
        }
    }

}
