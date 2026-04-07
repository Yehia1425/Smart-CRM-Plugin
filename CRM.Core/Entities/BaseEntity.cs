using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
