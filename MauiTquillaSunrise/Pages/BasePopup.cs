namespace MauiTquillaSunrise.Pages;
public abstract class BasePopup<TViewModel> : ContentView where TViewModel : BaseViewModel
{
    public BasePopup(TViewModel vm)
    {
#if DEBUG
        HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif

        BindingContext = vm;

        // Build popup content
        Build();

        // Apply themed background and size
       // ApplyPopupBackground();
    }
   
    private void ReloadUI(Type[] obj)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
        #if DEBUG
        HotReloadService.UpdateApplicationEvent -= ReloadUI;
        #endif
            Build();
           // ApplyPopupBackground();
        });

    }

    protected abstract void Build();

private void ApplyPopupBackground()
    {
        try
        {
            // Make the root popup container transparent
            this.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;

            if (Content is Microsoft.Maui.Controls.View originalContent)
            {
                // Determine a reasonable popup size (about 60% of shell, with sensible min/max)
                double popupWidth = 600;
                double popupHeight = 400;

                var appShell = Application.Current?.Windows?.FirstOrDefault(w => w.Page is AppShell);
                if (appShell is not null)
                {
                    popupWidth = Math.Clamp(appShell.Width * 0.6, 360, 900);
                    popupHeight = Math.Clamp(appShell.Height * 0.6, 240, 700);
                }

                // Inner popup container sized and centered
                var popupContainer = new Grid
                {
                    BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
                    WidthRequest = popupWidth,
                    HeightRequest = popupHeight,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                popupContainer.Children.Add(new Image
                {
                    Source = "retro_background.svg", // preferred
                    Aspect = Aspect.Fill,
                    Opacity = 1,
                    InputTransparent = true
                });

                // Content wrapper to add padding and rounded corners over the background
                var contentBorder = new Border
                {
                    Padding = 12,
                    StrokeThickness = 2,
                    StrokeShape = new RoundRectangle { CornerRadius = 18 },
                };
                contentBorder.SetDynamicResource(Border.StrokeProperty, "TquillaBorderStroke");
                contentBorder.Content = originalContent;

                popupContainer.Children.Add(contentBorder);

                Content = popupContainer;
            }
        }
        catch
        {
            // Avoid breaking popup rendering if background application fails
        }
    }

//    protected override Task OnDismissedByTappingOutsideOfPopup(CancellationToken token = default)
//    {
//        return base.OnDismissedByTappingOutsideOfPopup(token);
//#if DEBUG
//        HotReloadService.UpdateApplicationEvent -= ReloadUI;
//#endif
//    }
}
