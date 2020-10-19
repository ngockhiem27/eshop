using System.Threading.Tasks;
using eshop.core.ViewModels;

namespace eshop.core.Interfaces.Services
{
    public interface ICustomerAuthService
    {
        Task<CustomerAuthViewModel> AuthenticateAsync(string email, string password);
    }
}