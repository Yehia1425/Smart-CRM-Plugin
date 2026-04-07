using CRM.Services.Servcies;
using CRM.Shared.DTOs.Customer;
using CRM.Shared.Respones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smart_CRM_Plugin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseApiController
    {
        private readonly ICustomerServcies _customerServcies;

        public CustomersController(ICustomerServcies customerServcies)
        {
           _customerServcies = customerServcies;
        }

        // GET: BaseUrl/api/Customers
        [HttpGet]
        public async Task<ActionResult<GenericeResponse<IEnumerable<CustomerDto>>>> GetAllCustomer()
        {
            var result = await _customerServcies.GetAllAsync();
            return HandleResponse(result);
        }
        //Get:BaseUrl/api/Customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericeResponse<CustomerDto>>> GetByIdCustomer(int id)
        {
            var result = await _customerServcies.GetByIdAsync(id);
            return HandleResponse(result);
        }
        //Post:BaseUrl/api/Customers
        [HttpPost]
        public async Task<ActionResult<GenericeResponse<int>>> CreateCustomer(CreateCustomerDto dto)
        {
            var result = await _customerServcies.CreateAsync(dto);
            return HandleResponse(result);
        }
        //Delete:BaseUrl/api/Customers/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericeResponse<bool>>> DeleteCustomer(int id)
        {
            var result = await _customerServcies.DeleteAsync(id);
            return HandleResponse(result);
        }






    }
}
