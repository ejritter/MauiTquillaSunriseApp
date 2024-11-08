

namespace MauiTquillaSunrise.View;

public partial class MainView : ContentPage
{
	private MainViewModel _currentVm;
	public MainView(MainViewModel vm)
	{
		InitializeComponent();
        _currentVm = vm;
        BindingContext = vm;

	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		_currentVm.PageLoaded();
    }

    private void PickerChanged_Event(object sender, EventArgs e)
    {
		_currentVm.PickerChanged_Event(sender, e);
    }
}