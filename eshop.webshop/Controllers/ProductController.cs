using Microsoft.AspNetCore.Mvc;

namespace eshop.webshop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
