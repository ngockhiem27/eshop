using eshop.core.ViewModels;
using eshop.webshop.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, List<CategoryViewModel>)> GetAllCategory()
        {
            var uri = API.Category.GetAllCategory();
            var response = await _httpClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<CategoryViewModel>>(responseStream);
            return (statusCode, result);
        }

        public async Task<(HttpStatusCode, List<CategoryViewModel>)> GetAllCategoryWithProduct()
        {
            var uri = API.Category.GetAllCategoryWithProduct();
            var response = await _httpClient.GetAsync(uri);
            var statusCode = response.StatusCode;
            if (!response.IsSuccessStatusCode) return (statusCode, null);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<List<CategoryViewModel>>(responseStream);
            return (statusCode, result);
        }
    }
}
