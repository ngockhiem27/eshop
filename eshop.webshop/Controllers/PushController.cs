using eshop.core.ViewModels;
using eshop.infrastructure.WebPush.Models;
using eshop.webshop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.webshop.Controllers
{
    public class PushController : Controller
    {
        private readonly IPushService _pushService;

        public PushController(IPushService pushService)
        {
            this._pushService = pushService;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] PushSubscription subscription)
        {
            CustomerViewModel customer = null;
            if (User.Identity.IsAuthenticated)
            {
                var c = User.Claims.ToList();
                customer = new CustomerViewModel
                {
                    Id = Int32.Parse(User.Claims.Where(c => c.Type == "UserId").First().Value),
                    Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value
                };
            }
            PushSubscriptionModel subscriptionModel = new PushSubscriptionModel { Customer = customer, Subscription = subscription };
            var code = await _pushService.Subscribe(subscriptionModel);
            return StatusCode((int)code);
        }

        [HttpPost]
        public async Task<IActionResult> UnSubscribe([FromBody] PushSubscription subscription)
        {
            PushSubscriptionModel subscriptionModel = new PushSubscriptionModel { Subscription = subscription };
            var code = await _pushService.UnSubscribe(subscriptionModel);
            return StatusCode((int)code);
        }
    }
}
