namespace MauiTquillaSunrise.View;
public class ServerModelDataTemplate : DataTemplate
{
    public ServerModelDataTemplate() : base(() => CreateServerModelGrid().Top().CenterHorizontal())
    {

    }

    private static Grid CreateServerModelGrid() => new Grid()
    {
        RowDefinitions = Rows.Define(
            (Row.ServerModel, 55)),
        //width
        ColumnDefinitions = Columns.Define(
            (Column.DrinkImage,60),
            (Column.Labels,Star)),
        
        RowSpacing = 2,
        ColumnSpacing = 2,
        Margin = 2,
        Padding = 2,

        Children =
        {
            new Image()
                .Column(Column.DrinkImage)
                .Row(Row.ServerModel)
                .Height(50)
                .Width(30)
            //TODO fix this so that imgae paths are not reliant on viewmodels
                .Bind(Image.SourceProperty, source:"drinkicon.png"),

            new ServerLabel()
                .Column(Column.Labels)
                .Row(Row.ServerModel)
                .Top()
                .End()
                .Bind(Label.TextProperty, nameof(ServerModel.ServerName)),

           new ServerLabel()
                .Column(Column.Labels)
                .Row(Row.ServerModel)
                .CenterVertical()
                .End()
                .Bind(Label.TextProperty, nameof(ServerModel.DomainName)),

           new ServerLabel()
                .Column(Column.Labels)
                .Row(Row.ServerModel)
                .Bind(Label.TextProperty, nameof(ServerModel.Port))
                .Bottom()
                .End()
        }
    };

    private enum Row { ServerModel }
    private enum Column { DrinkImage, Labels }
}
