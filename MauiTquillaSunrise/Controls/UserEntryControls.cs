using Microsoft.Maui.Controls.Shapes;

namespace MauiTquillaSunrise.Controls;
public class UserEntryControls : ContentView
{
    public UserEntryControls()
    {
        Content = InitializeControls();
    }

    private Microsoft.Maui.Controls.View InitializeControls()
    {
        // Outer background from retro palette for high contrast
        var background = new Border
        {
            Padding = 14,
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle { CornerRadius = 20 }
        }
        .DynamicResource(Border.StrokeProperty, "TquillaBorderStroke");
        //.DynamicResource(Border.BackgroundColorProperty, "TquillaDarkPurple");

        var content = new Grid()
        {
            Margin = 5,
            ColumnSpacing = 12,
            RowSpacing = 8,
            RowDefinitions = Rows.Define(
                (Row.UserNameEntry, 50),
                (Row.PasswordEntry, 50)),

            ColumnDefinitions = Columns.Define(
                (Column.UsernamePasswordEntry, 300),
                (Column.UsernamePasswordAdd, 40),
                (Column.UsernamePasswordHide, 40),
                (Column.ServerEntryUpdateAll, 300),
                (Column.ServerDomainAdd, 40)),

            Children =
            {
                new CustomEntry()
                    .Placeholder("Username")
                    .Column(Column.UsernamePasswordEntry)
                    .Row(Row.UserNameEntry)
                    //.DynamicResource(Entry.TextColorProperty, "TquillaGold")
                    //.DynamicResource(Entry.PlaceholderColorProperty, "TquillaGold")
                    .Bind(Entry.IsPasswordProperty, getter:(MainViewModel vm) => vm.IsUsernameShowing)
                    .Bind(Entry.TextProperty, mode: BindingMode.TwoWay,
                                              getter:(MainViewModel vm) => vm.UserNameText,
                                              setter:(MainViewModel vm, string? userNameText) => vm.UserNameText = userNameText ?? string.Empty)
                    .Bind(Entry.ReturnCommandParameterProperty, getter:(MainViewModel vm) => vm.UserNameText)
                    .Bind(Entry.ReturnCommandProperty, getter:(MainViewModel vm) => vm.AddUsernameCommand),

                new CustomImageButton()
                    .Column(Column.UsernamePasswordAdd)
                    .Row(Row.UserNameEntry)
                    .Start()
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.AddButton)
                    .Bind(ImageButton.CommandParameterProperty, getter:(MainViewModel vm) => vm.UserNameText)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.AddUsernameCommand),

                new CustomImageButton()
                    .Column(Column.UsernamePasswordHide)
                    .Row(Row.UserNameEntry)
                    .Start()
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.RevealUsernameButtonIcon)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.RevealUsernameCommand),

                new CustomEntry()
                    .Column(Column.ServerEntryUpdateAll)
                    .Row(Row.UserNameEntry)
                    .Placeholder("Server.Domain.com:Port")
                    //.DynamicResource(Entry.TextColorProperty, "TquillaGold")
                    //.DynamicResource(Entry.PlaceholderColorProperty, "TquillaGold")
                    .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                              getter:(MainViewModel vm) => vm.ServerText,
                                              setter:(MainViewModel vm, string serverText) => vm.ServerText = serverText ?? string.Empty)
                    .Bind(Entry.ReturnCommandParameterProperty, getter:(MainViewModel vm) => vm.ServerText)
                    .Bind(Entry.ReturnCommandProperty, getter:(MainViewModel vm) => vm.AddServerCommand)
                    .Bind(Entry.IsEnabledProperty, getter:(MainViewModel vm) => vm.IsEnabled),

                new CustomImageButton()
                    .Column(Column.ServerDomainAdd)
                    .Row(Row.UserNameEntry)
                    .Start()
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.AddButton)
                    .Bind(ImageButton.CommandParameterProperty, getter:(MainViewModel vm) => vm.ServerText)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.AddServerCommand)
                    .Bind(ImageButton.IsEnabledProperty, getter:(MainViewModel vm) => vm.IsEnabled),

                new CustomEntry()
                    .Column(Column.UsernamePasswordEntry)
                    .Row(Row.PasswordEntry)
                    .Placeholder("Password")
                    //.DynamicResource(Entry.TextColorProperty, "TquillaGold")
                    //.DynamicResource(Entry.PlaceholderColorProperty, "TquillaGold")
                    .Bind(Entry.TextProperty, mode:BindingMode.TwoWay,
                                              getter:(MainViewModel vm) => vm.PasswordText,
                                              setter:(MainViewModel vm, string? passwordText) => vm.PasswordText = passwordText ?? string.Empty)
                    .Bind(Entry.IsPasswordProperty, getter:(MainViewModel vm) => vm.IsPasswordShowing)
                    .Bind(Entry.ReturnCommandParameterProperty, getter:(MainViewModel vm) => vm.PasswordText)
                    .Bind(Entry.ReturnCommandProperty, getter:(MainViewModel vm) => vm.AddPasswordCommand),

               new CustomImageButton()
                    .Column(Column.UsernamePasswordAdd)
                    .Row(Row.PasswordEntry)
                    .Start()
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.AddButton)
                    .Bind(ImageButton.CommandParameterProperty, getter:(MainViewModel vm) => vm.PasswordText)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.AddPasswordCommand),

               new CustomImageButton()
                    .Column(Column.UsernamePasswordHide)
                    .Row(Row.PasswordEntry)
                    .Start()
                    .Bind(ImageButton.SourceProperty, getter:(MainViewModel vm) => vm.RevealPasswordButtonIcon)
                    .Bind(ImageButton.CommandProperty, getter:(MainViewModel vm) => vm.RevealPasswordCommand),

               new CustomButton()
                    .Column(Column.ServerEntryUpdateAll)
                    .Row(Row.PasswordEntry)
                    .Text("Update All Server Credentials")
                    //.DynamicResource(Button.TextColorProperty, "TquillaGold")
                    .Bind(Button.IsEnabledProperty, getter:(MainViewModel vm) => vm.IsEnabled)
                    .Bind(Button.CommandProperty, getter:(MainViewModel vm) => vm.UpdateCredentialsCommand)
            }
        };

        background.Content = content;
        return background;
    }

    private enum Row {UserNameEntry,PasswordEntry}
    private enum Column {UsernamePasswordEntry,UsernamePasswordAdd, UsernamePasswordHide, ServerEntryUpdateAll, ServerDomainAdd}
}
