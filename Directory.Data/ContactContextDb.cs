using Directory.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Directory.Data
{
    public class ContactContextDb : DbContext
    {
        public ContactContextDb(DbContextOptions<ContactContextDb> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContactContextDb).Assembly);
        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<ContactInformation> ContactInformations { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportDetail> ReportDetails { get; set; } = null!;
        
    }
}
