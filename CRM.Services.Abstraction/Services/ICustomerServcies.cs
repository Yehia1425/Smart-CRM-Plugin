using CRM.Shared.DTOs.Customer;
using CRM.Shared.Respones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Servcies
{
    public interface ICustomerServcies
    {
       
        
            Task<GenericeResponse<IEnumerable<CustomerDto>>> GetAllAsync();
            Task<GenericeResponse<CustomerDto>> GetByIdAsync(int id);
            Task<GenericeResponse<int>> CreateAsync(CreateCustomerDto dto);
            Task<GenericeResponse<bool>> UpdateAsync(UpdateCustomerDto dto);
            Task<GenericeResponse<bool>> DeleteAsync(int id);
        
    }
}
