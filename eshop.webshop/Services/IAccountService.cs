using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using eshop.core.ViewModels;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface IAccountService
    {
        Task<CustomerLoginResponse> AuthenticateAsync(LoginRequest loginRequest);
        Task<CustomerLoginResponse> RegisterAsync(CustomerInfoRequest customerRequest);
        Task<(HttpStatusCode, CustomerViewModel)> UpdateAsync(int customerId, CustomerInfoRequest customerRequest);
        Task<(HttpStatusCode, CustomerViewModel)> GetAccountInfoAsync(int customerId);
    }
}