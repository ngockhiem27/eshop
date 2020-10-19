using eshop.core.DTO.Request;
using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
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

        public async Task<(HttpStatusCode, ProductViewModel)> AddProduct(ProductInfoRequest productRequest)
        {
            var uri = API.Product.AddProduct();
            StringContent postData = new StringContent(JsonSerializer.Serialize(productRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, postData);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ProductViewModel>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<(HttpStatusCode, ProductViewModel)> UpdateProduct(ProductInfoRequest productRequest)
        {
            var uri = API.Product.UpdateProduct(productRequest.Id);
            StringContent putData = new StringContent(JsonSerializer.Serialize(productRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri, putData);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ProductViewModel>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<HttpStatusCode> DeleteProduct(int id)
        {
            var uri = API.Product.DeleteProduct(id);
            var response = await _httpClient.DeleteAsync(uri);
            return response.StatusCode;
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

        public async Task<(HttpStatusCode, ImageViewModel)> AddProductImage(int id, IFormFile file)
        {
            var uri = API.Product.AddProductImages(id);
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.OpenReadStream())
            {
                Headers =
                {
                    ContentLength = file.Length,
                    ContentType = new MediaTypeHeaderValue(file.ContentType)
                }
            }, "File", WebUtility.HtmlEncode(file.FileName));

            var response = await _httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode) return (response.StatusCode, null);
            using var streamRead = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ImageViewModel>(streamRead);
            return (response.StatusCode, result);
        }

        public async Task<HttpStatusCode> DeleteProductImage(int imgId)
        {
            var uri = API.Product.DeleteProductImages(imgId);
            var response = await _httpClient.DeleteAsync(uri);
            return response.StatusCode;
        }
    }
}
