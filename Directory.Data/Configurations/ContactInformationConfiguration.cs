using Directory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory.Data.Configurations
{
    public class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> entity)
        {
            entity.ToTable("ContactInformation");

            entity.HasKey(e => e.Id);

            entity.HasQueryFilter(p => !p.Deleted);
        }
    }
}