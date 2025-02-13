using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace MauiTquillaSunrise.ViewModel;
public partial class MainViewModel : BaseViewModel
{

    private static string _cmdKeyList = @"cmdkey /list";
    private static string _hide = "Press to _hide password.";
    private static string _show = "Press to _show password.";
    private static string _eyeIcon = "ic_fluent_eye_24_regular.png";
    private static string _eyeOffIcon = "ic_fluent_eye_off_24_regular.png";
    private static string _addIcon = "ic_fluent_add_24_filled.png";
    private static string _minusIcon = "ic_fluent_subtract_24_regular.png";
    private static string _drinkImage = "drinkicon.png";
    private static string _serverImage = "ic_fluent_server_24_regular.png";
    private static string _emptyDrinkImage = "emptydrinkicon.png";
    private const string _allDomain = "###-ALL-###";
    private const string _infoTag = "!***INFO***!";
    private const string _infoFormat = "Make sure servers are in a [servername].[domainname].[topleveldomain]:[port] format";

    private List<ServerModel> _serversSelected = new();
    private List<ServerModel> _allServers = new();

    private ServerModel _newServer;


    [ObservableProperty]
    DomainModel selectedDomain;

    private bool isBusy = false;
    private Dictionary<DomainModel, List<ServerModel>> domainDictionary = new();

    [ObservableProperty]
    string emptyDrinkIcon = _emptyDrinkImage;

    [ObservableProperty]
    string serverIcon = _serverImage;

    [ObservableProperty]
    string drinkIcon = _drinkImage;

    [ObservableProperty]
    string title = string.Empty;

    [ObservableProperty]
    string removeServerIcon = _minusIcon;

    [ObservableProperty]
    string revealPasswordButtonText = _show;

    [ObservableProperty]
    string revealPasswordButtonIcon = _eyeOffIcon;

    [ObservableProperty]
    string revealUsernameButtonText = _show;

    [ObservableProperty]
    string revealUsernameButtonIcon = _eyeOffIcon;

    [ObservableProperty]
    ObservableCollection<ServerModel> servers = new();

    [ObservableProperty]
    ObservableCollection<DomainModel> domains = new();

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
    string addButton = _addIcon;

    [ObservableProperty]
    bool isServerSelected = false;

    [ObservableProperty]
    int serverCount = 0;

    ObservableCollection<ServerModel> _invalidServers = new();
    public MainViewModel()
    {
        IsEnabled = Utilities.IsUserInitialized(User);
    }

    private void SetPickerDefault()
    {
        //should always have the ###-ALL-### DomainModel.
        SelectedDomain = Domains[0];

        var picker = new Picker { SelectedIndex = Domains.IndexOf(SelectedDomain) };
        PickerChanged_Event(picker, EventArgs.Empty);

    }

    private void ServerCollection_Changed(object? sender, EventArgs e)
    {
        ServerCount = Servers.Count;
    }
    private void SetPickerDefault(string domainName)
    {
        //should always have the ###-ALL-### DomainModel.
        SelectedDomain = Domains.First(d => d.DomainName == domainName);
        var picker = new Picker { SelectedIndex = Domains.IndexOf(SelectedDomain) };
        PickerChanged_Event(picker, EventArgs.Empty);
    }

    private void AddServers(ServerModel server)
    {
        if (server is null)
        {
            return;
        }
        else
        {
            ServerModel? found = _allServers.
                                   FirstOrDefault(s => $"{s.ServerName}.{s.DomainName}" == $"{server.ServerName}.{server.DomainName}");
            switch (found)
            {
                case null:
                    _allServers.Add(_newServer);
                    LoadDomains();
                    SetPickerDefault(_newServer.DomainName);
                    break;
                default:
                    _allServers.Remove(found);
                    _allServers.Add(_newServer);
                    break;
            }

            if (Utilities.IsUserInitialized(User) == true)
            {
                UpdateCredentials(ServerText);
            }
            ServerText = string.Empty;
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
            try
            {
                _newServer = null;
                _newServer = server.GetFullServerName().CreateServerModel();
                ServerModel? found = _allServers.
                                       FirstOrDefault(s => $"{s.ServerName}.{s.DomainName}" == $"{_newServer.ServerName}.{_newServer.DomainName}");
                switch (found)
                {
                    case null:
                        _allServers.Add(_newServer);
                        LoadDomains();
                        SetPickerDefault(_newServer.DomainName);
                        break;
                    default:
                        _allServers.Remove(found);
                        _allServers.Add(_newServer);
                        break;
                }

                if (Utilities.IsUserInitialized(User) == true)
                {
                    UpdateCredentials(ServerText);
                }
                _newServer = null;
                ServerText = string.Empty;
            }
            catch (Exception ex)
            {
                GeneralPopupService.GeneralAlertPopup("Error!", $"Cannot add server {server}. Error: {ex}", false);
            }
        }
    }
    private void LoadDomains()
    {
        Domains.Clear();
        //initialize dictionary with ALL
        domainDictionary.Clear();
        domainDictionary.Add(new DomainModel { DomainName = _allDomain }, _allServers.ToList());

        foreach (ServerModel server in _allServers)
        {
            var found = domainDictionary.FirstOrDefault(domains => domains.Key.DomainName == server.DomainName)
                                        .Key;

            if (found == null)
            {
                var domainModel = new DomainModel { DomainName = server.DomainName };
                var domainServerList = _allServers.Where(servers => servers.DomainName == domainModel.DomainName).ToList();
                domainDictionary.Add(domainModel, domainServerList);
            }
        }

        foreach (var domainModel in domainDictionary)
        {
            Domains.Add(domainModel.Key);
        }

        var sorted = Domains.SortCollection();
        if (sorted.success == false)
        {
            GeneralPopupService.GeneralAlertPopup("Warning!", $"Could not sort domains: {sorted.message}", true);
        }
    }

