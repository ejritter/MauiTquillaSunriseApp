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

        
        Content =
            new ScrollView()
            {
                
                Content =
                    new VerticalStackLayout()
                    {
                        BackgroundColor = ResourceColors.TquillaBackground,
                        Spacing = 2,
                        Margin = 2,
                        HeightRequest = this.Window.Height * 0.9,
                        WidthRequest = this.Window.Width * 0.9,
                        Children =
                        {
                            new Label()
                                   .Font(bold: true, size: 18)
                                   .TextColor(ResourceColors.TquillaTextColor)
                                   .Bind(Label.TextProperty, getter: (GeneralAlertPopupViewModel vm) => vm.Title)
                                   .CenterHorizontal(),

                            new CollectionView() 
                            { 
                                ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical) { HorizontalItemSpacing = 2 },
                                Header = new Label()
                                                .Bind(Label.TextProperty, 
                                                                getter: (GeneralAlertPopupViewModel vm) => vm.Message)
                                                .Font(bold: true, size: 20) 
                                                .TextColor(ResourceColors.TquillaTextColor)
                            }.ItemTemplate(new ServerModelDataTemplate())
                             .Bind(CollectionView.ItemsSourceProperty, getter: (GeneralAlertPopupViewModel vm) => vm.Servers)
                             .Top()
                             .Center()
                             .Fill()
                             .Height(300),

                            new CustomButton()
                                .Text("OK")
                                .CenterHorizontal()
                                .Bind(Button.CommandProperty, getter: (GeneralAlertPopupViewModel vm) => vm.OkayClickedCommand),

                            new CustomButton()
                                .Text("Cancel")
                                .CenterHorizontal()
                                .Bind(Button.CommandProperty, getter: (GeneralAlertPopupViewModel vm) => vm.CancelClickedCommand)
                        }

                    }.Fill()
            };
    }
}
