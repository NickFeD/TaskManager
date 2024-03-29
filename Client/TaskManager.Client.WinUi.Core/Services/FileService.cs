using System.Collections.Concurrent;
using System.IO;
using System.Text;

using Newtonsoft.Json;

using TaskManager.Client.WinUi.Core.Contracts.Services;

namespace TaskManager.Client.WinUi.Core.Services;

public class FileService : IFileService
{
    private static readonly ConcurrentDictionary<string, object> _cdLock = new();

    public T Read<T>(string folderPath, string fileName)
    {
        
        var path = Path.Combine(folderPath, fileName);
        _cdLock.TryAdd(path, new object());
        lock (_cdLock[path])
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
        

        return default;
    }

    public void Save<T>(string folderPath, string fileName, T content)
    {
        var path = Path.Combine(folderPath, fileName);
        _cdLock.TryAdd(path, new object());
        var fileContent = JsonConvert.SerializeObject(content);
        lock (_cdLock[path])
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            
            File.WriteAllText(path, fileContent, Encoding.UTF8);
        }
            
    }

    public void Delete(string folderPath, string fileName)
    {

        var path = Path.Combine(folderPath, fileName);
        _cdLock.TryAdd(path, new object());

        lock (_cdLock[path])
        {
            if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
            {
                File.Delete(Path.Combine(folderPath, fileName));
            }
        }
            
    }
}
