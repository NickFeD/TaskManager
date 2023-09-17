using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Command.Models;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.ClientSDK
{
    public class MyClient : BaseClient
    {
        public MyClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken) : base(baseUrl+ "/api/my", httpClient, cancellationToken)
        {
        }

        public async Task<UserModel?> Get()
        {
            return await ResponseAsync<UserModel>(() => _httpClient.GetAsync(_httpClient.BaseAddress, _cancellationToken));
        }

        public async Task<bool> Update(UserModel model)
        {
            return await ResponseAsync(() => _httpClient.PutAsJsonAsync(_httpClient.BaseAddress,model,_cancellationToken));
        }

        public async Task<bool> DeleteMy(bool isConfirmed)
        {
            if (isConfirmed)
                return await ResponseAsync(() => _httpClient.DeleteAsync("?isConfirmed=true", _cancellationToken));
                return await ResponseAsync(() => _httpClient.DeleteAsync("?isConfirmed=false", _cancellationToken));
        }

        public async Task<List<ProjectModel>> GetMyProject()
        {
            return await ResponseAsync<List<ProjectModel>>(() => _httpClient.GetAsync("projects", _cancellationToken));
        }
    }
}
