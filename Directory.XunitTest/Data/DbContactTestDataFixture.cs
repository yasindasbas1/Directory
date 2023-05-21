using System;
using Directory.Data;
using Microsoft.EntityFrameworkCore;

namespace Formes.XunitTest.Data
{
    public class DbContactTestDataFixture : IDisposable
    {
        public ContactContextDb Context { get; private set; }

        public DbContactTestDataFixture()
        {
            var options = new DbContextOptionsBuilder<ContactContextDb>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;            
                      
            Context= new ContactContextDb(options);

            Context.SaveChanges();
            Context.ChangeTracker.Clear();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
