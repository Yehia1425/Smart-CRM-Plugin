using CRM.Core.Entities.TagEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.CustomersEntity
{
    public class CustomerTag
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
