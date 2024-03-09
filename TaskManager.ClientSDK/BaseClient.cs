using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Command.Models;

namespace TaskManager.ClientSDK
{
    public abstract class BaseClient
    {
        public BaseClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _httpClient = httpClient;
            _httpClient.BaseAddress=  new(baseUrl);
        }

        protected readonly HttpClient _httpClient;

        protected readonly CancellationToken _cancellationToken;

        //Скорей всего надо до писать
        protected virtual async Task<bool> ResponseAsync(Func<Task<HttpResponseMessage>> func)
        {
            var httpResponse = await func();
            if (httpResponse.IsSuccessStatusCode)
                return true;
            switch (httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    var response = await AccountClient.RefreshToken(_httpClient,
                        new RefreshTokenRequest { ExpiredToken = UserTokens.AuthResponse.Token, RefreshToken = UserTokens.AuthResponse.RefreshToken },
                        _cancellationToken);
                    return await ResponseAsync(func);
                default:
                    break;
            }
            return false;
        }

        protected virtual async Task<T?> ResponseAsync<T>(Func<Task<HttpResponseMessage>> func,bool ploho = false) where T : class
        {
            var httpResponse = await func();
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>(cancellationToken: _cancellationToken);
            }
            var content = httpResponse.Content.ReadAsStringAsync();
            switch (httpResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    var response = await AccountClient.RefreshToken(_httpClient,
                        new RefreshTokenRequest { ExpiredToken = UserTokens.AuthResponse.Token, RefreshToken = UserTokens.AuthResponse.RefreshToken },
                        _cancellationToken);
                    if (!ploho)
                        return await ResponseAsync<T>(func,true);
                    break;
                default:
                    break;
            }
            return null;
        }

        protected static async Task<T?> ResponseAsync<T>(HttpResponseMessage httpResponse,CancellationToken cancellationToken) where T : class
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
            }
            var content = httpResponse.Content.ReadAsStringAsync();
            return null;
        }
    }
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

        protected virtual Task<T?> ResponseAsync<T>(HttpResponseMessage httpResponse) where T : class
        {
            T? result = null;
            if (!httpResponse.IsSuccessStatusCode)
                return Task.Run(() => result);
            var httpContent = httpResponse.Content;
            return httpContent.ReadFromJsonAsync<T>(cancellationToken: _cancellationToken);
        }
    }
}
