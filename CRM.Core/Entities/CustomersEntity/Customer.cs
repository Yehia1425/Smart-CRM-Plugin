using CRM.Core.Entities.NoteEntity;
using CRM.Core.Entities.ReminderEntity;
using CRM.Core.Entities.UserEntity;
using CRM.Shared.SharedEnums;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.CustomersEntity
{
    public class Customer:BaseEntity<int>
    {

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public CustomerStatus Status { get; set; }

        public int? AssignedToId { get; set; }
        public User AssignedTo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<InteractionEntity.Interaction> Interactions { get; set; }
        public ICollection<CustomerTag> CustomerTags { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
    }
}
