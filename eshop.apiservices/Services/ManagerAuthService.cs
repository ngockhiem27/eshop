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
    public class ManagerAuthService : IManagerAuthService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IJwtAuthManager _jwtAuthManager;

        public ManagerAuthService(IManagerRepository managerRepository, IJwtAuthManager jwtAuthManager)
        {
            _managerRepository = managerRepository;
            _jwtAuthManager = jwtAuthManager;
        }

        public async Task<ManagerAuthViewModel> AuthenticateAsync(string email, string password)
        {
            var passwordHashed = AuthenticateHelper.HashPassword(password);
            var manager = await _managerRepository.AuthenticateManagerAsync(email, passwordHashed);
            if (manager == null) return null;
            var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, manager.Id.ToString()),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, manager.Role_Name),
                    new Claim("Scope", "Manager")
                };
            var jwtResult = _jwtAuthManager.GenerateTokens(email, claims, DateTime.Now);
            return new ManagerAuthViewModel
            {
                Manager = manager,
                JwtResult = jwtResult
            };
        }
    }
}
