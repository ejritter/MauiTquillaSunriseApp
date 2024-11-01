
using System.Collections.ObjectModel;

namespace MauiTquillaSunrise.Utility;
public static class Utilities
{
    public static string FormatAddCmdKey(string server, string username, string password)
    {
        string output;
        return output = $@"cmdkey /add:{server} /user:{username} /pass:{password}";
    }

    public static string FormatDeleteCmdKey(string server)
    {
        string output;
        return output = $"cmdkey /delete:{server}";
    }

    public static void SortServerList(ObservableCollection<ServerModel> servers)
    {
        var sortedServers = servers.OrderBy(s => s.ServerName).ToList();
        servers.Clear();

        foreach (var sortedServer in sortedServers)
        {
            servers.Add(sortedServer);
        }
    }

    public static bool IsUserInitialized(UserModel user)
    {
        if ((string.IsNullOrEmpty(user.Password) == false &&
            string.IsNullOrEmpty(user.UserName) == false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
