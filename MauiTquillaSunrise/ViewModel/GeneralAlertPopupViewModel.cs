namespace MauiTquillaSunrise.ViewModel;
public partial class GeneralAlertPopupViewModel : ObservableObject
{
    [ObservableProperty]
    string message;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    bool isDismissable = true;


    public GeneralAlertPopupViewModel(string title, string message, bool isDismissable)
    {
        Message = message;
        Title = title;
        IsDismissable = isDismissable;
    }
}
