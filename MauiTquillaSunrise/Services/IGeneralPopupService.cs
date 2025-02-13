namespace MauiTquillaSunrise.Services;
public interface IGeneralPopupService
{
    Task<bool> GetUserConfirmationPopup(string title, string message);

    Task<bool> GetUserConfirmationPopup(string title, string message, List<ServerModel> servers);

    void GeneralAlertPopup(string title, string message, bool isDismissable);

    void GeneralAlertPopup(string title, string message, ObservableCollection<ServerModel> invalidServers, bool isDismissable);
}
