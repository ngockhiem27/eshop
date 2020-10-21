using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface IAccountService
    {
        Task<CustomerLoginResponse> AuthenticateAsync(LoginRequest loginRequest);
        Task<CustomerLoginResponse> RegisterAsync(CustomerInfoRequest customerRequest);
    }
}