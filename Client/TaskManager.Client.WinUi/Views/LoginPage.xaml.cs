using Microsoft.UI.Xaml.Controls;

using TaskManager.Client.WinUi.ViewModels;
using TaskManager.Command.Models;

namespace TaskManager.Client.WinUi.Views;

public sealed partial class LoginPage : Page
{
    public LoginViewModel ViewModel
    {
        get;
    }

    public LoginPage()
    {
        ViewModel = App.GetService<LoginViewModel>();
        InitializeComponent();
    }
    private void RegisterLinkClick(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        Frame.Navigate(typeof(RegisterPage));
    }

    private async void LoginClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5023");
        TaskManagerAPIClient taskManagerAPIClient = new(client);
        var test = await taskManagerAPIClient.AuthTokenAsync(new AuthRequest() { Email = email.Text, Password = password.Password });
    }
}
