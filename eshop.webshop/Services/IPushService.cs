using eshop.infrastructure.WebPush.Models;
using System.Net;
using System.Threading.Tasks;

namespace eshop.webshop.Services
{
    public interface IPushService
    {
        public Task<HttpStatusCode> Subscribe(PushSubscriptionModel subscription);
        public Task<HttpStatusCode> UnSubscribe(PushSubscriptionModel subscription);
    }
}