using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using TaskManager.Client.WinUi.ViewModels;

namespace TaskManager.Client.WinUi.Views;

public sealed partial class AccountPage : Page
{
    public AccountViewModel ViewModel
    {
        get;
    }

    public AccountPage()
    {
        ViewModel = App.GetService<AccountViewModel>();
        InitializeComponent();
    }

    private void OnAccountButtonClick()
    {
        if (IsUserLoggedIn())
        {
            return;
        }
        else
        {
            
            this.Frame.Navigate(typeof(LoginPage));
        }
    }

    private bool IsUserLoggedIn()
    {
        // Здесь должна быть логика проверки статуса входа пользователя
        // Например, проверка наличия токена доступа или другого идентификатора сессии
        // Возвращаем true или false в зависимости от результата проверки
        return true; // или false
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        OnAccountButtonClick();
    }
}
