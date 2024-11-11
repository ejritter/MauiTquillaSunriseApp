namespace MauiTquillaSunrise.ViewModel;
public partial class GeneralAlertPopupViewModel : ObservableObject
{
    [ObservableProperty]
    string message;

    [ObservableProperty]
    string title;

    public GeneralAlertPopupViewModel(string title,string message)
    {
        Message = message;
        Title = title;
    }
}
