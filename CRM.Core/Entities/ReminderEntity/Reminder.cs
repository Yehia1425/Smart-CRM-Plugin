using CRM.Core.Entities.CustomersEntity;
using CRM.Shared.SharedEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.ReminderEntity
{
    public class Reminder
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime ReminderDate { get; set; }

        public ReminderStatus Status { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
