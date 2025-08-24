namespace MauiTquillaSunrise.Pages;
public class GeneralAlertPopupPage : BasePopup<GeneralAlertPopupViewModel>
{

    public GeneralAlertPopupPage(GeneralAlertPopupViewModel vm) : base(vm)
    {

    }

    //protected override async Task OnClosed(object? result, bool wasDismissedByTappingOutsideOfPopup, CancellationToken token = default)
    //{
    //    await base.OnClosed(result, wasDismissedByTappingOutsideOfPopup, token);
    //}

    protected override void Build()
    {

        this.DynamicResource(ContentView.BackgroundColorProperty, "TquillaDarkPurple");
        Content =
            new ScrollView()
            {
                Content =
                        new VerticalStackLayout()
                        {

                            // Let the BasePopup background image show through and size to container
                            Spacing = 8,
                            Margin = 8,

                            Children =
                            {
                                new Label()
                                       .Font(bold: true, size: 42)
                                       .DynamicResource(Label.TextColorProperty, "TquillaText")
                                       .Bind(Label.TextProperty, getter: (GeneralAlertPopupViewModel vm) => vm.Title)
                                       .CenterHorizontal(),

                                new CustomBorder()
                                {
                                    Padding = 12,
                                    Margin = 12,
                                    Content =
                                                new CollectionView()
                                                {
                                                    ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical) { HorizontalItemSpacing = 2 },
                                                    Header = new Label()
                                                                    .Bind(Label.TextProperty,
                                                                                    getter: (GeneralAlertPopupViewModel vm) => vm.Message)
                                                                    .Font(bold: true, size: 20)
                                                }.ItemTemplate(new ServerModelDataTemplate())
                                                 .Bind(CollectionView.ItemsSourceProperty, getter: (GeneralAlertPopupViewModel vm) => vm.Servers)
                                                 .Top()
                                                 .Center()
                                                 .Fill()
                                                 .Height(300),

                                }
                                .Fill(),

                               new CustomBorder()
                               {
                                   Content = new HorizontalStackLayout()
                                            {
                                                Children =
                                                {
                                                 new CustomButton()
                                                    .Text("OK")
                                                    .CenterHorizontal()
                                                    .Bind(CustomButton.CommandProperty, getter: (GeneralAlertPopupViewModel vm) => vm.OkayClickedCommand)
                                                    .Bind(CustomButton.ReturnCommandProperty, getter:(GeneralAlertPopupViewModel vm) => vm.OkayClickedCommand),

                                                 new CustomButton()
                                                    .Text("Cancel")
                                                    .CenterHorizontal()
                                                    .Bind(Button.CommandProperty, getter: (GeneralAlertPopupViewModel vm) => vm.CancelClickedCommand)
                                                }
                                            }.Center()
                               }
                               .Top()

                            }
                        }

            };
    }
}
