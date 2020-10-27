using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public interface IOrderService
    {
        Task<(HttpStatusCode, List<OrdersViewModel>)> GetAllOrders();
    }
}