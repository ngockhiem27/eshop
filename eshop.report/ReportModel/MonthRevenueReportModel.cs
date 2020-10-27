using System;
using System.Collections.Generic;
using System.Text;

namespace eshop.report.ReportModel
{
    public class DayRevenue
    {
        public int Day { get; set; }
        public int OrdersCount { get; set; }
        public decimal Revenue { get; set; }
    }
    public class MonthRevenueReportModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int OrdersCount { get; set; }
        public decimal Revenue { get; set; }
        public List<DayRevenue> Days { get; set; }
        public MonthRevenueReportModel()
        {
            Days = new List<DayRevenue>();
        }
    }
}
