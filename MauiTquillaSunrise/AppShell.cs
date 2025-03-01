using CommunityToolkit.Maui.Converters;

namespace MauiTquillaSunrise;
public class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        Items.Add(mainPage);
        FlyoutBehavior = FlyoutBehavior.Disabled;

       Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
