namespace MauiTquillaSunrise.ViewModel;
public partial class GetConfirmationPopupViewModel : ObservableObject
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    string message;

    public GetConfirmationPopupViewModel(string title, string message)
    {
        Title = title;
        Message = message;
    }


}
