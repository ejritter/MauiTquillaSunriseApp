
namespace MauiTquillaSunrise.View;

public partial class GetConfirmationPopup : Popup
{
	public string Message { get; set; }
	public string Title { get; set; }
	public GetConfirmationPopup()
	{
		InitializeComponent();
	}
	
	async void Ok_Clicked(object sender, EventArgs e)
	{
		await CloseAsync(true);
	}

	async void Cancel_Clicked(object sender, EventArgs e)
	{
		await CloseAsync(false);
	}
}