namespace MauiTquillaSunrise.View;

public partial class GeneralAlertPopupView : Popup
{
	public GeneralAlertPopupView(GeneralAlertPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	async void OkayClicked(object sender, EventArgs e)
	{
		await CloseAsync();
	}
}