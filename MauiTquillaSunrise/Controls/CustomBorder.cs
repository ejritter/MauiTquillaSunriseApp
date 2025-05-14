

namespace MauiTquillaSunrise.Controls;
public class CustomBorder : Border
{
    public CustomBorder()
    {
        InitializeBorder();
    }

    private void InitializeBorder()
    {
        this.Stroke = ResourceColors.TquillaBorderStroke;
        this.StrokeThickness = 2;
        this.StrokeShape = new RoundRectangle
        {
            CornerRadius = 25
        };

    }
}
