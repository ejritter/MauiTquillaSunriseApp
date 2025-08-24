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
        // Use theme resource for visibility over backgrounds
        this.SetDynamicResource(ImageButton.BackgroundColorProperty, "TquillaSecondary");
        this.SetDynamicResource(ImageButton.BorderColorProperty, "TquillaGold");
        this.BorderWidth = 1;
        this.CornerRadius = 6;
        this.Height(34);
        this.Width(34);
    }
}
