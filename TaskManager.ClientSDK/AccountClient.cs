using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Command.Models;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.ClientSDK
{
    public partial class AccountClient
    {
        protected readonly string _baseUrl;

        protected readonly HttpClient _httpClient;

        protected readonly CancellationToken _cancellationToken;

        public AccountClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken)
        {
            _baseUrl = baseUrl+ "/api/Account/";
            _httpClient = httpClient;
            _cancellationToken = cancellationToken;
        }

        protected virtual Task<bool> ResponseAsync(HttpResponseMessage httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }
        protected virtual async Task<T?> ResponseAsync<T>(HttpResponseMessage httpResponse) where T : class
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>(cancellationToken: _cancellationToken);
            }
            return null;
        }
        public async Task<AuthResponse> AuthToken(AuthRequest authRequest)
        {
            string url = _baseUrl + "AuthToken";
            var httpResponse = await _httpClient.PostAsJsonAsync(url, authRequest, cancellationToken: _cancellationToken);
            var response = await ResponseAsync<AuthResponse>(httpResponse);
            UserTokens.AuthResponse = response;
            if (response is null)
            {
                throw new NullReferenceException();
            }
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.Token);
            return response;
        }

        public async Task<bool> RefreshToken(RefreshTokenRequest refreshToken)
        {
            string url = _baseUrl + "RefreshToken";
            var httpResponse = await _httpClient.PostAsJsonAsync(url, refreshToken, cancellationToken: _cancellationToken);

            var response = await ResponseAsync<AuthResponse>(httpResponse);
            UserTokens.AuthResponse = response;
            if (response is null)
            {
                var temp = await httpResponse.Content.ReadAsStringAsync();
                throw new NullReferenceException();
            }
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.Token);
            return true;
        }

        //public async Task<AuthResponse> Registration(UserModel model)
        //{
        //    string url = _baseUrl + "Registration";
        //    var httpResponse = await _httpClient.PostAsJsonAsync(url, model, cancellationToken: _cancellationToken);
        //    var response = await ResponseAsync(httpResponse);
        //    if (response.IsSuccess)
        //        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.Token);
        //    return response;
        //}
    }
}
