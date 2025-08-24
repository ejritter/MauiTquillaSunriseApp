namespace MauiTquillaSunrise.ViewModel;

public abstract partial class BaseViewModel(IPopupService popupService) : ObservableObject
{
    protected readonly IPopupService _popupService = popupService;

}
