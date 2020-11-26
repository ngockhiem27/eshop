using eshop.webadmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.webadmin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _order;

        public OrderController(IOrderService order)
        {
            _order = order;
        }
        public async Task<IActionResult> Index()
        {
            var (_, result) = await _order.GetAllOrders();
            return View(result);
        }
    }
}
