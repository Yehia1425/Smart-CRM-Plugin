using CRM.Shared.SharedEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Shared.DTOs.Interaction
{
    public class UpdateInteractionDTO
    {
        public int Id { get; set; }
        public InteractionType Type { get; set; }
        public string Notes { get; set; }
        public int CustomerId { get; set; }
    }
}
