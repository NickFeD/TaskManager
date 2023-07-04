using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ClientSDK
{
    public abstract class BaseClient<TModel> where TModel : class
    {
        protected readonly string _baseUrl;

        protected HttpClient _httpClient;

        protected CancellationToken _cancellationToken;
        public BaseClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _baseUrl = new(baseUrl);
            _httpClient = httpClient;
        }

        public virtual Task<List<TModel>?> GetAllAsync()
        {
            return _httpClient.GetFromJsonAsync<List<TModel>?>(_baseUrl, _cancellationToken);
        }
        public virtual Task<TModel?> GetByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<TModel>(_baseUrl + $"/{id}", _cancellationToken);
        }

        public virtual async Task<TModel?> CreateAsync(TModel model)
        {
            var httpResponse = await _httpClient.PostAsJsonAsync<TModel>(_baseUrl, model, cancellationToken: _cancellationToken);
            return await ResponseAsync<TModel>(httpResponse);
        }

        public virtual Task UpdateAsync(int id, TModel model)
        {
            return _httpClient.PutAsJsonAsync(_baseUrl + $"/{id}", model, cancellationToken: _cancellationToken);
        }

        public virtual Task DeleteAsync(int id)
        {
            return _httpClient.DeleteAsync(_baseUrl + $"/{id}", cancellationToken: _cancellationToken);
        }

        protected Task<T?> ResponseAsync<T>(HttpResponseMessage httpResponse) where T : class
        {
            T? result = null;
            if (!httpResponse.IsSuccessStatusCode)
                return Task.Run(() => result);
            var httpContent = httpResponse.Content;
            return httpContent.ReadFromJsonAsync<T>(cancellationToken: _cancellationToken);
        }
    }
}
