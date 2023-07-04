using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskManager.Command.Models;

namespace TaskManager.ClientSDK
{
    public partial class UsersClient:BaseClient<UserModel>
    {

        public UsersClient(string baseUrl, HttpClient httpClient,CancellationToken cancellationToken) :base(baseUrl + "/api/users", httpClient, cancellationToken) { }

        public Task<List<ProjectModel>?>? GetByUserId( int userId)
        {
            return _httpClient.GetFromJsonAsync<List<ProjectModel>>(_baseUrl + $"/{userId}", cancellationToken: _cancellationToken);
        }
    }
}
