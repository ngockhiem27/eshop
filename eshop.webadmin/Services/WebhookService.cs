
using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace eshop.webadmin.Services
{
    public class WebhookService : IWebHookService
    {
        private readonly HttpClient _httpClient;
        private List<string> _subcribers = new List<string>();

        public WebhookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _subcribers.Add("https://localhost:7001/");
        }

        public void NotifyNewProductAsync(ProductViewModel product)
        {
            var uri = API.Webhook.NewProduct();
            StringContent content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            foreach (string subcriber in _subcribers)
            {
                _httpClient.PostAsync(subcriber + uri, content);
            }
        }

        public void NotifyUpdateProductAsync(int id, ProductViewModel product)
        {
            var uri = API.Webhook.UpdateProduct(id);
            StringContent content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            foreach (string subcriber in _subcribers)
            {
                _httpClient.PutAsync(subcriber + uri, content);
            }
        }

        public void NotifyNewProductImageAsync(int id, ImageViewModel image)
        {
            var uri = API.Webhook.NewProductImage(id);
            StringContent content = new StringContent(JsonSerializer.Serialize(image), Encoding.UTF8, "application/json");
            foreach (string subcriber in _subcribers)
            {
                _httpClient.PostAsync(subcriber + uri, content);
            }
        }

        public void NotifyRemoveProductAsync(int id)
        {
            var uri = API.Webhook.RemoveProduct(id);
            foreach (string subcriber in _subcribers)
            {
                _httpClient.DeleteAsync(subcriber + uri);
            }
        }
    }
}
