using Invoco.ApiGateway.Logging;

namespace Invoco.ApiGateway.Service
{
	public interface IApiRequestLogger
	{
		void Log(ApiLogEntry entry);
		List<ApiLogEntry> GetAll();
	}
}
