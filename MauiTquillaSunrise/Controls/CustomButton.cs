using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiTquillaSunrise.Controls;

public class CustomButton : Button
{
    public CustomButton()
    {
        InitializeButton();
    }

    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(
            propertyName: nameof(ReturnCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(CustomButton));

    public ICommand ReturnCommand
    {
        get => (ICommand)GetValue(ReturnCommandProperty);
        set => SetValue(ReturnCommandProperty, value);
    }
    

    private void InitializeButton()
    {
        // Use theme resources
        this.SetDynamicResource(Button.BackgroundColorProperty, "TquillaSecondary");
        this.SetDynamicResource(Button.TextColorProperty, "TquillaText");
    }
}
