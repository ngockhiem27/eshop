using System.Collections.Generic;

namespace eshop.core.ReportModels
{
    public class DailyLoginReportModel
    {
        public int Day { get; set; }
        public int Total { get; set; }
    }
    public class CountryLoginReportModel
    {
        public string Country { get; set; }
        public int Total { get; set; }
    }
    public class MonthlyLoginReportModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Total { get; set; }
        public int Unique { get; set; }
        public List<CountryLoginReportModel> Countries { get; set; }
        public List<DailyLoginReportModel> Daily { get; set; }
        public MonthlyLoginReportModel()
        {
            Countries = new List<CountryLoginReportModel>();
            Daily = new List<DailyLoginReportModel>();
        }
    }
}
