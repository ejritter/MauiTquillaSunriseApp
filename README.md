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

![image](https://github.com/user-attachments/assets/0f32d60c-5b64-4e2e-a3c7-c8b1d27e8178)



Once the app is loaded, user is free to enter username and password. 

![image](https://github.com/user-attachments/assets/aeeb4160-0834-4bb6-ae2e-092771e92a74)



User can hide their credentials pressing the hide eye icon.

![image](https://github.com/user-attachments/assets/162e9258-7cbf-40ab-acd4-f9063ce98830)



Once user has entered credentials, the enter SErver.domain.com:port Entry and Update All Credentials button will become available

![image](https://github.com/user-attachments/assets/fff47e05-4582-4099-a081-55675261bed9)



When adding a server, make sure to use the format listed in the Entry. If not, the server will not be managed by the app.

![image](https://github.com/user-attachments/assets/3c21e96a-fa11-4195-8a03-9a28589e1bf6)



User can choose to manage all domains at once

![image](https://github.com/user-attachments/assets/d9f6ac24-6fa4-41bb-b9e6-fb014dedd4b4)



Or specific domains

![image](https://github.com/user-attachments/assets/c9460f8b-d7a9-49cd-8c61-26008ed9d506)



When selecing a server in the Domain list, user has the option to remove it

![image](https://github.com/user-attachments/assets/5a95c1f2-0fb5-42a7-9781-5ad8c2152710)



If all servers within a domain are removed, that domain is removed and the Picker is defaulted to ###-ALL-###.
## DOES NOT
Does not validate the entered credentials against any systems. 
It is assumed that the user knows the correct credentials to enter for any domain or domains they are managing.

Although written in Maui, it is not targeting any platform outside of Windows.


### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with .NET MAUI workload installed
- Windows 10 or later (x64)

