using CRM.Core.Entities.CustomersEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.TagEntity
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CustomerTag> CustomerTags { get; set; }
    }
}
