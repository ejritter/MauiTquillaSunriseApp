

namespace MauiTquillaSunrise.View;

public partial class MainView : ContentPage
{
	private MainViewModel _currentVm;
	public MainView(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_currentVm = vm;
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		_currentVm.PageLoaded();
    }
}