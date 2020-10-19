using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
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

        public async Task<(HttpStatusCode, CategoryViewModel)> AddCategory(CategoryInfoRequest categoryRequest)
        {
            var uri = API.Category.AddCategory();
            StringContent postData = new StringContent(JsonSerializer.Serialize(categoryRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, postData);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<CategoryViewModel>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<(HttpStatusCode, CategoryViewModel)> UpdateCategory(CategoryInfoRequest categoryRequest)
        {
            var uri = API.Category.UpdateCategory(categoryRequest.Id);
            StringContent putData = new StringContent(JsonSerializer.Serialize(categoryRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri, putData);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<CategoryViewModel>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<HttpStatusCode> DeleteCategory(int id)
        {
            var uri = API.Category.DeleteCategory(id);
            var response = await _httpClient.DeleteAsync(uri);
            return response.StatusCode;
        }
    }
}
