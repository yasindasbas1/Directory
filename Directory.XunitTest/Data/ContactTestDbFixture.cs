using System;
using System.Xml;
using Directory.Data;
using Directory.XunitTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Formes.XunitTest.Data
{
    public class ContactTestDbFixture : IDisposable
    {
        public ContactContextDb Context { get; private set; }

        public ContactTestDbFixture()
        {
            var options = new DbContextOptionsBuilder<ContactContextDb>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;            
                      
            Context= new ContactContextDb(options);

            Context.Contacts.AddRange(TestData.ContactData());
            Context.ContactInformations.AddRange(TestData.ContactInformationData());
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
