using Directory.Core;
using Directory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> entity)
        {
            entity.ToTable("Report");

            entity.HasKey(e => e.Id);

            entity.Property(p => p.RequestTime)
                .HasColumnType("datetime");

            entity.Property(p => p.CompletedTime)
                .HasColumnType("datetime");

            entity.Property(p => p.Status)
                .HasConversion(v => Convert.ToByte(v),
                    v => (ReportStatuses)Enum.Parse(typeof(ReportStatuses), v.ToString()));

            entity.HasMany(p => p.ReportDetail)
                .WithOne()
                .HasForeignKey(p => p.ReportId)
                .IsRequired();
        }
    }
}