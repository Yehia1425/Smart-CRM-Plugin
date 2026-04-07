using CRM.Services.Abstraction.Services;
using CRM.Shared.DTOs.Interaction;
using CRM.Shared.Respones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smart_CRM_Plugin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : BaseApiController
    {
        private readonly IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        // GET: BaseUrl/api/Interaction
        [HttpGet]
        public async Task<ActionResult<GenericeResponse<IEnumerable<InteractionDTO>>>> GetAll()
        {
            var result = await _interactionService.GetAllAsync();
            return HandleResponse(result);
        }


        // GET: BaseUrl/api/Interaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericeResponse<InteractionDTO>>> GetById(int id)
        {
            var result = await _interactionService.GetByIdAsync(id);
            return HandleResponse(result);
        }


        // POST: BaseUrl/api/Interaction
        [HttpPost]
        public async Task<ActionResult<GenericeResponse<int>>> Create(CreateInteractionDTO dto)
        {
            var result = await _interactionService.AddAsync(dto);
            return HandleResponse(result);
        }


        // PUT: BaseUrl/api/Interaction
        [HttpPut]
        public async Task<ActionResult<GenericeResponse<bool>>> Update(UpdateInteractionDTO dto)
        {
            var result = await _interactionService.UpdateAsync(dto);
            return HandleResponse(result);
        }


        // DELETE: BaseUrl/api/Interaction/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenericeResponse<bool>>> Delete(int id)
        {
            var result = await _interactionService.DeleteAsync(id);
            return HandleResponse(result);
        }


        // GET: BaseUrl/api/Interaction/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<GenericeResponse<IEnumerable<InteractionDTO>>>> GetByCustomerId(int customerId)
        {
            var result = await _interactionService.GetByCustomerIdAsync(customerId);
            return HandleResponse(result);
        }
    }
}
