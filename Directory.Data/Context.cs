using Directory.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Directory.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }

        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<ContactInformation> ContactInformations { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportDetail> ReportDetails { get; set; } = null!;
        
    }
}
