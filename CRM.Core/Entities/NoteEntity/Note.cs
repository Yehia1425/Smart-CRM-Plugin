using CRM.Core.Entities.CustomersEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.NoteEntity
{
    public class Note
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
