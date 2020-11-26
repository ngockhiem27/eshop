using eshop.core.PushNotificationModels;
using eshop.infrastructure.WebPush.Models;
using eshop.webadmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eshop.webadmin.Controllers
{
    [Authorize]
    public class PushController : Controller
    {
        private readonly IPushService _pushService;

        public PushController(IPushService pushService)
        {
            this._pushService = pushService;
        }

        public IActionResult Index()
        {
            var subscriptions = _pushService.Subscriptions.Select(s => s.Value).ToList();

            ViewData["Subscriptions"] = subscriptions;
            return View();
        }

        public IActionResult Subscription()
        {
            return Ok(_pushService.Subscriptions.Select(s => s.Value).ToList());
        }

        [HttpPost()]
        public IActionResult Notify([FromQuery(Name = "endpoint")] string endpoint, [FromBody] NotificationModel notification)
        {
            _pushService.Notify(endpoint, notification);
            return Ok();
        }

        public IActionResult Broadcast([FromBody] NotificationModel notification)
        {
            _pushService.Broadcast(notification);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Subscribe([FromBody] PushSubscriptionModel subscription)
        {
            _pushService.Subscribe(subscription);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult UnSubscribe([FromBody] PushSubscriptionModel subscription)
        {
            _pushService.UnSubscribe(subscription);
            return Ok();
        }
    }
}
