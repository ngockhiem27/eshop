using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using eshop.infrastructure.RedisCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Cache
{
    public class ManagerCached : BaseCached, IManagerCached
    {
        private readonly IManagerRepository _managerRepository;
        private const string KEY_PREFIX = "Manager.";

        public ManagerCached(ICache cache, IManagerRepository managerRepository) : base(cache)
        {
            _managerRepository = managerRepository;
        }

        public async Task<List<ManagerViewModel>> GetAllManagersAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllManagersAsync));
            return await VerifyCache(cachedKey, () => _managerRepository.GetAllManagersAsync());
        }

        public async Task<ManagerViewModel> GetManagerByIdAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetManagerByIdAsync), id);
            return await VerifyCache(cachedKey, () => _managerRepository.GetManagerByIdAsync(id));
        }

        public async Task<List<RoleViewModel>> GetAllRoleAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllRoleAsync));
            return await VerifyCache(cachedKey, () => _managerRepository.GetAllRoleAsync());
        }

        public async Task<ManagerViewModel> AddManagerAsync(Manager manager)
        {
            await ClearGetAllKeysAsync();
            return await _managerRepository.AddManagerAsync(manager);
        }

        public async Task<ManagerViewModel> AuthenticateManagerAsync(string email, string passwordHash)
        {
            return await _managerRepository.AuthenticateManagerAsync(email, passwordHash);
        }

        public async Task<int> DeleteManagerAsync(int id)
        {
            await ClearGetAllKeysAsync();
            await ClearGetIdKeyAsync(id);
            return await _managerRepository.DeleteManagerAsync(id);
        }

        public async Task<ManagerViewModel> UpdateManagerAsync(int id, Manager manager)
        {
            await ClearGetAllKeysAsync();
            await ClearGetIdKeyAsync(id);
            return await _managerRepository.UpdateManagerAsync(id, manager);
        }

        private async Task ClearGetAllKeysAsync()
        {
            var cachedKeyAll1 = GetKey(KEY_PREFIX, nameof(GetAllManagersAsync));
            var cachedKeyAll2 = GetKey(KEY_PREFIX, nameof(GetAllRoleAsync));
            await _cache.RemoveAsync(cachedKeyAll1);
            await _cache.RemoveAsync(cachedKeyAll2);
        }

        private async Task ClearGetIdKeyAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetManagerByIdAsync), id);
            await _cache.RemoveAsync(cachedKey);
        }
    }
}
