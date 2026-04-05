using CRM.Core.Entities.CustomersEntity;
using CRM.Core.Entities.TagEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Data.Configurations
{
    public class CustomerTagConfiguration : IEntityTypeConfiguration<CustomerTag>
    {
        public void Configure(EntityTypeBuilder<CustomerTag> builder)
        {
            builder.ToTable("CustomerTags");

            builder.HasKey(ct => new { ct.CustomerId, ct.TagId });

            builder.HasOne(ct => ct.Customer)
                .WithMany(c => c.CustomerTags)
                .HasForeignKey(ct => ct.CustomerId);

            builder.HasOne(ct => ct.Tag)
                .WithMany(t => t.CustomerTags)
                .HasForeignKey(ct => ct.TagId);
        }
    }
}
