namespace MauiTquillaSunrise.ViewModel;
public partial class GeneralAlertPopupViewModel : ObservableObject
{
    [ObservableProperty]
    string message;

    public GeneralAlertPopupViewModel(string message)
    {
        Message = message;
    }
}
