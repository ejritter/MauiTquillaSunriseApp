namespace MauiTquillaSunrise.Controls;
public class CustomEntry : Entry
{
    public CustomEntry()
    {
        InitializeEntry();
    }

    private void InitializeEntry()
    {
        // Bind to theme resources from XAML
        this.SetDynamicResource(Entry.PlaceholderColorProperty, "TquillaPlaceHolder");
        this.SetDynamicResource(Entry.TextColorProperty, "TquillaText");
        this.SetDynamicResource(Entry.BackgroundColorProperty, "TquillaCard");
    }
}
