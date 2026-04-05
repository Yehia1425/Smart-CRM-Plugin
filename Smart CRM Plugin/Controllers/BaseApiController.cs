using CRM.Shared.Respones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smart_CRM_Plugin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResponse<T>(GenericeResponse<T> response)
        {
            if (response is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return response.StatusCode switch
            {
                StatusCodes.Status200OK => Ok(response),
                StatusCodes.Status400BadRequest => BadRequest(response),
                StatusCodes.Status401Unauthorized => Unauthorized(response),
                StatusCodes.Status403Forbidden => Forbid(),
                StatusCodes.Status404NotFound => NotFound(response),
                StatusCodes.Status500InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, response),
                _ => StatusCode(response.StatusCode, response)
            };
        }
    }
}
