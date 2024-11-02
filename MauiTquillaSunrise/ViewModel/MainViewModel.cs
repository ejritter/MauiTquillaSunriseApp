using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiTquillaSunrise.ViewModel;
public partial class MainViewModel : ObservableObject
{
    private static string cmdKeyList = @"cmdkey /list";
    private static string hide = "Press to hide password.";
    private static string show = "Press to show password.";
    private static string eyeIcon = "ic_fluent_eye_24_regular.png";
    private static string eyeOffIcon = "ic_fluent_eye_off_24_regular.png";
    private static string addIcon = "ic_fluent_add_24_filled.png";
    private static string minusIcon = "ic_fluent_subtract_24_regular.png";

    private Page _currentPage;
    private List<ServerModel> _serversSelected = new();

    [ObservableProperty]
    string title = "Maui T-Quilla Sunrise";

    [ObservableProperty]
    string removeServerIcon = minusIcon;

    [ObservableProperty]
    string revealPasswordButtonText = show;

    [ObservableProperty]
    string revealPasswordButtonIcon = eyeOffIcon;

    [ObservableProperty]
    string revealUsernameButtonText = show;

    [ObservableProperty]
    string revealUsernameButtonIcon = eyeOffIcon;

    [ObservableProperty]
    ObservableCollection<ServerModel> servers = new();

    [ObservableProperty]
    UserModel user = new();

    [ObservableProperty]
    string userNameText = string.Empty;

    [ObservableProperty]
    string passwordText = string.Empty;

    [ObservableProperty]
    string serverText = string.Empty;

    [ObservableProperty]
    bool isEnabled;

    [ObservableProperty]
    bool isPasswordShowing = false;
    
    [ObservableProperty]
    bool isUsernameShowing = false;

    [ObservableProperty]
    string addButton = addIcon;

    [ObservableProperty]
    bool isServerSelected = false;

    public MainViewModel()
    {
        IsEnabled = Utilities.IsUserInitialized(User);
        LoadServers();
    }

    public void PageLoaded()
    {
        _currentPage = (Page)Application.Current.MainPage;
    }

    [RelayCommand]
    public void RemoveServer()
    {
        StringBuilder message = new();
        message.AppendLine("Servers removed:");
        foreach (ServerModel server in _serversSelected.ToList())
        {
            message.AppendLine(server.ServerName);
            Servers.Remove(server);
            string removeCmdKey = Utilities.FormatDeleteCmdKey(server.ServerName);
            CmdCommand(removeCmdKey);
        }
        DisplayPopup("RemoveServer()", message.ToString());
    }

    [RelayCommand]
    public void ServerSelected(object sender)
    {
        if (sender is CollectionView servers)
        {
            if (servers.SelectedItems.Count > 0)
            {
                IsServerSelected = true;
                _serversSelected.Clear();
                _serversSelected = servers.SelectedItems.Cast<ServerModel>().ToList();
            }
            else
            {
                IsServerSelected = false;
                _serversSelected.Clear();
            }
        }
    }

    [RelayCommand]
    public void RevealPassword()
    {
        IsPasswordShowing = !IsPasswordShowing;
        RevealPasswordButtonIcon = RevealPasswordButtonIcon == eyeOffIcon ? eyeIcon : eyeOffIcon;
    }

    [RelayCommand]
    public async void UpdateCredentials(string? serverText)
    {
        string message = string.Empty;
        string title = "Updating Credentials";

        if (string.IsNullOrEmpty(serverText))
        {
            var confirm = await GetUserConfirmationPopup("Warning!", "You are about to update all servers in your vault to the current set username and password.");
            if (confirm)
            {
                foreach (ServerModel server in Servers)
                {
                    string addCmdKey = Utilities.FormatAddCmdKey(server.ServerName, User.UserName, User.Password);
                    message = "All server credentials updated to current set username and password.";
                    CmdCommand(addCmdKey);
                } 
            }
            else
            {
                message = "Update canceled";
            }
        }
        else
        {
            string addCmdKey = Utilities.FormatAddCmdKey(serverText, User.UserName, User.Password);
            CmdCommand(addCmdKey);
            title = "Added Server";
            message = $"{serverText} has been added.";
        }
        DisplayPopup(title, message);
    }

    private void DisplayPopup(string title, string message)
    {
        if (Shell.Current?.CurrentPage != null)
        {
            _currentPage.DisplayAlert(title, message, "OK");
        }
    }

    private async Task<bool> GetUserConfirmationPopup(string title, string message)
    {
        bool response = await _currentPage.DisplayAlert(title, message, "OK","CANCEL");
        return response;
    }

    private static string CmdCommand(string command)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();
        cmd.StandardInput.WriteLine(command);
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        string textBlob = cmd.StandardOutput.ReadToEnd();
        return textBlob;
    }

    private void LoadServers()
    {
        var results = CmdCommand(cmdKeyList);
        foreach (string line in results.Split("\n"))
        {
            if (line.Contains("Domain:target="))
            {
                int startIndex = line.IndexOf('=') + 1;
                int endIndex = line.Length - startIndex;
                string serverName = line.Substring(startIndex, endIndex);
                AddServer(serverName);
            }
        }
    }


    [RelayCommand]
    public void RevealUsername()
    {
        IsUsernameShowing = !IsUsernameShowing;
        RevealUsernameButtonIcon = RevealUsernameButtonIcon == eyeOffIcon ? eyeIcon : eyeOffIcon;
    }
    [RelayCommand]
    public void AddUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            DisplayPopup("AddUserName()", "Please provide username.");
            UserNameText = User.UserName;
            return;
        }
        else
        {
            User.UserName = userName;
            IsEnabled = Utilities.IsUserInitialized(User);
        }
    }

    [RelayCommand]
    public void AddPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            DisplayPopup("AddPassword()", "Please provide password.");
            PasswordText = User.Password;
            return;
        }
        else
        {
            User.Password = password;
            IsEnabled = Utilities.IsUserInitialized(User);
        }
    }

    [RelayCommand]
    public void AddServer(string server)
    {
        if (string.IsNullOrEmpty(server))
        {
            return;
        }
        else
        {
            ServerModel newServer = new ServerModel()
            {
                ServerName = server
            };

            ServerModel? found = Servers.FirstOrDefault(s => s.ServerName == newServer.ServerName);
            if (found != null)
            {
                Servers.Remove(found);
            }
            Servers.Add(newServer);
            Utilities.SortServerList(Servers);

            if (Utilities.IsUserInitialized(User) == true)
            {
                UpdateCredentials(ServerText);
            }
            ServerText = string.Empty;
        }
    }
}
