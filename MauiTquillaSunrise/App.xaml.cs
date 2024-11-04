
namespace MauiTquillaSunrise;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {

        Window window = base.CreateWindow(activationState);
        window.Width = 600;
        window.Title = "Maui T-Quilla Sunrise";
        return window;
    }

}
