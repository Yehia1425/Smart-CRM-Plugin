using CRM.Core.Entities.CustomersEntity;
using CRM.Shared.SharedEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core.Entities.UserEntity
{
    public class User:BaseEntity<int>
    {

        public string Name { get; set; }
        public string Email { get; set; }

        public UserRole Role { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
