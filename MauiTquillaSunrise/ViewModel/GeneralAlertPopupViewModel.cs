﻿namespace MauiTquillaSunrise.ViewModel;
public partial class GeneralAlertPopupViewModel : BaseViewModel
{
    [ObservableProperty]
    private string message = string.Empty;

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private bool isDismissable = true;

    [ObservableProperty]
    private ObservableCollection<ServerModel> servers = new();

    public event EventHandler<bool> PopupClosed;
    public GeneralAlertPopupViewModel()
    {
        
    }

    [RelayCommand]
    private void OkayClicked()
    {
        PopupClosed.Invoke(this, true);
    }

    [RelayCommand]
    private void CancelClicked()
    {
        PopupClosed.Invoke(this, false);
    }
} 
