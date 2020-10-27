using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.webadmin.Services
{
    public class ReportService : IReportService
    {
        private readonly IFileProvider fileProvider;
        private readonly IWebHostEnvironment env;

        public ReportService(IFileProvider fileProvider, IWebHostEnvironment env)
        {
            this.fileProvider = fileProvider;
            this.env = env;
        }

        public string YearRevenue(int year)
        {
            var d = env.WebRootPath;
            var c = fileProvider.GetDirectoryContents(d);
            return "";
        }
    }

    public interface IReportService
    {
        string YearRevenue(int year);
    }
}
