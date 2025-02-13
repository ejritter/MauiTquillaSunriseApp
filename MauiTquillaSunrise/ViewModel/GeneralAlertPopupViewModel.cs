namespace MauiTquillaSunrise.ViewModel;
public partial class GeneralAlertPopupViewModel : BaseViewModel
{
    [ObservableProperty]
    string message;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    bool isDismissable = true;

    [ObservableProperty]
    ObservableCollection<ServerModel> invalidServers = new();


    public GeneralAlertPopupViewModel(string title, string message, bool isDismissable)
    {
        Message = message;
        Title = title;
        IsDismissable = isDismissable;
    }

    public GeneralAlertPopupViewModel(string title, string message, ObservableCollection<ServerModel> invalidServers, bool isDismissable)
    {
        Title = title;
        Message = message;
        IsDismissable = isDismissable;
        InvalidServers = invalidServers;
    }

}
