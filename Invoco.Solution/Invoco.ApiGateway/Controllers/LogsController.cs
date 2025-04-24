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
            var logs = _logger.GetAll();

            if (logs == null || logs.Count == 0)
            {
                return NoContent(); // Optional: returns 204 if no logs available
            }

            return Ok(logs); // Returns a proper JSON array
        }
    }
}
