using eshop.webshop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.webshop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrdersService _order;

        public OrderController(IOrdersService order)
        {
            _order = order;
        }

        public async Task<IActionResult> Index()
        {
            var customerId = Int32.Parse(User.Identities.First().FindFirst("UserId").Value);
            var (_, result) = await _order.GetAllOrders(customerId);
            return View(result);
        }
    }
}
