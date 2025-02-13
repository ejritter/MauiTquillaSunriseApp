using CommunityToolkit.Maui.Markup;

namespace MauiTquillaSunrise;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IGeneralPopupService, GeneralPopupService>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<App>();

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<GeneralAlertPopupViewModel>();
        builder.Services.AddTransient<GeneralAlertPopupPage>();

        builder.Services.AddTransient<GetConfirmationPopupViewModel>();
        builder.Services.AddTransient<GetConfirmationPopupPage>();
        
        return builder.Build();
    }
}
