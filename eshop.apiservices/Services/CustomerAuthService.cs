using eshop.core.DTO.Request;
using eshop.core.Interfaces.Services;
using eshop.core.ViewModels;
using System;
using System.Threading.Tasks;

namespace eshop.apiservices.Services
{
    public class CustomerAuthService : ICustomerAuthService
    {
        public async Task<CustomerAuthViewModel> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerAuthViewModel> RegisterAsync(CustomerInfoRequest customerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
