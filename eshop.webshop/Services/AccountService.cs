using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.webshop.Infrastructure;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerLoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            var uri = API.Authenticate.Login();
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode) return null;
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var customerAuthResult = await JsonSerializer.DeserializeAsync<CustomerLoginResponse>(responseStream);
            return customerAuthResult;
        }

        public async Task<CustomerLoginResponse> RegisterAsync(CustomerInfoRequest customerRequest)
        {
            var uri = API.Authenticate.Register();
            var content = new StringContent(JsonSerializer.Serialize(customerRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode) return null;
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var customerAuthResult = await JsonSerializer.DeserializeAsync<CustomerLoginResponse>(responseStream);
            return customerAuthResult;
        }
    }
}
