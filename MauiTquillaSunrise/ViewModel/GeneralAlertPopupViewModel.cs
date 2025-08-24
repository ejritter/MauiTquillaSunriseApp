
namespace MauiTquillaSunrise.ViewModel;
public partial class GeneralAlertPopupViewModel : BaseViewModel, IQueryAttributable
{

    public GeneralAlertPopupViewModel(IPopupService popupService) : base(popupService)
    {
    }


    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private bool isDismissable = true;
    
    [ObservableProperty]
    private ObservableCollection<ServerModel> servers = new();

    //public event EventHandler<bool> PopupClosed;

    //[RelayCommand]
    //private void OkayClicked()
    //{
    //    PopupClosed.Invoke(this, true);
    //}

    //[RelayCommand]
    //private void CancelClicked()
    //{
    //    PopupClosed.Invoke(this, false);
    //}



    [RelayCommand]
    private async Task OkayClicked()
    {
        await _popupService.ClosePopupAsync(page:Shell.Current, result:true);    
    }

    [RelayCommand]
    private async Task CancelClicked()
    {
        await _popupService.ClosePopupAsync(page: Shell.Current, result: false);
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Title = (string)query[nameof(GeneralAlertPopupViewModel.Title)];
        Message = (string)query[nameof(GeneralAlertPopupViewModel.Message)];
        Servers = (ObservableCollection<ServerModel>)query[nameof(GeneralAlertPopupViewModel.Servers)];
        IsDismissable = (bool)query[nameof(GeneralAlertPopupViewModel.IsDismissable)];
    }
} 
