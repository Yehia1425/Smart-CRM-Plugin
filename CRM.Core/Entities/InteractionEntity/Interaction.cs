using CRM.Core.Entities.CustomersEntity;
using CRM.Shared.SharedEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.InteractionEntity
{
    public class Interaction:BaseEntity<int>
    {

        public InteractionType Type { get; set; }
        public string Notes { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
