
using System.ComponentModel;

namespace MauiTquillaSunrise.View;

public partial class GetConfirmationPopup : Popup
{

    public GetConfirmationPopup(GetConfirmationPopupViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        
    }

    async void Okay_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(true);
    }

    async void Cancel_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(false);
    }
}
