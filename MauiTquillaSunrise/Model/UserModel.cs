
namespace MauiTquillaSunrise.Model;
public partial class UserModel : ObservableObject
{
    [ObservableProperty]
    string userName = string.Empty;

    [ObservableProperty]
    string password = string.Empty;
}
