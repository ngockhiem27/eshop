using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.core.Interfaces.Services;
using eshop.core.ViewModels;

namespace eshop.apiservices.Services
{
    public class CustomerAuthService : ICustomerAuthService
    {
        public Task<CustomerAuthViewModel> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
