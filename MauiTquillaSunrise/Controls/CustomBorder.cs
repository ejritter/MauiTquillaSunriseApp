namespace MauiTquillaSunrise.Controls;
public class CustomBorder : Border
{
    public CustomBorder()
    {
        InitializeBorder();
    }

    private void InitializeBorder()
    {
        // Bind to theme stroke color and thickness
        this.SetDynamicResource(Border.StrokeProperty, "TquillaBorderStroke");
        //this.SetDynamicResource(Border.BackgroundColorProperty, "TquillaPanel");
        this.StrokeThickness = 2;
        this.StrokeShape = new RoundRectangle
        {
            CornerRadius = 25
        };

    }
}
