using Dapper;
using Dapper.Oracle;
using eshop.core.ReportModels;
using eshop.report.StoredViewModel;
using LogParser.LogModel;
using Nest;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LogParser.Reporter
{
    public class ReportGenerator : IDisposable
    {
        private ElasticClient _elasticClient;
        private IDbConnection _dbConnection;
        private const string REPORT_STORED_PACKAGE = "ESHOP_REPORT_API";
        private const string BASE_REPORT_PATH = "_Report";
        public ReportGenerator()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node)
                .DefaultMappingFor<CustomerLogin>(m => m.IndexName("customer_login"))
                .DefaultMappingFor<CustomerLogin>(m => m.IdProperty(p => p.Id));
            _elasticClient = new ElasticClient(settings);

            var f = new OracleClientFactory();
            _dbConnection = f.CreateConnection();
            _dbConnection.ConnectionString = "User Id=c##khiem;Password=123456;Data Source=localhost:1521/orcl;Pooling=true";
        }

        public object Query()
        {
            //var response = _client.Get<object>(1, idx => idx.Index("customer_login"));
            var res = _elasticClient.Search<CustomerLogin>(m => m.Query(q => q.MatchAll()).Aggregations(
                aggs => aggs.DateHistogram("month", m => m.Field(p => p.DateTime)
                                                          .CalendarInterval(DateInterval.Month)
                                                          .MinimumDocumentCount(2)
                                                          .Aggregations(agg => agg.Cardinality("car", f => f.Field(f => f.Id)))
                                                          )
                ));
            //var s = res.Documents.ToList();
            //var rq = new SearchRequest<CustomerLogin>
            //{
            //    Aggregations = new AggregationDictionary
            //    {

            //    }
            //};
            var aggs = res.Aggregations.DateHistogram("month");
            var car = res.Aggregations.Average("car");
            foreach (var item in aggs.Buckets)
            {
                var di = item;
            }

            var b = res.Aggregations.MinBucket("UK");
            return res;
        }

        public void MonthlyLogin(int month, int year)
        {
            var response = _elasticClient.Search<CustomerLogin>(sd => sd.Query(q => q
                .DateRange(dr => dr
                    .Field(f => f.DateTime)
                    .GreaterThanOrEquals(new DateTime(day: 1, month: month, year: year))
                    .LessThanOrEquals(new DateTime(day: DateTime.DaysInMonth(year, month), month: month, year: year))))
                        .Aggregations(agg => agg
                            .Terms("Country", t => t.Field(f => f.Country))
                            .Cardinality("Unique", c => c.Field(f => f.Id))
                            .DateHistogram("Daily", ad => ad
                                .Field(f => f.DateTime)
                                .CalendarInterval(DateInterval.Day))
                        )
            );
            var report = new MonthlyLoginReportModel();
            report.Month = month;
            report.Year = year;
            report.Total = response.Documents.Count;
            report.Unique = (int)response.Aggregations.Cardinality("Unique").Value;

            var countries = response.Aggregations.Terms("Country").Buckets.ToList();
            countries.ForEach(c =>
            {
                report.Countries.Add(new CountryLoginReportModel { Country = c.Key, Total = (int)c.DocCount });
            });

            var daily = response.Aggregations.DateHistogram("Daily").Buckets.ToList();
            daily.ForEach(d =>
            {
                report.Daily.Add(new DailyLoginReportModel { Day = Int32.Parse(d.Date.ToString("dd")), Total = (int)d.DocCount });
            });
            var filePath = GetFilePath("Login", year, month);
            WriteReport(filePath, JsonSerializer.Serialize(report));
            return;
        }

        public void MonthlyRevenue(int month, int year)
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open) _dbConnection.Open();
                var param = new OracleDynamicParameters();
                param.Add("RP_MONTH", month, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add("RP_YEAR", year, dbType: OracleMappingType.Int16, direction: ParameterDirection.Input);
                param.Add("RP_CURSOR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
                var query = REPORT_STORED_PACKAGE + ".SP_MONTHLYSALES";
                var result = SqlMapper.Query<MonthRevenueViewModel>(_dbConnection, query, param: param, commandType: CommandType.StoredProcedure).ToList();

                var monthRevenue = new MonthlyRevenueReportModel();
                monthRevenue.Month = month;
                monthRevenue.Year = year;
                monthRevenue.Revenue = result.Sum(e => e.Revenue);
                monthRevenue.OrdersCount = result.Sum(e => e.Orders_Count);
                result.ForEach(r =>
                {
                    monthRevenue.Days.Add(new DailyRevenue
                    {
                        Day = r.Day,
                        OrdersCount = r.Orders_Count,
                        Revenue = r.Revenue
                    });
                });
                var filePath = GetFilePath("Revenue", year, month);
                WriteReport(filePath, JsonSerializer.Serialize(monthRevenue));
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
        }

        private void WriteReport(string path, string data)
        {
            using var f = new FileStream(path, FileMode.Create);
            using StreamWriter writer = new StreamWriter(f);
            writer.Write(data);
        }

        private string GetFilePath(string category, int year, int month)
        {
            var dirPath = Path.Combine(Directory.GetCurrentDirectory(), BASE_REPORT_PATH, category, year.ToString());
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            var filePath = Path.Combine(dirPath, $"{month}.json");
            return filePath;
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
