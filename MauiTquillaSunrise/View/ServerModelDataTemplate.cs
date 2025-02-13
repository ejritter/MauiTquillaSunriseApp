namespace MauiTquillaSunrise.View;
public class ServerModelDataTemplate : DataTemplate
{
    public ServerModelDataTemplate() : base(() => CreateServerModelGrid().Top())
    {

    }

    private static Grid CreateServerModelGrid() => new Grid()
    {
        RowDefinitions = Rows.Define(
            (Row.ServerModel, 50)),
        //width
        ColumnDefinitions = Columns.Define(
            (Column.DrinkImage,10),
            (Column.Labels,80)),
        
        RowSpacing = 5,
        ColumnSpacing = 2,
        Margin = 8,
        Padding = 8,

        Children =
        {
            new Image()
                .Column(Column.DrinkImage)
                .Row(Row.ServerModel)
                .Height(50)
                .Width(30)
            //TODO fix this so that imgae paths are not reliant on viewmodels
                .Bind(Image.SourceProperty, source:"drinkicon.png"),

            new Label()
                .Column(Column.Labels)
                .Row(Row.ServerModel)
                .Top()
                .Center()
                .Bind(Label.TextProperty, nameof(ServerModel.ServerName)),

           new Label()
                .Column(Column.Labels)
                .Row(Row.ServerModel)
                .CenterVertical()
                .Center()
                .Bind(Label.TextProperty, nameof(ServerModel.DomainName)),

           new Label()
                .Column(Column.Labels)
                .Row(Row.ServerModel)
                .Bind(Label.TextProperty, nameof(ServerModel.Port))
                .Bottom()
                .Center()
        }
    };

    private enum Row { ServerModel }
    private enum Column { DrinkImage, Labels }
}
