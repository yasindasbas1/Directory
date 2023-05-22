using System;
using Directory.Data;
using Directory.XunitTest.Data;
using Microsoft.EntityFrameworkCore;

namespace Formes.XunitTest.Data
{
    public class ReportTestDbFixture : IDisposable
    {
        public ReportContextDb Context { get; private set; }

        public ReportTestDbFixture()
        {
            var options = new DbContextOptionsBuilder<ReportContextDb>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;            
                      
            Context= new ReportContextDb(options);

            Context.Reports.AddRange(TestData.ReportData());
            Context.ReportDetails.AddRange(TestData.ReportDetailData());

            Context.SaveChanges();
            Context.ChangeTracker.Clear();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
