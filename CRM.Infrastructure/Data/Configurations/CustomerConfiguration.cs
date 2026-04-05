using CRM.Core.Entities.CustomersEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Phone)
            .HasMaxLength(20);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.Property(c => c.Status)
            .HasConversion<string>();

        // العلاقة مع User
        builder.HasOne(c => c.AssignedTo)
            .WithMany(u => u.Customers)
            .HasForeignKey(c => c.AssignedToId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}