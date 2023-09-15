using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Command.Models;

namespace TaskManager.ClientSDK
{
    public class MyClient : BaseClient
    {
        public MyClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken) : base(baseUrl, httpClient, cancellationToken)
        {
        }

        public async Task<UserModel?> GetMy()
        {
            string url = _baseUrl+"api/my";
            var response = await _httpClient.GetAsync(url);
            return await ResponseAsync<UserModel>(response);

        }
    }
}
