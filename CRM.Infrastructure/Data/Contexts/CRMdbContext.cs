using CRM.Core.Entities.CustomersEntity;
using CRM.Core.Entities.InteractionEntity;
using CRM.Core.Entities.NoteEntity;
using CRM.Core.Entities.ReminderEntity;
using CRM.Core.Entities.TagEntity;
using CRM.Core.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CRM.Infrastructure.Data.Contexts
{
    public class CRMdbContext : DbContext
    {
        public CRMdbContext(DbContextOptions<CRMdbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<User> Users { get; set; }


    }
}