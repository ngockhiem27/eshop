using Microsoft.AspNetCore.Mvc;

namespace eshop.webshop.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
