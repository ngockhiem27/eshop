using eshop.core.ViewModels;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Services
{
    public interface IManagerAuthService
    {
        Task<ManagerAuthViewModel> AuthenticateAsync(string email, string password);
    }
}
