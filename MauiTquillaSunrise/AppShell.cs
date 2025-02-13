using CommunityToolkit.Maui.Converters;

namespace MauiTquillaSunrise;
public class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        Items.Add(mainPage);
        FlyoutBehavior = FlyoutBehavior.Disabled;
        BackgroundColor = Color.FromArgb("#FFC107");

       Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
