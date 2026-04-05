using CRM.Core.Entities.NoteEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Infrastructure.Data.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("Notes");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(n => n.Customer)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
