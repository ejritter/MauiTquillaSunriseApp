
using System.Runtime.CompilerServices;

namespace MauiTquillaSunrise.Controls;

public class CustomImageButton : ImageButton
{
    public CustomImageButton()
    {
        InitializeImageButton();
    }

    private void InitializeImageButton()
    {
        this.BackgroundColor = ResourceColors.TquillaSkyBlue;
        this.Height(30);
        this.Width(30);
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
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
