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
        this.Clicked += OnButtonClicked;
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
    
    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (ReturnCommand?.CanExecute(null) == true)
        {
            ReturnCommand.Execute(null);
        }
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
