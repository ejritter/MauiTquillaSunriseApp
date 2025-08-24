using Microsoft.UI.Xaml.Controls;
using System.Runtime.CompilerServices;

namespace MauiTquillaSunrise.Controls;
public class CustomPicker : Picker
{
    public CustomPicker()
    {
        InitializePicker();
        
    }

    private void InitializePicker()
    {
        this.Title = "Select Island:";
        this.ItemDisplayBinding = new Binding(nameof(DomainModel.DomainName));
        // Use theme resources (vibrant gold on dark purple)
        this.SetDynamicResource(Picker.TextColorProperty, "TquillaText");
        this.SetDynamicResource(Picker.TitleColorProperty, "TquillaGold");
        //this.SetDynamicResource(Picker.BackgroundColorProperty, "TquillaDarkPurple");
    }
}
