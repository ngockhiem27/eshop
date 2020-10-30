using eshop.webadmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eshop.webadmin.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Revenue(int id)
        {
            var result = reportService.MonthlyRevenueByYear(id);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("Report/Login/{year}")]
        public IActionResult Login(int year)
        {
            var result = reportService.MonthlyLoginByYear(year);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("Report/Login/Country/{year}/{month}")]
        public IActionResult LoginCountry(int year, int month)
        {
            var result = reportService.DailyLoginByMonth(year, month);
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
