
namespace MauiTquillaSunrise.Pages;
public abstract class BasePopup<TViewModel> : Popup where TViewModel : BaseViewModel
{
    public BasePopup(TViewModel vm)
    {
#if DEBUG
        HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif

        BindingContext = vm;

        var appShell = Application.Current
            .Windows
            .FirstOrDefault(windows => windows.Page is AppShell);

        if (appShell is null == false)
        {
            Window.Width = appShell.Width * 0.5;
            Window.Height = appShell.Height * 0.5;
            
        }
        else
        {
            Window.Width = 200;
            Window.Height = 200;
        }
        Build();

    }
    protected override Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
    {
        #if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
        #endif
        return base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
    }
    private void ReloadUI(Type[] obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
        #if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
        #endif
            Build();
        });

    }

    protected abstract void Build();



    protected override Task OnDismissedByTappingOutsideOfPopup(CancellationToken token = default)
    {
        return base.OnDismissedByTappingOutsideOfPopup(token);
#if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
#endif
    }

}
