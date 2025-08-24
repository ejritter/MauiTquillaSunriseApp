using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTquillaSunrise.Controls;

public class ServerLabel : Label
{
    public ServerLabel()
    {
        FontSize = 10;
        // Use theme resource for text color (vibrant gold for visibility)
        this.SetDynamicResource(Label.TextColorProperty, "TquillaBlue1");
    }
}
