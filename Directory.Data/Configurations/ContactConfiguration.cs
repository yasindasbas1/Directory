using Directory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Directory.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.ToTable("Contact");

            entity.HasKey(e => e.Id);

            entity.HasQueryFilter(p => !p.Deleted);


            entity.HasMany(p => p.ContactInformations)
                .WithOne()
                .HasForeignKey(p=> p.ContactId)
                .IsRequired();
        }
    }
}