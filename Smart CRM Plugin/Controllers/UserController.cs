using CRM.Services.Abstraction.Services;
using CRM.Shared.DTOs.Customer;
using CRM.Shared.DTOs.User;
using CRM.Shared.Respones;
using Microsoft.AspNetCore.Mvc;

namespace Smart_CRM_Plugin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: BaseUrl/api/Users
        [HttpGet]
        public async Task<ActionResult<GenericeResponse<IEnumerable<UserDTO>>>> GetAllUsers()
        {
            var result = await _userServices.GetAllUserAsync();
            return HandleResponse(result);
        }

        //Get:BaseUrl/api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericeResponse<UserDTO>>> GetByIdUser(int id)
        {
            var result = await _userServices.GetUserByIdAsync(id);
            return HandleResponse(result);
        }
        //Post:BaseUrl/api/Users
        [HttpPost]
        public async Task<ActionResult<GenericeResponse<int>>> CreateUser(CreateUserDTO dto)
        {
            var result = await _userServices.CreateUserAsync(dto);
            return HandleResponse(result);
        }

        //Delete:BaseUrl/api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericeResponse<bool>>> DeleteCustomer(int id)
        {
            var result = await _userServices.DeleteUserAsync(id);
            return HandleResponse(result);
        }
    }
}
