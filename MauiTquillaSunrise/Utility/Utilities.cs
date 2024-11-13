


namespace MauiTquillaSunrise.Utility;
public static class Utilities
{
    private static Random random = new();
    public static string FormatAddCmdKey(string server, string username, string password)
    {
        string output;
        return output = $@"cmdkey /add:{server} /user:{username} /pass:{password}";
    }


    public static string GenerateBogusServers(int servers)
    {
        StringBuilder output = new();
        for (int i = 0; i < servers; i++)
        {
            int randomDomain = random.Next(0, (servers + 1));
            output.AppendLine($@"cmdkey /add:server{i}.domainname{randomDomain}.com:1433 /user:dummy /pass:fake123");
        }
        return output.ToString();
    }
    public static string FormatDeleteCmdKey(string server)
    {
        string output;
        return output = $"cmdkey /delete:{server}";
    }

    public static string FormatListCmdKey(ServerModel server)
    {
        string output = $"cmdkey /list:{server.ServerName}.{server.DomainName}:{server.Port}";

        return output;
    }


    public static (bool success, string message) SortCollection<T>(this ObservableCollection<T> collection) where T : new()
    {
        bool outputBool = false;
        string outputString = "";

        try
        {
            var item = new T();
            var properties = item.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Contains(nameof(DomainModel.DomainName)) ||
                    property.Name.Contains(nameof(ServerModel.ServerName)))
                {
                    var sortedCollection = collection.OrderBy(c => c.GetType().GetProperty(property.Name).GetValue(c, null)).ToList();
                    if (item is ServerModel)
                    {
                        sortedCollection.Clear();
                        sortedCollection = collection.OrderBy(c => c.GetType().GetProperty(nameof(DomainModel.DomainName)).GetValue(c, null))
                                                     .ThenBy(c => c.GetType().GetProperty(nameof(ServerModel.ServerName)).GetValue(c, null))
                                                     .ToList();
                    }
                    collection.Clear();

                    foreach (T sortedItem in sortedCollection)
                    {
                        collection.Add(sortedItem);
                    }
                }
                break;
            }

            if (item is DomainModel)
            {
                //enforce our ###-ALL-### to the top 
                var allDomains = collection.FirstOrDefault(c => c.GetType().GetProperty(nameof(DomainModel.DomainName)).GetValue(c, null).ToString() == "###-ALL-###");
                collection.Remove(allDomains);
                collection.Insert(0, allDomains);
            }
            outputBool = true;
        }
        catch (Exception ex)
        {
            outputBool = false;
            outputString = ex.Message;
        }
        return (outputBool, outputString);
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
