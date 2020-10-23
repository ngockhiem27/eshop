using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using eshop.webshop.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly HttpClient _httpClient;

        public OrdersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, OrdersViewModel)> AddOrder(OrderInfoRequest orderInfo)
        {
            var uri = API.Orders.AddOrder();
            var postData = new StringContent(JsonSerializer.Serialize(orderInfo), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, postData);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<OrdersViewModel>(responseStream);
            return (statusCode, result);
        }

        public async Task<(HttpStatusCode, List<OrdersViewModel>)> GetAllOrders(int customerId)
        {
            var uri = API.Orders.GetAllOrders(customerId);
            var response = await _httpClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<OrdersViewModel>>(responseStream);
            return (statusCode, result);
        }
    }
}
