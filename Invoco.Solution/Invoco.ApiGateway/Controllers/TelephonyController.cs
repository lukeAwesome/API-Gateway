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
		private readonly IApiRequestLogger _logger;
		private readonly LacrmService _lacrmService;

		public TelephonyController(IApiRequestLogger logger, LacrmService lacrmService)
		{
			_logger = logger;
			_lacrmService = lacrmService;
		}

		[HttpPost("call")]
		public async Task<IActionResult> ReceiveCall([FromBody] CallEvent callEvent)
		{
			if (callEvent == null || string.IsNullOrWhiteSpace(callEvent.CallersTelephoneNumber))
				return BadRequest("Invalid call data.");

			var success = await _lacrmService.CreateContactAsync(callEvent);

			return success
				? Ok(new { message = "Contact created in LACRM.", callEvent.CallId })
				: StatusCode(500, "Failed to create contact.");
		}
	}
}
