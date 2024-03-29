using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TaskManager.Client.WinUi.Core.Contracts.Services;
using TaskManager.Client.WinUi.Core.Helpers;
using TaskManager.Client.WinUi.Helpers;
using TaskManager.Client.WinUi.Models;
using Windows.Storage;

namespace TaskManager.Client.WinUi.Services;
internal class LocalUserDataService
{
    private const string _defaultApplicationDataFolder = "TaskManager.Client.WinUi/ApplicationData";
    private const string _defaultLocalSettingsFile = "LocalUserData.json";


    private readonly IFileService _fileService;

    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _applicationDataFolder;
    private readonly string _localsettingsFile;

    private IDictionary<string, object> _settings;

    private bool _isInitialized;

    public LocalUserDataService(IFileService fileService)
    {
        _fileService = fileService;

        //_applicationDataFolder = Path.Combine(_localApplicationData, _options.ApplicationDataFolder ?? _defaultApplicationDataFolder);
        //_localsettingsFile = _options.LocalSettingsFile ?? _defaultLocalSettingsFile;

        _settings = new Dictionary<string, object>();
    }



    private async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            _settings = await Task.Run(() => _fileService.Read<IDictionary<string, object>>(_applicationDataFolder, _localsettingsFile)) ?? new Dictionary<string, object>();

            _isInitialized = true;
        }
    }

    public async Task<T?> ReadAsync<T>(string key)
    {
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out var obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }
        }
        else
        {
            await InitializeAsync();

            if (_settings != null && _settings.TryGetValue(key, out var obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }
        }

        return default;
    }

    public async Task SaveAsync<T>(string key, T value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values[key] = await Json.StringifyAsync(value);
        }
        else
        {
            await InitializeAsync();

            _settings[key] = await Json.StringifyAsync(value);

            await Task.Run(() => _fileService.Save(_applicationDataFolder, _localsettingsFile, _settings));
        }
    }
}
