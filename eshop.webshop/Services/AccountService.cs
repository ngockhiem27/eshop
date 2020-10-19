using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using System.Net.Http;
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

        public Task<CustomerLoginResponse> AuthenticateAsync(LoginRequest loginRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
