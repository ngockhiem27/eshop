using eshop.core.DTO.Request;
using eshop.core.DTO.Response;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface IAccountService
    {
        Task<ManagerLoginResponse> AuthenticateAsync(LoginRequest loginRequest);
    }
}
