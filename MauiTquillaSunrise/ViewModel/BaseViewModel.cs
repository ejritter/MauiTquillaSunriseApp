namespace MauiTquillaSunrise.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
    protected IGeneralPopupService GeneralPopupService { get; private set; }
    public BaseViewModel()
    {
        AssignServices();
    }

    private void AssignServices()
    {
        GeneralPopupService = GetServiceType<IGeneralPopupService>() ??
                new GeneralPopupService();
    }

    private T GetServiceType<T>()
    {
        return MauiWinUIApplication.Current.Services.GetService<T>();
    }
}
