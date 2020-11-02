
using eshop.core.ViewModels;
using eshop.webadmin.Infrastructure;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public class WebhookService : IWebHookService
    {
        private readonly HttpClient _httpClient;

        public WebhookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void PostProductUpdateAsync(ProductViewModel product)
        {
            var uri = API.Webhook.ProductUpdate();
            StringContent content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            _httpClient.PostAsync(uri, content);
        }
    }
}
