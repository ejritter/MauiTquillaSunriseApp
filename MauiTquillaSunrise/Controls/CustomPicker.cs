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
        this.TextColor = ResourceColors.TquillaTextColor;
        this.TitleColor = ResourceColors.TquillaTextColor;
        this.Background = ResourceColors.TquillaSkyBlue;
    }


}
