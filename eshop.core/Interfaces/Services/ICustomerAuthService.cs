using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Services
{
    public interface ICustomerAuthService
    {
        Task<CustomerAuthViewModel> AuthenticateAsync(string email, string password);
        Task<CustomerAuthViewModel> RegisterAsync(CustomerInfoRequest customerRequest);
    }
}