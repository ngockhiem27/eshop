using eshop.core.Entities;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Repositories
{
    public interface IManagerRepository
    {
        public Task<List<ManagerViewModel>> GetAllManagersAsync();
        public Task<ManagerViewModel> AddManagerAsync(Manager manager);
        public Task<ManagerViewModel> GetManagerByIdAsync(int id);
        public Task<ManagerViewModel> GetManagerByEmailAsync(string email);
        public Task<ManagerViewModel> AuthenticateManagerAsync(string email, string passwordHash);
        public Task<int> DeleteManagerAsync(int id);
        public Task<ManagerViewModel> UpdateManagerAsync(Manager manager);
    }
}
