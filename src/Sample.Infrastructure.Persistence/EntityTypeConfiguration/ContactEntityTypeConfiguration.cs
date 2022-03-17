using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.Contacts;

namespace Sample.Infrastructure.Persistence.EntityTypeConfiguration
{
    internal class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Name).HasMaxLength(200).IsRequired();
        }
    }
}
