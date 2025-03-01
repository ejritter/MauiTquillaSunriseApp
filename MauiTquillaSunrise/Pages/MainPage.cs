using CommunityToolkit.Maui.Behaviors;
using Windows.ApplicationModel.Activation;

namespace MauiTquillaSunrise.Pages;
public class MainPage : BasePage<MainViewModel>
{
    public MainPage(MainViewModel vm) : base(vm)
    {
        Title = vm.Title;
        this.Behaviors(new EventToCommandBehavior()
        {
            EventName = nameof(this.Loaded),
            Command = vm.PageLoadedCommand
        });
    }


    protected override void Build()
    {
        Content = new Grid()
        {
            //height
            RowDefinitions = Rows.Define(
               (Row.UsernameButton, 50),
               (Row.PasswordButton, 50),
               (Row.ServerCount, 20),
               (Row.Picker, 60),
               (Row.ServerCollectionView, Star)),
            
            //width
            ColumnDefinitions = Columns.Define(
               (Column.EntryLabels, 300),
               (Column.AddHideButtons, 60),
               (Column.ServerAddRemoveUpdateButtons, 300),
               (Column.AddButton, 30)),

            ColumnSpacing = 8,
            RowSpacing = 8,
            Padding = 8,
            Margin = 8,

            Children =
            {
                 //first row
                new Entry()
                    .Placeholder("Username")
                    .Column(Column.EntryLabels)
                    .Row(Row.UsernameButton)
                    .Bind(Entry.IsPasswordProperty, getter:(MainViewModel vm) => vm.IsUsernameShowing)
                    .Bind(Entry.TextProperty, mode: BindingMode.TwoWay,
                                              getter:(MainViewModel vm) => vm.UserNameText,
                                              setter:(MainViewModel vm, string? userNameText) => vm.UserNameText = userNameText ?? string.Empty)
                    .Bind(Entry.ReturnCommandParameterProperty, getter:(MainViewModel vm) => vm.UserNameText)
                    .Bind(Entry.ReturnCommandProperty, getter:(MainViewModel vm) => vm.AddUsernameCommand),

                new ImageButton()
                    .Column(Column.AddHideButtons)
                    .Row(Row.UsernameButton)
                    .Start()
                    .Width(30)
                    .Height(30)
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.AddButton)
                    .Bind(ImageButton.CommandParameterProperty, getter:(MainViewModel vm) => vm.UserNameText)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.AddUsernameCommand),

                new ImageButton()
                    .Column(Column.AddHideButtons)
                    .Row(Row.UsernameButton)
                    .End()
                    .Width(30)
                    .Height(30)
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.RevealUsernameButtonIcon)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.RevealUsernameCommand),

                new Entry()
                    .Column(Column.ServerAddRemoveUpdateButtons)
                    .Row(Row.UsernameButton)
                    .Placeholder("Server.Domain.com:Port")
                    .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                              getter:(MainViewModel vm) => vm.ServerText,
                                              setter:(MainViewModel vm, string serverText) => vm.ServerText = serverText ?? string.Empty)
                    .Bind(Entry.ReturnCommandParameterProperty, getter:(MainViewModel vm) => vm.ServerText)
                    .Bind(Entry.ReturnCommandProperty, getter:(MainViewModel vm) => vm.AddServerCommand)
                    .Bind(Entry.IsEnabledProperty, getter:(MainViewModel vm) => vm.IsEnabled),


                new ImageButton()
                    .Column(Column.AddButton)
                    .Row(Row.UsernameButton)
                    .Width(30)
                    .Height(30)
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.AddButton)
                    .Bind(ImageButton.CommandParameterProperty, getter:(MainViewModel vm) => vm.ServerText)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.AddServerCommand)
                    .Bind(ImageButton.IsEnabledProperty, getter:(MainViewModel vm) => vm.IsEnabled),

                    //second row
               new Entry()
                    .Column(Column.EntryLabels)
                    .Row(Row.PasswordButton)
                    .Placeholder("Password")
                    .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                              getter:(MainViewModel vm) => vm.PasswordText,
                                              setter:(MainViewModel vm, string? passwordText) => vm.PasswordText = passwordText ?? string.Empty)
                    .Bind(Entry.IsPasswordProperty, getter:(MainViewModel vm) => vm.IsPasswordShowing)
                    .Bind(Entry.ReturnCommandParameterProperty, getter:(MainViewModel vm) => vm.PasswordText)
                    .Bind(Entry.ReturnCommandProperty, getter:(MainViewModel vm) => vm.AddPasswordCommand),

               new ImageButton()
                    .Column(Column.AddHideButtons)
                    .Row(Row.PasswordButton)
                    .Width(30)
                    .Height(30)
                    .Start()
                    .Bind(ImageButton.CommandParameterProperty, getter:(MainViewModel vm) => vm.PasswordText)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.AddPasswordCommand)
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.AddButton),

               new ImageButton()
                    .Column(Column.AddHideButtons)
                    .Row(Row.PasswordButton)
                    .Width(30)
                    .Height(30)
                    .End()
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.RevealPasswordCommand)
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.RevealPasswordButtonIcon),

               new Button()
                    .Column(Column.ServerAddRemoveUpdateButtons)
                    .Row(Row.PasswordButton)
                    .Padding(5)
                    .Bind(Button.IsEnabledProperty, getter:(MainViewModel vm) => vm.IsEnabled)
                    .Bind(Button.CommandProperty, getter:(MainViewModel vm) => vm.UpdateCredentialsCommand)
                    .Text("Update All Server Credentials"),

               new Label()
                    .Column(Column.EntryLabels)
                    .Row(Row.ServerCount)
                    .Text("Server Count:")
                    .Start(),

               new Label()
                    .Column(Column.EntryLabels)
                    .Row(Row.ServerCount)
                    .CenterHorizontal()
                    .Bind(Label.TextProperty, getter:(MainViewModel vm) => vm.ServerCount),


                 new Picker(){Title ="Select Island:", ItemDisplayBinding = new Binding(nameof(DomainModel.DomainName))}
                    .ColumnSpan(All<Column>())
                    .Row(Row.Picker)
                    .Center()
                    .Bottom()
                    .Fill()
                    .Bind(Picker.ItemsSourceProperty, mode:BindingMode.OneWay,  getter:(MainViewModel vm) => vm.Domains)
                    .Bind(Picker.SelectedItemProperty,mode:BindingMode.OneWay,  getter:(MainViewModel vm) => vm.SelectedDomain)
                    .Invoke(picker =>
                    {
                        if (base.BindingContext is MainViewModel vm)
                        {
                            picker.SelectedIndexChanged += vm.SetServersToPickerSelectedItem;
                        }
                    }),

               new CollectionView()
               {
                   SelectionMode = SelectionMode.Multiple,
                   MaximumWidthRequest = 250
               }
                    .Assign(out CollectionView serverCv)
                    .ColumnSpan(3)
                    .Row(Row.ServerCollectionView)
                    .EmptyView("Vault currently empty")
                    .Top()
                    .Center()
                    .Fill()
                    .Bind(CollectionView.ItemsSourceProperty, getter:(MainViewModel vm) => vm.Servers)
                    .Bind(CollectionView.SelectionChangedCommandParameterProperty, source:serverCv)
                    .Bind(CollectionView.SelectionChangedCommandProperty, getter:(MainViewModel vm) => vm.ServerSelectedCommand)
                    .ItemTemplate(new ServerModelDataTemplate()),

               new ImageButton()
                    .Column(Column.ServerAddRemoveUpdateButtons)
                    .Row(Row.ServerCollectionView)
                    .Width(30)
                    .Height(30)
                    .Center()
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.RemoveServerIcon)
                    .Bind(ImageButton.IsVisibleProperty, getter: (MainViewModel vm) => vm.IsServerSelected)
                    .Bind(ImageButton.CommandProperty, getter: (MainViewModel vm) => vm.RemoveServerCommand)
            }

        };
    }

    private enum Row { UsernameButton, PasswordButton, ServerCount, Picker, ServerCollectionView }
    private enum Column { EntryLabels, AddHideButtons, ServerAddRemoveUpdateButtons, AddButton }
}
