using eshop.core.Entities;
using eshop.core.Helper;
using eshop.core.Interfaces.Repositories;
using eshop.core.Interfaces.Services;
using eshop.core.ViewModels;
using eshop.infrastructure.JwtAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.apiservices.Services
{
    public class CustomerAuthService : ICustomerAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtAuthManager _jwtAuthManager;

        public CustomerAuthService(ICustomerRepository customerRepository, IJwtAuthManager jwtAuthManager)
        {
            _customerRepository = customerRepository;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<CustomerAuthViewModel> AuthenticateAsync(string email, string password)
        {
            var passwordHashed = AuthenticateHelper.HashPassword(password);
            var customer = await _customerRepository.AuthenticateCustomerAsync(email, passwordHashed);
            if (customer == null) return null;
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim("Scope", "Customer")
                };
            var jwtResult = _jwtAuthManager.GenerateTokens(email, claims, DateTime.Now);
            return new CustomerAuthViewModel
            {
                Customer = customer,
                JwtResult = jwtResult
            };
        }

        public async Task<CustomerAuthViewModel> RegisterAsync(Customer newCustomer)
        {
            var customer = await _customerRepository.AddCustomerAsync(newCustomer);
            if (customer == null) return null;
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.Email),
                    new Claim("Scope", "Customer")
                };
            var jwtResult = _jwtAuthManager.GenerateTokens(customer.Email, claims, DateTime.Now);
            return new CustomerAuthViewModel
            {
                Customer = customer,
                JwtResult = jwtResult
            };
        }
    }
}
