namespace MauiTquillaSunrise.Controls;
public class UserServerControls : ContentView
{
    public UserServerControls()
    {
        Content = IniatializeControls();
    }

    private Microsoft.Maui.Controls.View IniatializeControls()
    {
        var background = new Border
        {
            Padding = 14,
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle { CornerRadius = 20 }
        }
        .DynamicResource(Border.StrokeProperty, "TquillaBorderStroke");
        //.DynamicResource(Border.BackgroundColorProperty, "TquillaDarkPurple");

        // Restore picker variable so we can attach behavior to fire SetServersToPickerSelectedItemCommand
        var picker = new CustomPicker()
            .ColumnSpan(All<Column>())
            .Row(Row.Picker)
            .Center()
            .Bottom()
            .Fill()
            .Bind(Picker.ItemsSourceProperty, mode: BindingMode.OneWay, getter: (MainViewModel vm) => vm.Domains)
            .Bind(Picker.SelectedItemProperty, mode: BindingMode.TwoWay, getter: (MainViewModel vm) => vm.SelectedDomain,
                                                                     setter: (MainViewModel vm, DomainModel dm) => vm.SelectedDomain = dm ?? null);

        // Fire VM command whenever the picker selection changes
        picker.Behaviors.Add(new EventToCommandBehavior
        {
            EventName = nameof(Picker.SelectedIndexChanged),
            Command = new Command(() =>
            {
                if (BindingContext is MainViewModel vm)
                {
                    vm.SetServersToPickerSelectedItemCommand.Execute(null);
                }
            })
        });

        var content = new Grid()
        {
            ColumnDefinitions = Columns.Define(
                (Column.LabelsAndCount, Star)),

            RowDefinitions = Rows.Define(
                (Row.ServerCount, 50),
                (Row.Picker, 70)),

            Children =
            {
                picker
            }
        };

        background.Content = content;
        return background;
    }

    private enum Row { ServerCount, Picker }
    private enum Column { LabelsAndCount }
}
