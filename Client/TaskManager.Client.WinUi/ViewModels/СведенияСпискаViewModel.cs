using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using TaskManager.Client.WinUi.Contracts.ViewModels;
using TaskManager.Client.WinUi.Core.Contracts.Services;
using TaskManager.Client.WinUi.Core.Models;

namespace TaskManager.Client.WinUi.ViewModels;

public partial class СведенияСпискаViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    [ObservableProperty]
    private SampleOrder? selected;

    public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

    public СведенияСпискаViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        SampleItems.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetListDetailsDataAsync();

        foreach (var item in data)
        {
            SampleItems.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        Selected ??= SampleItems.First();
    }
}
