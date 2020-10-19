using eshop.core.ViewModels;
using eshop.webshop.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProduct()
        {
            var uri = API.Product.GetAllProduct();
            var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductViewModel>>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<(HttpStatusCode, List<ProductViewModel>)> GetAllProductWithCategory()
        {
            var uri = API.Product.GetAllProductWithCategory();
            var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ProductViewModel>>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<(HttpStatusCode, List<ImageViewModel>)> GetProductImage(int id)
        {
            var uri = API.Product.GetProductImages(id);
            var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<ImageViewModel>>(streamRead);
            return (response.StatusCode, result);
        }
    }
}
