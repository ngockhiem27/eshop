using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using eshop.infrastructure.RedisCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Cache
{
    public class CustomerCached : BaseCached, ICustomerCached
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly string KEY_PREFIX = "Customer.";

        public CustomerCached(IRedisCache cache, ICustomerRepository customerRepository) : base(cache)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerViewModel>> GetAllCustomersAsync()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllCustomersAsync));
            return await VerifyCache(cachedKey, () => _customerRepository.GetAllCustomersAsync());
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetCustomerByIdAsync), id);
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<CustomerViewModel> AddCustomerAsync(Customer customer)
        {
            await ClearGetAllKeysAsync();
            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<CustomerViewModel> AuthenticateCustomerAsync(string email, string passwordHash)
        {
            return await _customerRepository.AuthenticateCustomerAsync(email, passwordHash);
        }

        public async Task<int> DeleteCustomerAsync(int id)
        {
            await ClearGetAllKeysAsync();
            await ClearGetIdKeyAsync(id);
            return await _customerRepository.DeleteCustomerAsync(id);
        }

        public async Task<CustomerViewModel> UpdateCustomerAsync(int id, Customer customer)
        {
            await ClearGetAllKeysAsync();
            await ClearGetIdKeyAsync(id);
            return await _customerRepository.UpdateCustomerAsync(id, customer);
        }

        private async Task ClearGetAllKeysAsync()
        {
            var cachedKeyAll = GetKey(KEY_PREFIX, nameof(GetAllCustomersAsync));
            await _cache.RemoveAsync(cachedKeyAll);
        }

        private async Task ClearGetIdKeyAsync(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetCustomerByIdAsync), id);
            await _cache.RemoveAsync(cachedKey);
        }
    }
}
