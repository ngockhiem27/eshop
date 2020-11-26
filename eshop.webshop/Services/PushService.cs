using eshop.infrastructure.WebPush.Models;
using eshop.webshop.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public class PushService : IPushService
    {
        private readonly HttpClient _httpClient;

        public PushService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<HttpStatusCode> Subscribe(PushSubscriptionModel subscription)
        {
            var uri = API.PushNotification.Subscribe();
            StringContent data = new StringContent(JsonSerializer.Serialize(subscription), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, data);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> UnSubscribe(PushSubscriptionModel subscription)
        {
            var uri = API.PushNotification.UnSubscribe();
            StringContent data = new StringContent(JsonSerializer.Serialize(subscription), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, data);
            return response.StatusCode;
        }
    }
}
