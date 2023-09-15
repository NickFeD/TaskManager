using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace TaskManager.ClientSDK
{
    public partial class Client
    {
        private string _baseUrl;
        private HttpClient _httpClient;

        public UsersClient Users { get; set; }
        public AccountClient Account { get; set; }
        public MyClient My { get; set; }
        public Client(string baseUrl, System.Net.Http.HttpClient httpClient, CancellationToken cancellationToken)
        {
            Users = new (baseUrl,httpClient,cancellationToken);
            Account = new (baseUrl,httpClient,cancellationToken);
            My = new (baseUrl,httpClient,cancellationToken);
            _baseUrl = baseUrl;
            _httpClient = httpClient;
            //_settings = new System.Lazy<System.Text.Json.JsonSerializerOptions>(CreateSerializerSettings);
        }
        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }
    }
}
