using CommunityToolkit.Maui.Behaviors;

namespace MauiTquillaSunrise.Pages;

public abstract class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
{

    protected BasePage(TViewModel vm)
    {
        BindingContext = vm;
        Build();

    }

    protected abstract void Build();

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

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
}
