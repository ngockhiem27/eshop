using eshop.core.ReportModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace eshop.webadmin.Services
{
    public class ReportService : IReportService
    {
        public ReportService()
        {
        }

        public string MonthlyRevenueByYear(int year)
        {
            try
            {
                var reportData = new List<MonthlyRevenueReportModel>();
                foreach (string file in Directory.EnumerateFiles($"_Report/Revenue/{year}/", "*", SearchOption.AllDirectories))
                {
                    var fileData = File.ReadAllText(file);
                    var report = JsonSerializer.Deserialize<MonthlyRevenueReportModel>(fileData);
                    reportData.Add(report);
                }
                reportData.Sort((x, y) => x.Month.CompareTo(y.Month));
                var xAxis = reportData.Select(e => e.Month).ToArray();
                var yAxis = reportData.Select(e => e.Revenue).ToArray();
                var result = new { x = xAxis, y = yAxis };
                return JsonSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string MonthlyLoginByYear(int year)
        {
            try
            {
                var reportData = new List<MonthlyLoginReportModel>();
                foreach (string file in Directory.EnumerateFiles($"_Report/Login/{year}/", "*", SearchOption.AllDirectories))
                {
                    var fileData = File.ReadAllText(file);
                    var report = JsonSerializer.Deserialize<MonthlyLoginReportModel>(fileData);
                    reportData.Add(report);
                }
                reportData.Sort((x, y) => x.Month.CompareTo(y.Month));
                var months = reportData.Select(e => e.Month).ToArray();
                var total = reportData.Select(e => e.Total).ToArray();
                var unique = reportData.Select(e => e.Unique).ToArray();
                var result = new { months = months, total = total, unique = unique };
                return JsonSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string DailyLoginByMonth(int year, int month)
        {
            try
            {
                var fileData = File.ReadAllText($"_Report/Login/{year}/{month}.json");
                var report = JsonSerializer.Deserialize<MonthlyLoginReportModel>(fileData);

                return JsonSerializer.Serialize(report);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public interface IReportService
    {
        string MonthlyRevenueByYear(int year);
        string DailyLoginByMonth(int year, int month);
        string MonthlyLoginByYear(int year);
    }
}
