namespace MauiTquillaSunrise.Pages;
public class GeneralAlertPopupPage : BasePopup<GeneralAlertPopupViewModel>
{
    public GeneralAlertPopupPage(GeneralAlertPopupViewModel vm) : base(vm)
    {
    }
    protected override void Build()
    {
        this.Bind(CanBeDismissedByTappingOutsideOfPopupProperty,
                    getter: (GeneralAlertPopupViewModel vm) => vm.IsDismissable);

        Color = Colors.Red;
        Content = new VerticalStackLayout()
        {
            Spacing = 10,
            Margin = 10,
            HeightRequest = this.Window.Height * 0.9,
            WidthRequest = this.Window.Width * 0.9,
            Children =
            {
                  new Label()
                       .Font(bold: true, size: 18)
                       .Bind(Label.TextProperty, getter: (GeneralAlertPopupViewModel vm) => vm.Title)
                       .CenterHorizontal(),

                  new CollectionView() {ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical),
                                        Header = new Label().Bind(Label.TextProperty, getter:(GeneralAlertPopupViewModel vm) => vm.Message)
                                                            .Font(bold:true, size:20)}
                       .ItemTemplate(new ServerModelDataTemplate())
                       .Bind(CollectionView.ItemsSourceProperty, getter: (GeneralAlertPopupViewModel vm) => vm.Servers)
                       .Height(300),

                  new Button()
                    .Text("OK")
                    .CenterHorizontal()
                    .Bind(Button.CommandProperty, getter:(GeneralAlertPopupViewModel vm) => vm.OkayClickedCommand),

                  new Button()
                    .Text("Cancel")
                    .CenterHorizontal()
                    .Bind(Button.CommandProperty, getter:(GeneralAlertPopupViewModel vm) => vm.CancelClickedCommand)
            }


        }
        .Fill();

    }
}
