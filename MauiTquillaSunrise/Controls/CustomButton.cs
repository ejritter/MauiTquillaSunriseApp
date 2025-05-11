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
    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == nameof(IsEnabled))
        {
            UpdateBackground();
        }
    }

    private void UpdateBackground()
    {
        this.BackgroundColor = this.IsEnabled ? ResourceColors.TquillaSkyBlue :
                                                ResourceColors.TquillaDisabled;
    }
    private void InitializeButton()
    {
        this.BackgroundColor = ResourceColors.TquillaSkyBlue;
        this.TextColor = ResourceColors.TquillaTextColor;
    }
}
