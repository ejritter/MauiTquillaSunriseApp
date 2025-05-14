

namespace MauiTquillaSunrise.Controls;
public class UserServerControls : ContentView
{
    public UserServerControls()
    {
        Content = IniatializeControls();
    }

    private Border IniatializeControls()
    {
        var picker = new CustomPicker()
        .ColumnSpan(All<Column>())
        .Row(Row.Picker)
        .Center()
        .Bottom()
        .Fill()
        .Bind(Picker.ItemsSourceProperty, mode: BindingMode.OneWay, getter: (MainViewModel vm) => vm.Domains)
        .Bind(Picker.SelectedItemProperty, mode: BindingMode.TwoWay, getter: (MainViewModel vm) => vm.SelectedDomain,
                                                                     setter: (MainViewModel vm, DomainModel dm) => vm.SelectedDomain = dm ?? null);

        // Add the behavior to connect the SelectedIndexChanged event to the command
        picker.Behaviors.Add(new EventToCommandBehavior
        {
            EventName = "SelectedIndexChanged",
            Command = new Command(() =>
            {
                if (this.BindingContext is MainViewModel vm)
                {
                    vm.SetServersToPickerSelectedItemCommand.Execute(null);
                }
            })
        });

        return new Border()
        {
            Padding = 15,
            Stroke = ResourceColors.TquillaBorderStroke,
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = 25
            },

            Content = new Grid()
            {
                ColumnDefinitions = Columns.Define(
                    (Column.LabelsAndCount, Star)),

                RowDefinitions = Rows.Define(
                    (Row.ServerCount, 50),
                    (Row.Picker, 70)),

                Children =
                            {
                                new Label()
                                    .Column(Column.LabelsAndCount)
                                    .Row(Row.ServerCount)
                                    .Start()
                                    .TextCenterHorizontal()
                                    .Bind(Label.TextProperty, getter:(MainViewModel vm) => vm.ServerStringCount)
                                    .TextColor(ResourceColors.TquillaTextColor),

                                picker
                                    .Center()
                                    .Row(Row.Picker)
                            }
            }.Top().Center()
        };
    }

    private enum Row { ServerCount, Picker }
    private enum Column { LabelsAndCount }
}
