using Microsoft.AspNetCore.Mvc;

namespace eshop.webshop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
