# MauiTquillaSunriseApp# MauiTquillaSunriseApp

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Acknowledgements

- [.NET MAUI](https://dotnet.microsoft.com/apps/maui)
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/MVVM)

## Contact

For any questions or feedback, please contact me at ejritter87@gmail.com.
## Welcome!
This app is built to help me learn how to work with MAUI. It is a simple app that will allow you to manage your T-SQL credentials in Windows Credential Manager.
This is accomplished utilizing the cmdkey commands.

## DOES
Runs cmdkey /list up front and will parse all entries found. Errors will be reported if a server cannot be managed by the app.
!add error screenshot

Once the app is loaded, user is free to enter username and password. 
!add basic UI here

User can hide their credentials pressing the hide eye icon.
!add hide credentials here

Once user has entered credentials, the enter SErver.domain.com:port Entry and Update All Credentials button will become available
!add server entry available here

When adding a server, make sure to use the format listed in the Entry. If not, the server will not be managed by the app.
!add server entry error here

User can choose to manage all domains at once
!add all domains picker 

Or specific domains
!add selected domain here

When selecing a server in the Domain list, user has the option to remove it
!add remove server here

If all servers within a domain are removed, that domain is removed and the Picker is defaulted to ###-ALL-###.
## DOES NOT
Does not validate the entered credentials against any systems. 
It is assumed that the user knows the correct credentials to enter for any domain or domains they are managing.

Although written in Maui, it is not targeting any platform outside of Windows.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with .NET MAUI workload installed
- Windows 10 or later (x64)

