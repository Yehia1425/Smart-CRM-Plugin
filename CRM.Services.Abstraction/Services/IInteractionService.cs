using CRM.Shared.DTOs.Interaction;
using CRM.Shared.Respones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Abstraction.Services
{
    public interface IInteractionService
    {
        Task<GenericeResponse<IEnumerable<InteractionDTO>>> GetAllAsync();

        Task<GenericeResponse<InteractionDTO>> GetByIdAsync(int id);

        Task<GenericeResponse<int>> AddAsync(CreateInteractionDTO createDTO);

        Task<GenericeResponse<bool>> UpdateAsync(UpdateInteractionDTO updateDTO);

        Task<GenericeResponse<bool>> DeleteAsync(int id);

        Task<GenericeResponse<IEnumerable<InteractionDTO>>> GetByCustomerIdAsync(int customerId);
    }
}
