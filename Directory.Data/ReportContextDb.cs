using Microsoft.EntityFrameworkCore;
using Directory.Data.Entities;

namespace Directory.Data
{
    public class ReportContextDb : DbContext
    {
        public ReportContextDb(DbContextOptions<ReportContextDb> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportContextDb).Assembly);
        }

        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportDetail> ReportDetails { get; set; } = null!;
        
    }
}
