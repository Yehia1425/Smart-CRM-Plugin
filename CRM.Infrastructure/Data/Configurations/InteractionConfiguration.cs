using CRM.Core.Entities.InteractionEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Data.Configurations
{
    public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
    {
        public void Configure(EntityTypeBuilder<Interaction> builder)
        {
            builder.ToTable("Interactions");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Type)
                .HasConversion<string>();

            builder.Property(i => i.Notes)
                .HasMaxLength(500);

            builder.HasOne(i => i.Customer)
                .WithMany(c => c.Interactions)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
