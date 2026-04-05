using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Shared.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; }= default!;
        public int? AssignedToId { get; set; }
    }
}
