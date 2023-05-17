using Directory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory.Data.Configurations
{
    public class ReportDetailConfiguration : IEntityTypeConfiguration<ReportDetail>
    {
        public void Configure(EntityTypeBuilder<ReportDetail> entity)
        {
            entity.ToTable("ReportDetail");

            entity.HasKey(e => e.Id);
        }
    }
}