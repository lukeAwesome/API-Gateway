using Invoco.ApiGateway.Logging;
using Invoco.ApiGateway.Service;
using Microsoft.AspNetCore.Mvc;

namespace Invoco.ApiGateway.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LogsController : ControllerBase
	{
		private readonly IApiRequestLogger _logger;

		public LogsController(IApiRequestLogger logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<List<ApiLogEntry>> GetLogs()
		{
			return Ok(_logger.GetAll());
		}
	}
}
