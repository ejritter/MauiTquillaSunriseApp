namespace MauiTquillaSunrise.Utility;
public static class Utilities
{
    private static Random random = new();
    private const string _invalidFormat = @"Server cannot be managed. Please make sure server is formatted as 'ServerName.DomainName.TopLevelDomain.Port'";
    private const string _partialBlankServerName = @"Part of the full server name returned blank.. Could not be parsed.";
    public static string FormatAddCmdKey(string server, string username, string password)
    {
        string output;
        return output = $@"cmdkey /add:{server} /user:{username} /pass:{password}";
    }

     public static (string serverName, string fullDomainName, string port) GetFullServerName(this ServerModel server)
    {
        string serverName = "";
        string domainName = "";
        string fullDomainName = "";
        string topLevelDomain = "";
        string port = "";

        try
        {
            serverName = server.ServerName.ToString() ?? "";
            domainName = server.DomainName.Split('.')[0] ?? "";
            topLevelDomain = server.DomainName.Split('.')[1] ?? "";
            port = server.Port.ToString() ?? "";

            fullDomainName = $"{domainName}.{topLevelDomain}";
            
            if(serverName == "" || domainName == "" || topLevelDomain == "" ||
                port == "")
            {
                throw new InvalidDataException(_partialBlankServerName);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidDataException($"{_invalidFormat}. Exception thrown: {ex.Message}");
        }
        return (serverName, fullDomainName, port);
    }
    
    public static ServerModel CreateServerModel(this (string serverName, string fullDomainName, string port) fullServerName)
    {
            var output = new ServerModel()
            {
                ServerName = fullServerName.serverName,
                DomainName = fullServerName.fullDomainName,
                Port = fullServerName.port
            };

        return output;
    }

    public static (string serverName, string fullDomainName, string port) GetFullServerName(this string server)
    {
        StringBuilder errorMessage = new();
        string serverName = "";
        string domainName = "";
        string fullDomainName = "";
        string topLevelDomain = "";
        string port = "";
        
        try
        {
            if (server.IndexOf(":") < server.IndexOf("."))
            {
                throw new InvalidDataException(@": cannot come before .");
            }
            serverName = server.Split('.')[0].ToString() ?? "";
            domainName = server.Split('.')[1].ToString() ?? "";
            topLevelDomain = server.Split('.')[2].ToString() ?? "";
            port = server.Split(':')[1].ToString() ?? "";
            topLevelDomain = topLevelDomain.Replace($":{port}", null) ?? "";
            fullDomainName = $"{domainName}.{topLevelDomain}" ?? "";
            
            if(serverName == "" || domainName == "" || topLevelDomain == "" ||
                port == "")
            {
               throw new InvalidDataException(_partialBlankServerName);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidDataException($"{_invalidFormat}. Exception thrown: {ex.Message}");
        }
        return (serverName, fullDomainName, port);
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
        string output;
        if (string.IsNullOrEmpty(server.DomainName) ||
            string.IsNullOrEmpty(server.Port))
        {
            output = $"cmdkey /list:{server.ServerName}";
        }
        else
        {
            output = $"cmdkey /list:{server.ServerName}.{server.DomainName}:{server.Port}";
        }
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
