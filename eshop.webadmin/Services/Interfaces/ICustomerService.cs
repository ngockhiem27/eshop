using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface ICustomerService
    {
        Task<(HttpStatusCode, List<CustomerViewModel>)> GetAllCustomers();
    }
}