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
    public partial class AccountClient : BaseClient
    {
        public AccountClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken) : 
            base(baseUrl, httpClient, cancellationToken)
        {
        }
        public async Task<AuthResponse> AuthToken(AuthRequest authRequest)
        {
            var response = await ResponseAsync<AuthResponse>(()=> _httpClient.PostAsJsonAsync("/api/Account/AuthToken", authRequest, cancellationToken: _cancellationToken));
            if (response is null)
            {
                return null;
            }
            UserTokens.AuthResponse = response;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.Token);
            return response;
        }

        public async Task<bool> RefreshToken(RefreshTokenRequest refreshToken)
        {

            var response = await ResponseAsync<AuthResponse>(()=>_httpClient.PostAsJsonAsync("/api/Account/RefreshToken", refreshToken, cancellationToken: _cancellationToken));
            if (response is null)
            {
                throw new NullReferenceException();
            }
            UserTokens.AuthResponse = response;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", response.Token);
            return true;
        }

        public static async Task<bool> RefreshToken(HttpClient httpClient, RefreshTokenRequest refreshToken,CancellationToken cancellationToken = new())
        {
            var httpResponse = await httpClient.PostAsJsonAsync("/api/Account/RefreshToken", refreshToken, cancellationToken: cancellationToken);

            var response = await ResponseAsync<AuthResponse>(httpResponse,cancellationToken);
            if (response is null)
            {
                var temp = await httpResponse.Content.ReadAsStringAsync();
                throw new NullReferenceException();
            }
            UserTokens.AuthResponse = response;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( "Bearer", response.Token);
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
