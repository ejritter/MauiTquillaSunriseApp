namespace MauiTquillaSunrise.Services;
public class GeneralPopupService : IGeneralPopupService
{
    public async Task<bool> GetUserConfirmationPopup(string title, string message, List<ServerModel> servers)
    {
        var confirmationPage = new GetConfirmationPopupPage(new GetConfirmationPopupViewModel(title, message, servers));
        var response = await Shell.Current.CurrentPage.ShowPopupAsync(confirmationPage);

        if (response is bool foundResponse)
        {
            return foundResponse;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> GetUserConfirmationPopup(string title, string message)
    {
        var confirmationPage = new GetConfirmationPopupPage(new GetConfirmationPopupViewModel(title, message));
        var response = await Shell.Current.CurrentPage.ShowPopupAsync(confirmationPage);

        if (response is bool foundResponse)
        {
            return foundResponse;
        }
        else
        {
            return false;
        }
    }


    public async void GeneralAlertPopup(string title, string message, bool isDismissable)
    {
        var generalAlertPage = new GeneralAlertPopupPage(new GeneralAlertPopupViewModel(title, message, isDismissable));
        await Shell.Current.CurrentPage.ShowPopupAsync(generalAlertPage);
    }

    public async void GeneralAlertPopup(string title, string message, ObservableCollection<ServerModel> invalidServers, bool isDismissable)
    {
        var generalAlertPage = new GeneralAlertPopupPage(new GeneralAlertPopupViewModel(title, message, invalidServers, isDismissable));
        await Shell.Current.CurrentPage.ShowPopupAsync(generalAlertPage);
    }

}
