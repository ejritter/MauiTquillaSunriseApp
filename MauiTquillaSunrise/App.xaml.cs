namespace MauiTquillaSunrise;
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        SetTheme();
        MainPage = new AppShell();
    }

    private void SetTheme()
    {
        if (AppInfo.RequestedTheme == AppTheme.Dark)
        {
            Resources["EntryBackground"] = Resources["DarkEntryBackground"];
            Resources["EntryText"] = Resources["DarkEntryText"];
            Resources["ImageButtonBackground"] = Resources["DarkImageButtonBackground"];
            Resources["ImageButtonIcon"] = Resources["DarkImageButtonIcon"];
            Resources["LabelText"] = Resources["DarkLabelText"];
            Resources["AppShellBackground"] = Resources["DarkAppShellBackground"];
            Resources["AppShellText"] = Resources["DarkAppShellText"];
        }
        else
        {
            Resources["EntryBackground"] = Resources["LightEntryBackground"];
            Resources["EntryText"] = Resources["LightEntryText"];
            Resources["ImageButtonBackground"] = Resources["LightImageButtonBackground"];
            Resources["ImageButtonIcon"] = Resources["LightImageButtonIcon"];
            Resources["LabelText"] = Resources["LightLabelText"];
            Resources["AppShellBackground"] = Resources["LightAppShellBackground"];
            Resources["AppShellText"] = Resources["LightAppShellText"];
        }
    }
}
