using Invoco.ApiGateway.Logging;
using Invoco.ApiGateway.Model;
using Invoco.ApiGateway.Service;
using Microsoft.AspNetCore.Mvc;

namespace Invoco.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelephonyController : ControllerBase
    {
        private readonly LacrmService _lacrmService;
        private readonly IApiRequestLogger _logger;

        public TelephonyController(LacrmService lacrmService, IApiRequestLogger logger)
        {
            _lacrmService = lacrmService;
            _logger = logger;
        }

        [HttpPost("call")]
        public async Task<IActionResult> ReceiveCall([FromBody] CallEvent callEvent)
        {
            if (callEvent == null ||
                string.IsNullOrWhiteSpace(callEvent.CallersTelephoneNumber) ||
                string.IsNullOrWhiteSpace(callEvent.CallersName) ||
                string.IsNullOrWhiteSpace(callEvent.CallId))
            {
                return BadRequest("Invalid call data.");
            }

            var success = await _lacrmService.CreateContactAsync(
                callEvent.CallersName,
                callEvent.CallersTelephoneNumber
            );

            return success
                ? Ok(new { message = "Contact successfully created in LACRM.", callEvent.CallId })
                : StatusCode(500, "Failed to create contact in LACRM.");
        }
    }
}
