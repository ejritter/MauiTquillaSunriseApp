namespace MauiTquillaSunrise.Pages;

public abstract class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
{
    public abstract void Build();

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Build();
#if DEBUG
        HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
#if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
#endif

    }

    private void ReloadUI(Type[] obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Build();
        });

    }

    protected void PageLoaded(object sender, EventArgs e)
    {
        if (sender is null)
        {
            return;
        }

        if (BindingContext is MainViewModel vm)
        {
            vm.PageLoaded();
        }
    }
    public BasePage(TViewModel vm)
    {
        BindingContext = vm;
        Build();
        Loaded += PageLoaded;
    }
}
