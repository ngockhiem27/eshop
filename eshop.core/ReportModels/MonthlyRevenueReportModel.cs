using System.Collections.Generic;

namespace eshop.core.ReportModels
{
    public class DailyRevenue
    {
        public int Day { get; set; }
        public int OrdersCount { get; set; }
        public decimal Revenue { get; set; }
    }
    public class MonthlyRevenueReportModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int OrdersCount { get; set; }
        public decimal Revenue { get; set; }
        public List<DailyRevenue> Days { get; set; }
        public MonthlyRevenueReportModel()
        {
            Days = new List<DailyRevenue>();
        }
    }
}
