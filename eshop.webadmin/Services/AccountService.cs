using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.webadmin.Infrastructure;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _apiClient;

        public AccountService(HttpClient httpClient)
        {
            _apiClient = httpClient;
        }

        public async Task<ManagerLoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            var uri = API.Authenticate.Login();
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode) return null;
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var managerAuthResult = await JsonSerializer.DeserializeAsync<ManagerLoginResponse>(responseStream);
            return managerAuthResult;
        }
    }
}
