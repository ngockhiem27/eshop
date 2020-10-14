using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public class ManagerService : IManagerService
    {
        private readonly HttpClient _apiClient;
        public ManagerService(HttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<(HttpStatusCode, List<ManagerViewModel>)> GetAllManagerAsync()
        {
            var uri = API.Manager.GetAllManager();
            var response = await _apiClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ManagerViewModel>>(responseStream);
            return (statusCode, result);
        }

        public async Task<(HttpStatusCode, ManagerViewModel)> GetManagerById(int id)
        {
            var uri = API.Manager.GetManagerById(id);
            var response = await _apiClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ManagerViewModel>(responseStream);
            return (statusCode, result);
        }

        public async Task<HttpStatusCode> DeleteManager(int id)
        {
            var uri = API.Manager.DeleteManager(id);
            var response = await _apiClient.DeleteAsync(uri);
            return response.StatusCode;
        }

        public async Task<(HttpStatusCode, ManagerViewModel)> AddNewManager(ManagerInfoRequest manager)
        {
            var uri = API.Manager.AddManager();
            var serialized = new StringContent(JsonSerializer.Serialize(manager), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, serialized);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ManagerViewModel>(responseStream);
            return (statusCode, result);
        }

        public async Task<(HttpStatusCode, ManagerViewModel)> UpdateManager(ManagerInfoRequest manager)
        {
            var uri = API.Manager.AddManager();
            var serialized = new StringContent(JsonSerializer.Serialize(manager), Encoding.UTF8, "application/json");
            var response = await _apiClient.PutAsync(uri, serialized);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ManagerViewModel>(responseStream);
            return (statusCode, result);
        }
    }
}
