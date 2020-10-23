using eshop.core.Entities;
using eshop.core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.core.Interfaces.Repositories
{
    public interface IOrdersRepository
    {
        Task<OrdersViewModel> AddOrder(int customerId, List<OrderItems> items);
        Task<OrdersViewModel> GetOrderById(int id);
        Task<List<OrdersViewModel>> GetAllOrders();
        Task<OrderItemsViewModel> GetOrderItemsById(int id);
        Task<List<OrderItemsViewModel>> GetAllOrderItems();
        Task<List<OrdersViewModel>> GetAllCustomerOrders(int customerId);
    }
}
