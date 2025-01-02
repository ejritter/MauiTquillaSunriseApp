
namespace MauiTquillaSunrise;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window
        {
            Page = new AppShell(),
            Width = 600,
            Title = "Maui T-Quilla Sunrise"
        };
        return window;
    }

}
