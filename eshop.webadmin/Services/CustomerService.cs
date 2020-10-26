using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, List<CustomerViewModel>)> GetAllCustomers()
        {
            var uri = API.Customer.GetAllCustomers();
            var response = await _httpClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<CustomerViewModel>>(responseStream);
            return (statusCode, result);
        }
    }
}
