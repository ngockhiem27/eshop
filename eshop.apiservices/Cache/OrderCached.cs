using eshop.core.Entities;
using eshop.core.Interfaces.Repositories;
using eshop.core.ViewModels;
using eshop.infrastructure.RedisCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eshop.apiservices.Cache
{
    public class OrderCached : BaseCached, IOrderCached
    {
        private readonly IOrdersRepository _ordersRepository;
        private const string KEY_PREFIX = "Order.";

        public OrderCached(ICache cache, IOrdersRepository ordersRepository) : base(cache)
        {
            _ordersRepository = ordersRepository;
        }

        private void ClearGetAllOrders()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllOrders));
            _cache.RemoveAsync(cachedKey);
        }
        private void ClearGetCustomerOrders(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllCustomerOrders), id);
            _cache.RemoveAsync(cachedKey);
        }

        public async Task<OrdersViewModel> GetOrderById(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetOrderById), id);
            return await VerifyCache(cachedKey, () => _ordersRepository.GetOrderById(id));
        }

        public async Task<List<OrdersViewModel>> GetAllOrders()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllOrders));
            return await VerifyCache(cachedKey, () => _ordersRepository.GetAllOrders());
        }

        public async Task<OrderItemsViewModel> GetOrderItemsById(int id)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetOrderItemsById), id);
            return await VerifyCache(cachedKey, () => _ordersRepository.GetOrderItemsById(id));
        }

        public async Task<List<OrderItemsViewModel>> GetAllOrderItems()
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllOrderItems));
            return await VerifyCache(cachedKey, () => _ordersRepository.GetAllOrderItems());
        }

        public async Task<List<OrdersViewModel>> GetAllCustomerOrders(int customerId)
        {
            var cachedKey = GetKey(KEY_PREFIX, nameof(GetAllCustomerOrders), customerId);
            return await VerifyCache(cachedKey, () => _ordersRepository.GetAllCustomerOrders(customerId));
        }

        public async Task<OrdersViewModel> AddOrder(int customerId, List<OrderItems> items)
        {
            ClearGetAllOrders();
            ClearGetCustomerOrders(customerId);
            return await _ordersRepository.AddOrder(customerId, items);
        }
    }
}
