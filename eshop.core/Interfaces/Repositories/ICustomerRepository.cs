using eshop.core.Entities;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        public Task<List<CustomerViewModel>> GetAllCustomersAsync();
        public Task<CustomerViewModel> AddCustomerAsync(Customer customer);
        public Task<CustomerViewModel> GetCustomerByIdAsync(int id);
        public Task<CustomerViewModel> GetCustomerByEmailAsync(string email);
        public Task<CustomerViewModel> AuthenticateCustomerAsync(string email, string passwordHash);
        public Task<int> DeleteCustomerAsync(int id);
        public Task<CustomerViewModel> UpdateCustomerAsync(int id, Customer customer);
    }
}