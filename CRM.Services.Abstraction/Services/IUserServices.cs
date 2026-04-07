using CRM.Shared.DTOs.User;
using CRM.Shared.Respones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Abstraction.Services
{
    public interface IUserServices
    {
        Task<GenericeResponse<IEnumerable<UserDTO>>> GetAllUserAsync();

        Task<GenericeResponse<UserDTO>> GetUserByIdAsync(int id);

        Task<GenericeResponse<int>> CreateUserAsync(CreateUserDTO userDTO);

        Task<GenericeResponse<bool>> UpdateUserAsync(UpdateUserDTO userDTO);

        Task<GenericeResponse<bool>> DeleteUserAsync(int id);
    }
}
