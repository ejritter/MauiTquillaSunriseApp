using mauiColors =  Microsoft.Maui.Graphics;

namespace MauiTquillaSunrise.View;
public class ServerModelDataTemplate : DataTemplate
{
    public ServerModelDataTemplate() : base(() => CreateServerModelGrid()
                                            .Top()
                                    .CenterHorizontal())
    {
    }

   private static Microsoft.Maui.Controls.View CreateServerModelGrid()
    {
        // Resolve theme colors once
        var normalBg   = (Color)Application.Current.Resources["TquillaDarkPurpleTransparent"];
        var selectedBg = (Color)Application.Current.Resources["TquillaSecondary"];
        var selectedStroke = (Color)Application.Current.Resources["TquillaGold"];

        var grid = new Grid
        {
            RowDefinitions = Rows.Define((Row.ServerModel, 55)),
            ColumnDefinitions = Columns.Define((Column.DrinkImage, 60), (Column.Labels, Star)),
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
                    .Bind(Image.SourceProperty, source: "drinkicon.png"),

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
                    .Bottom()
                    .End()
                    .Bind(Label.TextProperty, nameof(ServerModel.Port))
            }
        };

        // Root container that will receive Selected/Normal visual states from CollectionView
        var border = new Border
        {
            Padding = 4,
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle { CornerRadius = 12 },
            BackgroundColor = normalBg,
            Stroke = mauiColors.Colors.Transparent,
            Content = grid
        };

        VisualStateManager.SetVisualStateGroups(border, new VisualStateGroupList
        {
            new VisualStateGroup
            {
                Name = "CommonStates",
                States =
                {
                    new VisualState
                    {
                        Name = "Normal",
                        Setters =
                        {
                            new Setter { Property = Border.BackgroundColorProperty, Value = normalBg },
                            new Setter { Property = Border.StrokeProperty,           Value = mauiColors.Colors.Transparent },
                            new Setter { Property = Border.ScaleProperty,            Value = 1.0 }
                        }
                    },
                    new VisualState
                    {
                        Name = "Selected",
                        Setters =
                        {
                            new Setter { Property = Border.BackgroundColorProperty, Value = selectedBg },
                            new Setter { Property = Border.StrokeProperty,           Value = selectedStroke },
                            new Setter { Property = Border.ScaleProperty,            Value = 0.98 }
                        }
                    }
                }
            }
        });

        return border;
    }

    private enum Row { ServerModel }
    private enum Column { DrinkImage, Labels }
}
