using LogParser.Reporter;
using System;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0 && args[0] == "1")
            {
                Fetcher.Fetcher fetcher = new Fetcher.Fetcher();
                fetcher.Fetch("eshop.apiservices");
                Console.WriteLine("END FETCHING!!!");
                return;
            }
            if (args.Length > 0 && args[0] == "2")
            {
                ReportGenerator reportGenerator = new ReportGenerator();
                var month = Int32.Parse(args[1]);
                var year = Int32.Parse(args[2]);
                reportGenerator.MonthlyRevenue(month, year);
                Console.WriteLine("END REPORT GENERATING!!!");
                return;
            }

            ReportGenerator report = new ReportGenerator();
            //report.MonthlyLogin(2020, 10);
            for(int i = 1; i < 13; i++)
            {
                report.MonthlyRevenue(i, 2020);
            }
            Console.WriteLine("END!!!");
        }
    }
}