    [RelayCommand]
    public async void UpdateCredentials(string? serverText)
    {
        StringBuilder _message = new();
        string _title = "Updating Credentials";
        bool _errors = false;

        if (isBusy)
        {
            return;
        }

        isBusy = true;
        try
        {
            if (string.IsNullOrEmpty(serverText))
            {
                _message.AppendLine($"Update servers for selected domain: {SelectedDomain.DomainName}?");
                var confirm = await GeneralPopupService.GetUserConfirmationPopup(_title, _message.ToString(), Servers.ToList());
                if (confirm)
                {
                    _message.Clear();
                    try
                    {
                        foreach (ServerModel _server in Servers)
                        {
                            string addCmdKey = Utilities.FormatAddCmdKey(_server.ServerName, User.UserName, User.Password);
                            CmdCommand(addCmdKey);
                            if (CheckIfServerExists(_server) == false)
                            {
                                throw new Exception($"Unknown error adding {_server.ServerName}.{_server.DomainName}:{_server.Port} using CmdKey: {addCmdKey}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //error found/thrown manually trying to do reach loop
                        _errors = true;
                        _title = "Error";
                        _message.AppendLine($"{ex.Message}");
                    }
                }
                else
                {
                    //user cancelled
                    _message.Clear();
                    _message.AppendLine("Update cancelled.");
                }
            }
            else
            {
                //update single _server
                try
                {
                    string addCmdKey = Utilities.FormatAddCmdKey(serverText, User.UserName, User.Password);
                    CmdCommand(addCmdKey);
                    if (CheckIfServerExists(_newServer) == false)
                    {
                        throw new Exception($"Unknown error adding {serverText} using CmdKey: {addCmdKey}");
                    }
                }
                catch (Exception ex)
                {

                    _errors = true;
                    _title = "Error";
                    _message.AppendLine($"{ex.Message}");
                    RemoveServer(_newServer);
                }

            }
        }
        finally
        {
            isBusy = false;
            if (_errors == false)
            {
                _message.AppendLine("Done.");
            }
            GeneralPopupService.GeneralAlertPopup(_title, _message.ToString(), false);
        }
    }

    public void PageLoaded()
    {
        // LoadDummyServers();
        LoadServers();
        LoadDomains();
        SetPickerDefault();
        Servers.CollectionChanged += ServerCollection_Changed;
    }


    private async void RemoveServer(ServerModel server)
    {
        if (isBusy)
        {
            return;
        }
        isBusy = true;

        try
        {
            if (CheckIfServerExists(server))
            {
                await RemoveServerFromCollections(server);
            }
        }
        catch (Exception ex)
        {
            var _ = await GeneralPopupService.GetUserConfirmationPopup("Error removing server", $"{server.ServerName} could not be removed: {ex.Message}");
        }
        finally
        {
            isBusy = false;
        }
    }

    private async Task RemoveServerFromCollections(ServerModel server)
    {
        var serverDomain = Domains.First(d => d.DomainName == server.DomainName);
        var allDomains = Domains.First(d => d.DomainName == _allDomain);

        string removeCmdKey = Utilities.FormatDeleteCmdKey($"{server.ServerName}.{server.DomainName}:{server.Port}");
        CmdCommand(removeCmdKey);
        _allServers.Remove(server);
        domainDictionary[serverDomain].Remove(server);
        domainDictionary[allDomains].Remove(server);
        Servers.Remove(server);
        if (domainDictionary[serverDomain].Count <= 0)
        {
            LoadDomains();
            SetPickerDefault();
        }
        //else
        //{
        //    SetPickerDefault(server.DomainName);
        //}
    }

    [RelayCommand]
    public async void RemoveServer()
    {
        if (isBusy)
        {
            return;
        }
        isBusy = true;
        bool errors = false;
        try
        {
            StringBuilder message = new();
            string title = "Removing Servers";
            message.Append($"Managing: {SelectedDomain.DomainName} ");
            message.AppendLine($"Server Count: {_serversSelected.ToList().Count} ");

            var response = await GeneralPopupService.GetUserConfirmationPopup(title, message.ToString(), _serversSelected.ToList());
            if (response)
            {
                foreach (ServerModel server in _serversSelected.ToList())
                {
                    if (CheckIfServerExists(server))
                    {
                        await RemoveServerFromCollections(server);
                    }
                    else
                    {
                        message.AppendLine($"{server.ServerName} could not be found.");
                    }
                }
                if (errors == false)
                {
                    message.AppendLine("Done.");
                }
                GeneralPopupService.GeneralAlertPopup(title, message.ToString(), false);
            }
        }
        finally
        {
            isBusy = false;
        }
    }

    private bool CheckIfServerExists(ServerModel server)
    {
        bool output = false;
        var command = Utilities.FormatListCmdKey(server);
        var results = CmdCommand(command);

        if (results.Contains("* NONE *"))
        {
            output = false;
        }
        else
        {
            output = true;
        }

        return output;
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
        RevealPasswordButtonIcon = RevealPasswordButtonIcon == _eyeOffIcon ? _eyeIcon : _eyeOffIcon;
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

    private void LoadDummyServers()
    {
        var commands = Utilities.GenerateBogusServers(500);
        foreach (string commandLine in commands.Split("\n"))
        {
            CmdCommand(commandLine);
        }
    }


    private void LoadServers()
    {

        StringBuilder errors = new();
        string currentLine = string.Empty;
        string serverName = string.Empty;
        try
        {
            var results = CmdCommand(_cmdKeyList);
            results = results.TrimStart().Trim().TrimEnd();
            foreach (string line in results.Trim().Split("\n"))
            {
                try
                {
                    if (line.Contains("Domain:target="))
                    {
                        _newServer = null;
                        currentLine = line.Trim().TrimStart().TrimEnd();
                        int startIndex = currentLine.IndexOf('=') + 1;
                        int endIndex = currentLine.Length - startIndex;
                        
                        serverName = currentLine.Substring(startIndex, endIndex);

                        _newServer = serverName.GetFullServerName().CreateServerModel();

                        AddServers(_newServer);
                    }
                }
                catch (Exception)
                {
                    _invalidServers.Add(new ServerModel { ServerName = serverName });
                    _newServer = null;
                    continue;
                }
            }

            if (_invalidServers.Count > 0)
            {
                errors.AppendLine($"Cannot manage the following servers:");
                GeneralPopupService.GeneralAlertPopup(title:"Errors!", message:errors.ToString(), invalidServers:_invalidServers, isDismissable:false);
            }
        }
        catch (Exception ex)
        {
            errors.AppendLine($"General error: {ex.Message}");
            GeneralPopupService.GeneralAlertPopup(title: "Erorrs!", message: errors.ToString(), isDismissable:false);
        }
        //finally
        //{
        //    if (_invalidServers.Count > 0)
        //    {
        //        errors.Insert(0, $"Cannot manage the following\n");
        //        GeneralPopupService.GeneralAlertPopup(title:"Errors!", message:errors.ToString(), invalidServers:_invalidServers, isDismissable:false);
        //    }
        //}
    }

    [RelayCommand]
    public void RevealUsername()
    {
        IsUsernameShowing = !IsUsernameShowing;
        RevealUsernameButtonIcon = RevealUsernameButtonIcon == _eyeOffIcon ? _eyeIcon : _eyeOffIcon;
    }
    [RelayCommand]
    public void AddUsername(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            GeneralPopupService.GeneralAlertPopup("Adding Username", "Please provide username.", true);
            UserNameText = User.UserName ?? string.Empty;
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
            GeneralPopupService.GeneralAlertPopup("Adding Password", "Please provide password.", true);
            PasswordText = User.Password ?? string.Empty;
            return;
        }
        else
        {
            User.Password = password;
            IsEnabled = Utilities.IsUserInitialized(User);
        }
    }


    public void PickerChanged_Event(object sender, EventArgs e)
    {
        if (sender is Picker picker && picker.SelectedIndex >= 0)
        {
            SelectedDomain = Domains[picker.SelectedIndex];
            Servers.Clear();
            foreach (ServerModel server in domainDictionary[SelectedDomain])
            {
                Servers.Add(server);
            }
            ServerCollection_Changed(Servers, EventArgs.Empty);
            var sorted = Servers.SortCollection();
            if (sorted.success == false)
            {
                GeneralPopupService.GeneralAlertPopup("Warning!", $"Could not sort servers: {sorted.message}", true);
            }
        }
    }
}
