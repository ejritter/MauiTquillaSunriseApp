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
    private List<ServerModel> _serversSelected;

    [ObservableProperty]
    string title = "Maui T-Quilla Sunrise";

    [ObservableProperty]
    string removeServerIcon;

    [ObservableProperty]
    string revealPasswordButtonText;

    [ObservableProperty]
    string revealPasswordButtonIcon;

    [ObservableProperty]
    ObservableCollection<ServerModel> servers;

    [ObservableProperty]
    UserModel user;

    [ObservableProperty]
    string userNameText;

    [ObservableProperty]
    string passwordText;

    [ObservableProperty]
    string serverText;

    [ObservableProperty]
    bool isEnabled;

    [ObservableProperty]
    bool isShowing;

    [ObservableProperty]
    string addButton;

    [ObservableProperty]
    bool isServerSelected;

    public MainViewModel()
    {
        Servers = new();
        User = new();
        UserNameText = string.Empty;
        PasswordText = string.Empty;
        ServerText = string.Empty;
        IsEnabled = Utilities.IsUserInitialized(User);
        IsShowing = true;
        RevealPasswordButtonText = show;
        RevealPasswordButtonIcon = eyeOffIcon;
        RemoveServerIcon = minusIcon;
        IsServerSelected = false;
        _serversSelected = new();
        AddButton = addIcon;
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
        IsShowing = !IsShowing;
        RevealPasswordButtonIcon = RevealPasswordButtonIcon == eyeOffIcon ? eyeIcon : eyeOffIcon;
    }

    [RelayCommand]
    public void UpdateCredentials(string? serverText)
    {
        string message = string.Empty;
        if (string.IsNullOrEmpty(serverText))
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
            string addCmdKey = Utilities.FormatAddCmdKey(serverText, User.UserName, User.Password);
            CmdCommand(addCmdKey);
            message = $"{serverText} has been added.";
        }
        DisplayPopup("UpdateCredentials", message);
    }

    private void DisplayPopup(string title, string message)
    {
        if (Shell.Current?.CurrentPage != null)
        {
            _currentPage.DisplayAlert(title, message, "OK");
        }
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
