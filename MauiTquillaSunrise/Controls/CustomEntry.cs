namespace MauiTquillaSunrise.Controls;
public class CustomEntry : Entry
{
    public CustomEntry()
    {
        InitializeEntry();
    }

    private void InitializeEntry()
    {
        this.PlaceholderColor = ResourceColors.TquillaPlaceHolder;
        this.TextColor = ResourceColors.TquillaTextColor;
        this.BackgroundColor = ResourceColors.TquillaSkyBlue;
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged();
        if (propertyName == nameof(IsEnabled))
        {
            UpdateBackgroundColor();
        }
    }

    private void UpdateBackgroundColor()
    {
        this.BackgroundColor = this.IsEnabled ? ResourceColors.TquillaSkyBlue :
                                                ResourceColors.TquillaDisabled;
    }
}
