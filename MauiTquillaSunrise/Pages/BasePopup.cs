namespace MauiTquillaSunrise.Pages;
public abstract class BasePopup<TViewModel> : Popup where TViewModel : BaseViewModel
{
    private void ReloadUI(Type[] obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Build();
        });

    }

    public abstract void Build();
    
    protected void ClosePopup(object sender, EventArgs e)
    {
        this.Close();
    }


    protected void ClosePopup(object sender, EventArgs e, bool yesOrNo)
    {
        if (yesOrNo)
        {
            this.Close();
        }
    }
    
    public BasePopup(TViewModel vm)
    {
#if DEBUG
        HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif

        BindingContext = vm;

        var appShell = Application.Current.Windows.FirstOrDefault(windows => windows.Page is AppShell);
        if (appShell is null == false)
        {
            Window.Width = appShell.Width * 0.9;
            Window.Height = appShell.Height * 0.9;
            
        }
        else
        {
            Window.Width = 200;
            Window.Height = 200;
        }
        Build();

    }
}
