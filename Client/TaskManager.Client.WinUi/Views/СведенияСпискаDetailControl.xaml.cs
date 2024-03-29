using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using TaskManager.Client.WinUi.Core.Models;

namespace TaskManager.Client.WinUi.Views;

public sealed partial class СведенияСпискаDetailControl : UserControl
{
    public SampleOrder? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as SampleOrder;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(SampleOrder), typeof(СведенияСпискаDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public СведенияСпискаDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is СведенияСпискаDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
