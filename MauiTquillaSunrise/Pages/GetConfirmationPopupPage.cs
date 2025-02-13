namespace MauiTquillaSunrise.Pages;
public class GetConfirmationPopupPage : BasePopup<GetConfirmationPopupViewModel>
{
    public GetConfirmationPopupPage(GetConfirmationPopupViewModel vm) : base(vm)
    {

    }

    public override void Build()
    {
        this.Bind(CanBeDismissedByTappingOutsideOfPopupProperty, 
                        getter: (GetConfirmationPopupViewModel vm) => vm.IsDismissable);

        Color = Colors.Red;

        Content = new VerticalStackLayout()
        {
            Spacing = 10,
            Margin = 10,

            Children =
            {
                new Border()
                {
                    Content = new VerticalStackLayout()
                    {
                        Spacing = 10,
                        Margin = 10,
                        Children =
                        {
                            new Label()
                                .Bind(Label.TextProperty, getter:(GetConfirmationPopupViewModel vm) => vm.Title)
                                .FontSize(18),

                            new Label()
                                .Bind(Label.TextProperty, getter:(GetConfirmationPopupViewModel vm) => vm.Message)
                                .FontSize(2)
                        }
                    }.CenterHorizontal()
                     .FillHorizontal()
                },

                new Border()
                {
                     Content = new CollectionView()
                                    .Bind(CollectionView.ItemsSourceProperty, getter:(GetConfirmationPopupViewModel vm) => vm.Servers)
                                    .Height(100)
                                    .ItemTemplate(new ServerModelDataTemplate())
                },

                new HorizontalStackLayout()
                {
                    Spacing = 10,

                    Children =
                    {
                        new Button()
                            .Assign(out Button okButton)
                            .Text("OK")
                            .Bind(Button.CommandParameterProperty, source:okButton )
                            .Bind(Button.CommandProperty, getter:(GetConfirmationPopupViewModel vm) => vm.OkayClickedCommand)
                            .Invoke(button =>
                            {
                                button.Clicked += (sender, e) => base.ClosePopup(sender, e, true);
                            }),
                        
                        new Button()
                            .Assign(out Button cancelButton)
                            .Text("CANCEL")
                            .Bind(Button.CommandParameterProperty, source:cancelButton )
                            .Bind(Button.CommandProperty, getter:(GetConfirmationPopupViewModel vm) => vm.CancelClickedCommand)
                            .Invoke(button =>
                            {
                                button.Clicked += (sender, e) => base.ClosePopup(sender, e, false);
                            })
                            
                    }
                }
            }
        }
        .FillHorizontal()
        .CenterHorizontal();
    }

}
