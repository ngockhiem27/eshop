using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface IOrdersService
    {
        Task<(HttpStatusCode, OrdersViewModel)> AddOrder(OrderInfoRequest orderInfo);
        Task<(HttpStatusCode, List<OrdersViewModel>)> GetAllOrders(int customerId);
    }
}