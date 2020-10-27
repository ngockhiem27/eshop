using eshop.webadmin.Services;
using Microsoft.AspNetCore.Mvc;

namespace eshop.webadmin.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult Index()
        {
            reportService.YearRevenue(2020);
            return View();
        }
    }
}
