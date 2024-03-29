using Microsoft.UI.Xaml.Controls;

using TaskManager.Client.WinUi.ViewModels;

namespace TaskManager.Client.WinUi.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
