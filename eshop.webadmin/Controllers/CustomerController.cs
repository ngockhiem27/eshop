using eshop.webadmin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eshop.webadmin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customer;

        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
        }

        public async Task<IActionResult> Index()
        {
            var (_, result) = await _customer.GetAllCustomers();
            return View(result);
        }
    }
}
