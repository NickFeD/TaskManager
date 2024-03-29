using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

using TaskManager.Client.WinUi.ViewModels;

namespace TaskManager.Client.WinUi.Views;

public sealed partial class СведенияСпискаPage : Page
{
    public СведенияСпискаViewModel ViewModel
    {
        get;
    }

    public СведенияСпискаPage()
    {
        ViewModel = App.GetService<СведенияСпискаViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
