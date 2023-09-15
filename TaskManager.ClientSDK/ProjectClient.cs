//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;
//using TaskManager.Command.Models;
//using TaskManager.Command.Models.Abstracted;

//namespace TaskManager.ClientSDK
//{
//    public class ProjectClient : BaseClient
//    {
//        public ProjectClient(string baseUrl, HttpClient httpClient, CancellationToken cancellationToken) : base(baseUrl+ "/api/project", httpClient, cancellationToken)
//        {
//        }

//        public async Task<Response<ProjectModel>> Create(ProjectModel model)
//        {
//            string url = _baseUrl;
//            var httpResponse = await _httpClient.PostAsJsonAsync(url, model, cancellationToken: _cancellationToken);
//            return await ResponseAsync<ProjectModel>(httpResponse);
//        }
//        public Task<Response?> Delete(int id)
//        {
//            string url = _baseUrl+id;
//            return _httpClient.DeleteFromJsonAsync<Response>(url,cancellationToken: _cancellationToken);
//        }

//        public Task<Response<ProjectModel>?> GetById(int id)
//        {
//            string url = _baseUrl + id;
//            return _httpClient.GetFromJsonAsync<Response<ProjectModel>>(url,cancellationToken:_cancellationToken);
//        }

//        public async Task<Response?> Edit(int id, ProjectModel model)
//        {
//            string url = _baseUrl + id;
//            var httpResponse = await _httpClient.PutAsJsonAsync(url, model, cancellationToken: _cancellationToken);
//            return await ResponseAsync(httpResponse);
//        }

//        public async Task<Response> AddUsers(int projectId, params int[] usersId)
//        {
//            string url = _baseUrl + projectId + "/Users";
//            var httpResponse = await _httpClient.PostAsJsonAsync(url, usersId, cancellationToken: _cancellationToken);
//            return await ResponseAsync(httpResponse);
//        }
//        public Task<Response<List<UserRoleModel>>?> GetUsers(int id)
//        {
//            string url = _baseUrl + id + "/Users";
//            return _httpClient.GetFromJsonAsync< Response<List<UserRoleModel>>>(url, cancellationToken: _cancellationToken);
//        }
//    }
//}
