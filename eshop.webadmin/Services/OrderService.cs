using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, List<OrdersViewModel>)> GetAllOrders()
        {
            var uri = API.Order.GetAllOrders();
            var response = await _httpClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<OrdersViewModel>>(responseStream);
            return (statusCode, result);
        }
    }
}
