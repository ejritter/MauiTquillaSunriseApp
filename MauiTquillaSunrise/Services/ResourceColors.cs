using System.Xml;
using System.Collections.Generic;

namespace MauiTquillaSunrise.Services;
public static class ResourceColors
{
    // Store color name to hex value mapping

    static ResourceColors()
    {
        LoadColorXml();
    }


        public static Color TquillaPrimaryColor => GetColorByName("TquillaPrimary");
        public static Color TquillaTextColor => GetColorByName("TquillaText");
        public static Color TquillaPlaceHolder => GetColorByName("TquillaPlaceHolder");
        public static Color TquillaIceImageFiller => GetColorByName("TquillaIceImageFiller");
        public static Color TquillaSkyBlue => GetColorByName("TquillaSkyBlue");
        public static Color TquillaBorderStroke => GetColorByName("TquillaBorderStroke");
        public static Color TquillaSecondary => GetColorByName("TquillaSecondary");
        public static Color TquillaDisabled => GetColorByName("TquillaDisabled");
        public static Color TquillaBackground => GetColorByName("TquillaBackground");


    private static readonly Dictionary<string, string> colorHexMap = new();

    private static void LoadColorXml()
    {
        xmlColors.Load(colorsPath);
        var resourceDictionary = xmlColors.DocumentElement.ChildNodes;

        // XML namespace for 'x' prefix
        var nsmgr = new XmlNamespaceManager(xmlColors.NameTable);
        nsmgr.AddNamespace("x", "http://schemas.microsoft.com/winfx/2009/xaml");

        if (resourceDictionary != null)
        {
            foreach (XmlNode node in resourceDictionary)
            {
                // Only process <Color> elements
                if (node.NodeType == XmlNodeType.Element && node.Name == "Color")
                {
                    var keyAttr = node.Attributes?["x:Key"] ?? node.Attributes?["Key", nsmgr.LookupNamespace("x")];
                    if (keyAttr != null)
                    {
                        var colorName = keyAttr.Value;
                        var colorValue = node.InnerText.Trim();
                        colorHexMap[colorName] = colorValue;
                    }
                }
            }
        }
    }

    private const string colorsPath = @"Resources\Styles\Colors.xaml";
    private static readonly XmlDocument xmlColors = new XmlDocument();

    // Returns the hex string (e.g. "#8B0000") for the given color name, or null if not found
    public static string? GetColorHexByName(string colorName)
    {
        if (colorHexMap.TryGetValue(colorName, out var hex))
            return hex;
        return null;
    }

    // Optionally, keep the original method for compatibility, returning a Color object
    public static Color GetColorByName(string colorName)
    {
        var hex = GetColorHexByName(colorName);
        if (!string.IsNullOrEmpty(hex))
        {
            try
            {
                return Color.FromArgb(hex);
            }
            catch
            {
                // Fallback to black if parsing fails
            }
        }
        return Color.FromArgb("Black");
    }

        public static string GetColorStringByName(string colorName)
    {
        var hex = GetColorHexByName(colorName);
        if (!string.IsNullOrEmpty(hex))
        {
            try
            {
                return hex;
            }
            catch
            {
                // Fallback to black if parsing fails
            }
        }
        return "Black";
    }
}
