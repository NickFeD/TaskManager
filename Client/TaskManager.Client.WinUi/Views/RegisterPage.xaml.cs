using Microsoft.UI.Xaml.Controls;

using TaskManager.Client.WinUi.ViewModels;

namespace TaskManager.Client.WinUi.Views;

public sealed partial class RegisterPage : Page
{
    public RegisterViewModel ViewModel
    {
        get;
    }

    public RegisterPage()
    {
        ViewModel = App.GetService<RegisterViewModel>();
        InitializeComponent();
    }

    private void LoginLinkClick(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        Frame.Navigate(typeof(LoginPage));
    }
}
